﻿using GameSpec.Formats;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace GameSpec.Lucas.Formats
{
    public unsafe class PakBinary_XX : PakBinary<PakBinary_XX>
    {
        public override Task Read(BinaryPakFile source, BinaryReader r, object tag)
        {
            var files = source.Files = new List<FileSource>();

            return Task.CompletedTask;
        }

        public override Task<Stream> ReadData(BinaryPakFile source, BinaryReader r, FileSource file, FileOption option = default)
        {
            throw new NotImplementedException();
        }
    }
}