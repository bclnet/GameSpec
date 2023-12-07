using GameSpec.Metadata;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace GameSpec.Formats
{
    public class BinaryTxt : IGetMetadataInfo
    {
        public static Task<object> Factory(BinaryReader r, FileSource f, PakFile s) => Task.FromResult((object)new BinaryTxt(r, (int)f.FileSize));

        public BinaryTxt() { }
        public BinaryTxt(BinaryReader r, int fileSize) => Data = r.ReadEncoding(fileSize);

        public string Data;

        List<MetadataInfo> IGetMetadataInfo.GetInfoNodes(MetadataManager resource, FileSource file, object tag) => new List<MetadataInfo> {
            new MetadataInfo(null, new MetadataContent { Type = "Text", Name = Path.GetFileName(file.Path), Value = Data }),
        };
    }
}
