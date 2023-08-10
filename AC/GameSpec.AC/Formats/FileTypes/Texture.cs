using GameSpec.AC.Formats.Props;
using GameSpec.Formats;
using GameSpec.Metadata;
using OpenStack.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using static GameSpec.AC.Formats.Props.SurfacePixelFormat;

namespace GameSpec.AC.Formats.FileTypes
{
    [PakFileType(PakFileType.Texture)]
    public unsafe class Texture : FileType, IGetMetadataInfo, ITexture
    {
        public readonly int Unknown;
        public readonly SurfacePixelFormat PixFormat;
        public readonly int Length;
        public readonly byte[] SourceData;
        public readonly uint[] Palette;

        public Texture(BinaryReader r, FamilyGame game)
        {
            Id = r.ReadUInt32();
            Unknown = r.ReadInt32();
            Width = r.ReadInt32();
            Height = r.ReadInt32();
            PixFormat = (SurfacePixelFormat)r.ReadUInt32();
            Length = r.ReadInt32();
            SourceData = r.ReadBytes(Length);
            var hasPalette = PixFormat == PFID_INDEX16 || PixFormat == PFID_P8;
            if (hasPalette) game.Ensure();
            Palette = hasPalette ? DatabaseManager.Portal.GetFile<Palette>(r.ReadUInt32()).Colors : null;
            if (PixFormat == PFID_CUSTOM_RAW_JPEG)
            {
                using var image = new Bitmap(new MemoryStream(SourceData));
                Width = image.Width;
                Height = image.Height;
            }
            Format = PixFormat switch
            {
                PFID_DXT1 => (PFID_DXT1, TextureGLFormat.CompressedRgbaS3tcDxt1Ext, TextureGLFormat.CompressedRgbaS3tcDxt1Ext, TextureUnityFormat.DXT1, TextureUnityFormat.DXT1),
                PFID_DXT3 => (PFID_DXT3, TextureGLFormat.CompressedRgbaS3tcDxt3Ext, TextureGLFormat.CompressedRgbaS3tcDxt3Ext, TextureUnityFormat.Unknown, TextureUnityFormat.Unknown),
                PFID_DXT5 => (PFID_DXT5, TextureGLFormat.CompressedRgbaS3tcDxt5Ext, TextureGLFormat.CompressedRgbaS3tcDxt5Ext, TextureUnityFormat.DXT5, TextureUnityFormat.DXT5),
                var x when x == PFID_CUSTOM_RAW_JPEG ||
                x == PFID_R8G8B8 ||
                x == PFID_CUSTOM_LSCAPE_R8G8B8 ||
                x == PFID_A8 ||
                x == PFID_CUSTOM_LSCAPE_ALPHA ||
                x == PFID_R5G6B5 => (x, (TextureGLFormat.Rgb8, TextureGLPixelFormat.Rgb, TextureGLPixelType.UnsignedByte), (TextureGLFormat.Rgb8, TextureGLPixelFormat.Rgb, TextureGLPixelType.UnsignedByte), TextureUnityFormat.RGB24, TextureUnityFormat.RGB24),
                var x when x == PFID_INDEX16 ||
                x == PFID_P8 ||
                x == PFID_A8R8G8B8 ||
                x == PFID_A4R4G4B4 => (x, (TextureGLFormat.Rgba8, TextureGLPixelFormat.Rgba, TextureGLPixelType.UnsignedByte), (TextureGLFormat.Rgba8, TextureGLPixelFormat.Rgba, TextureGLPixelType.UnsignedByte), TextureUnityFormat.RGBA32, TextureUnityFormat.RGBA32),
                _ => throw new ArgumentOutOfRangeException(nameof(Format), $"{Format}"),
            };
        }

        (SurfacePixelFormat type, object gl, object vulken, object unity, object unreal) Format;

        public IDictionary<string, object> Data => null;
        public int Width { get; }
        public int Height { get; }
        public int Depth => 0;
        public int MipMaps => 1;
        public TextureFlags Flags => 0;

