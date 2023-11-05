﻿using GameSpec.Formats;
using GameSpec.Valve.Formats.Blocks;
using GameSpec.Valve.Formats.Extras;
using System;
using System.IO;
using System.Threading.Tasks;

namespace GameSpec.Valve.Formats
{
    /// <summary>
    /// FormatExtensions
    /// </summary>
    public static class FormatExtensions
    {
        // object factory
        public static (DataOption, Func<BinaryReader, FileMetadata, PakFile, Task<object>>) GetObjectFactoryFactory(this FileMetadata source, FamilyGame game)
        {
            Task<object> BinaryPakFactory(BinaryReader r, FileMetadata f, PakFile s)
            {
                if (r.BaseStream.Length < 6) return null;
                var input = r.Peek(z => z.ReadBytes(6));
                var magic = BitConverter.ToUInt32(input, 0);
                var magicResourceVersion = BitConverter.ToUInt16(input, 4);
                if (magic == PakBinaryVpk.MAGIC) throw new InvalidOperationException("Pak File");
                else if (magic == CompiledShader.MAGIC) return Task.FromResult((object)new CompiledShader(r, f.Path));
                else if (magic == ClosedCaptions.MAGIC) return Task.FromResult((object)new ClosedCaptions(r));
                else if (magic == ToolsAssetInfo.MAGIC) return Task.FromResult((object)new ToolsAssetInfo(r));
                else if (magic == DATABinaryKV3.MAGIC || magic == DATABinaryKV3.MAGIC2) { var kv3 = new DATABinaryKV3 { Size = (uint)r.BaseStream.Length }; kv3.Read(null, r); return Task.FromResult((object)kv3); }
                else if (magicResourceVersion == BinaryPak.KnownHeaderVersion) return Task.FromResult((object)new BinaryPak(r));
                //else if (magicResourceVersion == BinaryPak.KnownHeaderVersion)
                //{
                //    var pak = new BinaryPak(r);
                //    switch (pak.DataType)
                //    {
                //        //case DATA.DataType.Mesh: return Task.FromResult((object)new DATAMesh(pak));
                //        default: return Task.FromResult((object)pak);
                //    }
                //}
                else return null;
            }
            return Path.GetExtension(source.Path).ToLowerInvariant() switch
            {
                var x when game.Engine == "HL" && (x == ".pic" || x == ".tex" || x == ".fnt") => (0, BinaryWad.Factory),
                var x when game.Engine == "HL" && x == ".spr"  => (0, BinarySpr.Factory),
                _ => (0, BinaryPakFactory),
            };
        }
    }
}