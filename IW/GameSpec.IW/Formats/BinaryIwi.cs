using GameSpec.Formats;
using GameSpec.Metadata;
using OpenStack.Graphics;
using OpenStack.Graphics.DirectX;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using static GameSpec.IW.Formats.BinaryIwi.FORMAT;

namespace GameSpec.IW.Formats
{
    // https://github.com/XLabsProject/img-format-helper - IWI
    // https://github.com/DentonW/DevIL/blob/master/DevIL/src-IL/src/il_iwi.cpp - IWI
    // https://github.com/XLabsProject/img-format-helper - IWI
    public class BinaryIwi : ITexture, IGetMetadataInfo
    {
        public static Task<object> Factory(BinaryReader r, FileMetadata f, PakFile s) => Task.FromResult((object)new BinaryIwi(r));

        public enum VERSION : byte
        {
            /// <summary>
            /// COD2
            /// </summary>
            COD2 = 0x05,
            /// <summary>
            /// COD4
            /// </summary>
            COD4 = 0x06,
            /// <summary>
            /// COD5
            /// </summary>
            COD5 = 0x06,
            /// <summary>
            /// CODMW2
            /// </summary>
            CODMW2 = 0x08,
            /// <summary>
            /// CODMW3
            /// </summary>
            CODMW3 = 0x08,
            /// <summary>
            /// CODBO1
            /// </summary>
            CODBO1 = 0x0D,
            /// <summary>
            /// CODBO2
            /// </summary>
            CODBO2 = 0x1B,
        }

        public enum FORMAT : byte
        {
            /// <summary>
            /// ARGB32 - DDS_Standard_A8R8G8B8
            /// </summary>
            ARGB32 = 0x01,
            /// <summary>
            /// RGB24 - DDS_Standard_R8G8B8
            /// </summary>
            RGB24 = 0x02,
            /// <summary>
            /// GA16 - DDS_Standard_D16_UNORM
            /// </summary>
            GA16 = 0x03,
            /// <summary>
            /// A8 - DDS_Standard_A8_UNORM
            /// </summary>
            A8 = 0x04,
            /// <summary>
            /// A8b - DDS_Standard_A8_UNORM
            /// </summary>
            A8b = 0x05,
            /// <summary>
            /// JPG
            /// </summary>
            JPG = 0x07,
            /// <summary>
            /// DXT1 - DDS_BC1_UNORM;
            /// </summary>
            DXT1 = 0x0B,
            /// <summary>
            /// DXT3 - DDS_BC2_UNORM
            /// </summary>
            DXT2 = 0x0C,
            /// <summary>
            /// DXT5 - DDS_BC3_UNORM
            /// </summary>
            DXT3 = 0x0D,
            /// <summary>
            /// DXT5 - DDS_BC5_UNORM
            /// </summary>
            DXT5 = 0x0E,
        }

        [Flags]
        public enum FLAGS : byte
        {
            NOPICMIP = 1 << 0,
            /// <summary>
            /// NOMIPMAPS
            /// </summary>
            NOMIPMAPS = 1 << 1,
            /// <summary>
            /// CUBEMAP
            /// </summary>
            CUBEMAP = 1 << 2,
            /// <summary>
            /// VOLMAP
            /// </summary>
            VOLMAP = 1 << 3,
            /// <summary>
            /// STREAMING
            /// </summary>
            STREAMING = 1 << 4,
            /// <summary>
            /// LEGACY_NORMALS
            /// </summary>
            LEGACY_NORMALS = 1 << 5,
            /// <summary>
            /// CLAMP_U
            /// </summary>
            CLAMP_U = 1 << 6,
            /// <summary>
            /// CLAMP_V
            /// </summary>
            CLAMP_V = 1 << 7,
        }

        [Flags]
        public enum FLAGS_EXT : int
        {
            /// <summary>
            /// DYNAMIC
            /// </summary>
            DYNAMIC = 1 << 16,
            /// <summary>
            /// RENDER_TARGET
            /// </summary>
            RENDER_TARGET = 1 << 17,
            /// <summary>
            /// SYSTEMMEM
            /// </summary>
            SYSTEMMEM = 1 << 18
        }

