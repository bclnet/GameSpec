﻿using GameSpec.Formats;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace GameSpec.Origin.Formats
{
    public unsafe class PakBinary_U9 : PakBinary<PakBinary_U9>
    {
        // Headers
        #region Headers
        // http://wiki.ultimacodex.com/wiki/Ultima_IX_Internal_Formats#FLX_Format

        [StructLayout(LayoutKind.Sequential, Pack = 0x1)]
        struct FLX_HeaderFile
        {
            public uint Position;
            public uint FileSize;
        }

        #endregion

        public override Task Read(BinaryPakFile source, BinaryReader r, object tag)
        {
            var fileName = Path.GetFileNameWithoutExtension(source.FilePath).ToLowerInvariant();
            var prefix
                = fileName.Contains("bitmap") ? "bitmap"
                : fileName.Contains("texture") ? "texture"
                : fileName.Contains("sdinfo") ? "sdinfo"
                : fileName;
            r.Seek(0x50);
            var numFiles = r.ReadInt32();
            r.Seek(0x80);
            var headerFiles = r.ReadTArray<FLX_HeaderFile>(sizeof(FLX_HeaderFile), numFiles);
            var files = source.Files = new FileSource[numFiles];
            for (var i = 0; i < files.Count; i++)
            {
                var headerFile = headerFiles[i];
                files[i] = new FileSource
                {
                    Path = $"{prefix}/{i}",
                    FileSize = headerFile.FileSize,
                    Position = headerFile.Position,
                };
            }
            return Task.CompletedTask;
        }

        public override Task<Stream> ReadData(BinaryPakFile source, BinaryReader r, FileSource file, FileOption option = default)
        {
            r.Seek(file.Position);
            return Task.FromResult((Stream)new MemoryStream(r.ReadBytes((int)file.FileSize)));
        }
    }
}