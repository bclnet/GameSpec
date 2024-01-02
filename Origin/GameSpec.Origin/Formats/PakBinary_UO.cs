﻿using GameSpec.Formats;
using System;
using System.IO;
using System.Threading.Tasks;

namespace GameSpec.Origin.Formats
{
    public unsafe class PakBinary_UO : PakBinary
    {
        public static readonly PakBinary Instance = new PakBinary_UO();

        public override Task ReadAsync(BinaryPakFile source, BinaryReader r, object tag)
        {
            return Task.CompletedTask;
        }

        public override Task<Stream> ReadDataAsync(BinaryPakFile source, BinaryReader r, FileSource file, FileOption option = default)
        {
            r.Seek(file.Position);
            return Task.FromResult((Stream)new MemoryStream(r.ReadBytes((int)file.FileSize)));
        }
    }
}