        /// <summary>
        /// Describes a IWI file header.
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 0x1)]
        public unsafe struct HEADER
        {
            public const int SizeOf = 8;
            /// <summary>
            /// MAGIC (IWi)
            /// </summary>
            public const uint MAGIC = 0x69574900;
            /// <summary>
            /// Format
            /// </summary>
            [MarshalAs(UnmanagedType.U1)] public FORMAT Format;
            /// <summary>
            /// Usage
            /// </summary>
            [MarshalAs(UnmanagedType.U1)] public FLAGS Flags;
            /// <summary>
            /// Width
            /// </summary>
            public ushort Width;
            /// <summary>
            /// Height
            /// </summary>
            public ushort Height;
            /// <summary>
            /// Depth
            /// </summary>
            public ushort Depth;

            /// <summary>
            /// Verifies this instance.
            /// </summary>
            public void Verify()
            {
                if (Width == 0 || Height == 0) throw new FormatException($"Invalid DDS file header");
                if (Format >= DXT1 && Format <= DXT5 && Width != MathX.NextPower(Width) && Height != MathX.NextPower(Height)) throw new FormatException($"DXT images must have power-of-2 dimensions..");
                if (Format > DXT5) throw new FormatException($"Unknown Format: {Format}");
            }

            public static byte[] Read(BinaryReader r, out HEADER header, out Range[] ranges, out (FORMAT type, object gl, object vulken, object unity, object unreal) format)
            {
                var magic = r.ReadUInt32();
                var version = (VERSION)(magic >> 24);
                magic <<= 8;
                if (magic != HEADER.MAGIC) throw new FormatException($"Invalid IWI file magic: {magic}.");
                if (version == VERSION.CODMW2) r.Seek(8);
                header = r.ReadT<HEADER>(SizeOf);
                header.Verify();

                // read mips offsets
                r.Seek(version switch
                {
                    VERSION.COD2 => 0xC,
                    VERSION.COD4 => 0xC,
                    VERSION.CODMW2 => 0x10,
                    VERSION.CODBO1 => 0x10,
                    VERSION.CODBO2 => 0x20,
                    _ => throw new FormatException($"Invalid IWI Version: {version}."),
                });

                var mips = r.ReadTArray<int>(sizeof(int), version < VERSION.CODBO1 ? 4 : 8);
                var mipsLength = mips[0] == mips[1] || mips[0] == mips[^1] ? 1 : mips.Length - 1;
                var mipsBase = mipsLength == 1 ? (int)r.Position() : mips[^1];
                var size = (int)(r.BaseStream.Length - mipsBase);
                ranges = mipsLength > 1
                    ? Enumerable.Range(0, mipsLength).Select(i => new Range(mips[i + 1] - mipsBase, mips[i] - mipsBase)).ToArray()
                    : new[] { new Range(0, size) };
                r.Seek(mipsBase);
                format = header.Format switch
                {
                    ARGB32 => (ARGB32, (TextureGLFormat.Rgba, TextureGLPixelFormat.Rgb, TextureGLPixelType.UnsignedInt8888Reversed), (TextureGLFormat.Rgba, TextureGLPixelFormat.Rgb, TextureGLPixelType.UnsignedInt8888Reversed), TextureUnityFormat.RGBA32, TextureUnityFormat.RGBA32),
                    RGB24 => (RGB24, TextureGLFormat.Rgb, TextureGLFormat.Rgb, TextureUnityFormat.Unknown, TextureUnityFormat.Unknown),
                    DXT1 => (DXT1, TextureGLFormat.CompressedRgbaS3tcDxt1Ext, TextureGLFormat.CompressedRgbaS3tcDxt1Ext, TextureUnityFormat.DXT1, TextureUnityFormat.DXT1),
                    DXT2 => (DXT2, TextureGLFormat.CompressedRgbaS3tcDxt3Ext, TextureGLFormat.CompressedRgbaS3tcDxt3Ext, TextureUnityFormat.Unknown, TextureUnityFormat.Unknown),
                    DXT3 => (DXT3, TextureGLFormat.CompressedRgbaS3tcDxt3Ext, TextureGLFormat.CompressedRgbaS3tcDxt3Ext, TextureUnityFormat.Unknown, TextureUnityFormat.Unknown),
                    DXT5 => (DXT5, TextureGLFormat.CompressedRgbaS3tcDxt5Ext, TextureGLFormat.CompressedRgbaS3tcDxt5Ext, TextureUnityFormat.DXT5, TextureUnityFormat.DXT5),
                    _ => throw new ArgumentOutOfRangeException(nameof(Header.Format), $"{header.Format}"),
                };
                return r.ReadBytes(size);
            }
        }

        public BinaryIwi(BinaryReader r)
        {
            Bytes = HEADER.Read(r, out Header, out Mips, out Format);
        }

        HEADER Header;
        Range[] Mips;
        byte[] Bytes;
        (FORMAT type, object gl, object vulken, object unity, object unreal) Format;

        public IDictionary<string, object> Data => null;
        public int Width => Header.Width;
        public int Height => Header.Height;
        public int Depth => 0;
        public int NumMipMaps => Mips.Length;
        public TextureFlags Flags => (Header.Flags & FLAGS.CUBEMAP) != 0 ? TextureFlags.CUBE_TEXTURE : 0;

        public byte[] Begin(int platform, out object format, out Range[] ranges, out bool forward)
        {
            format = (FamilyPlatform.Type)platform switch
            {
                FamilyPlatform.Type.OpenGL => Format.gl,
                FamilyPlatform.Type.Unity => Format.unity,
                FamilyPlatform.Type.Unreal => Format.unreal,
                FamilyPlatform.Type.Vulken => Format.vulken,
                FamilyPlatform.Type.StereoKit => throw new NotImplementedException("StereoKit"),
                _ => throw new ArgumentOutOfRangeException(nameof(platform), $"{platform}"),
            };
            ranges = Mips;
            forward = true;
            return Bytes;
        }
        public void End() { }

        List<MetadataInfo> IGetMetadataInfo.GetInfoNodes(MetadataManager resource, FileMetadata file, object tag) => new List<MetadataInfo> {
            new MetadataInfo(null, new MetadataContent { Type = "Texture", Name = Path.GetFileName(file.Path), Value = this }),
            new MetadataInfo("Texture", items: new List<MetadataInfo> {
                new MetadataInfo($"Format: {Format.type}"),
                new MetadataInfo($"Width: {Header.Width}"),
                new MetadataInfo($"Height: {Header.Height}"),
                new MetadataInfo($"Mipmaps: {Mips.Length}"),
            }),
        };
    }
}