        public byte[] Begin(int platform, out object format, out Range[] mips)
        {
            byte[] Expand()
            {
                // https://www.hanselman.com/blog/how-do-you-use-systemdrawing-in-net-core
                // https://stackoverflow.com/questions/1563038/fast-work-with-bitmaps-in-c-sharp
                switch (PixFormat)
                {
                    case PFID_CUSTOM_RAW_JPEG:
                        {
                            var d = new byte[Width * Height * 3];
                            using var image = new Bitmap(new MemoryStream(SourceData));
                            var data = image.LockBits(new Rectangle(0, 0, image.Width, image.Height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
                            var s = (byte*)data.Scan0.ToPointer();
                            for (var i = 0; i < d.Length; i += 3) { d[i + 0] = s[i + 0]; d[i + 1] = s[i + 1]; d[i + 2] = s[i + 2]; }
                            image.UnlockBits(data);
                            return d;
                        }
                    case PFID_DXT1:
                    case PFID_DXT3:
                    case PFID_DXT5: return SourceData;
                    case PFID_R8G8B8: // RGB
                    case PFID_CUSTOM_LSCAPE_R8G8B8: return SourceData;
                    //case PFID_CUSTOM_LSCAPE_R8G8B8:
                    //    {
                    //        var d = new byte[Width * Height * 3];
                    //        var s = SourceData;
                    //        for (int i = 0; i < d.Length; i += 3) { d[i + 0] = s[i + 2]; d[i + 1] = s[i + 1]; d[i + 2] = s[i + 0]; }
                    //        return d;
                    //    }
                    case PFID_A8R8G8B8: // ARGB format. Most UI textures fall into this category
                        {
                            var d = new byte[Width * Height * 4];
                            var s = SourceData;
                            for (var i = 0; i < d.Length; i += 4) { d[i + 0] = s[i + 1]; d[i + 1] = s[i + 2]; d[i + 2] = s[i + 3]; d[i + 3] = s[i + 0]; }
                            return d;
                        }
                    case PFID_A8: // Greyscale, also known as Cairo A8.
                    case PFID_CUSTOM_LSCAPE_ALPHA:
                        {
                            var d = new byte[Width * Height * 3];
                            var s = SourceData;
                            for (int i = 0, j = 0; i < d.Length; i += 3, j++) { d[i + 0] = s[j]; d[i + 1] = s[j]; d[i + 2] = s[j]; }
                            return d;
                        }
                    case PFID_R5G6B5: // 16-bit RGB
                        {
                            var d = new byte[Width * Height * 3];
                            fixed (byte* _ = SourceData)
                            {
                                var s = (ushort*)_;
                                for (int i = 0, j = 0; i < d.Length; i += 4, j++)
                                {
                                    var val = s[j];
                                    d[i + 0] = (byte)((val >> 8 & 0xF) / 0xF * 255);
                                    d[i + 1] = (byte)((val >> 4 & 0xF) / 0xF * 255);
                                    d[i + 2] = (byte)((val & 0xF) / 0xF * 255);
                                }
                            }
                            return d;
                        }
                    case PFID_A4R4G4B4:
                        {
                            var d = new byte[Width * Height * 4];
                            fixed (byte* s_ = SourceData)
                            {
                                var s = (ushort*)s_;
                                for (int i = 0, j = 0; i < d.Length; i += 4, j++)
                                {
                                    var val = s[j];
                                    d[i + 0] = (byte)(((val & 0xF800) >> 11) << 3);
                                    d[i + 1] = (byte)(((val & 0x7E0) >> 5) << 2);
                                    d[i + 2] = (byte)((val & 0x1F) << 3);
                                }
                            }
                            return d;
                        }
                    case PFID_INDEX16: // 16-bit indexed colors. Index references position in a palette;
                        {
                            var p = Palette;
                            var d = new byte[Width * Height * 4];
                            fixed (byte* s_ = SourceData)
                            fixed (byte* d_ = d)
                            {
                                var s = (ushort*)s_;
                                var d2 = (uint*)d_;
                                for (var i = 0; i < d.Length >> 2; i++) d2[i] = p[s[i]];
                            }
                            return d;
                        }
                    case PFID_P8: // Indexed
                        {
                            var p = Palette;
                            var d = new byte[Width * Height * 4];
                            var s = SourceData;
                            fixed (byte* d_ = d)
                            {
                                var d2 = (uint*)d_;
                                for (var i = 0; i < d.Length >> 2; i++) d2[i] = p[s[i]];
                            }
                            return d;
                        }
                    default: Console.WriteLine($"Unhandled SurfacePixelFormat ({Format}) in RenderSurface {Id:X8}"); return null;
                }
            }

            format = (FamilyPlatform.Type)platform switch
            {
                FamilyPlatform.Type.OpenGL => Format.gl,
                FamilyPlatform.Type.Unity => Format.unity,
                FamilyPlatform.Type.Unreal => Format.unreal,
                FamilyPlatform.Type.Vulken => Format.vulken,
                FamilyPlatform.Type.StereoKit => throw new NotImplementedException("StereoKit"),
                _ => throw new ArgumentOutOfRangeException(nameof(platform), $"{platform}"),
            };
            var bytes = Expand();
            mips = new[] { Range.All };
            return bytes;
        }
        public void End() { }

        List<MetadataInfo> IGetMetadataInfo.GetInfoNodes(MetadataManager resource, FileMetadata file, object tag)
        {
            var nodes = new List<MetadataInfo> {
                //new MetadataInfo(null, new MetadataContent { Type = "Text", Name = Path.GetFileName(file.Path), Value = "PICTURE" }),
                new MetadataInfo(null, new MetadataContent { Type = "Texture", Name = Path.GetFileName(file.Path), Value = this }),
                new MetadataInfo($"{nameof(Texture)}: {Id:X8}", items: new List<MetadataInfo> {
                    new MetadataInfo($"Unknown: {Unknown}"),
                    new MetadataInfo($"Format: {Format.type}"),
                    new MetadataInfo($"Width: {Width}"),
                    new MetadataInfo($"Height: {Height}"),
                    new MetadataInfo($"Type: {Format}"),
                    new MetadataInfo($"Size: {Length} bytes"),
                })
            };
            return nodes;
        }
    }
}
