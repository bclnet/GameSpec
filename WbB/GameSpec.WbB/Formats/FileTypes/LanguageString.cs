using GameSpec.Metadata;
using GameSpec.Formats;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GameSpec.WbB.Formats.FileTypes
{
    /// <summary>
    /// These are client_portal.dat files starting with 0x31.
    /// This is called a "String" in the client; It has been renamed to avoid conflicts with the generic "String" class.
    /// </summary>
    [PakFileType(PakFileType.String)]
    public class LanguageString : FileType, IGetMetadataInfo
    {
        public string CharBuffer;

        public LanguageString(BinaryReader r)
        {
            Id = r.ReadUInt32();
            CharBuffer = r.ReadC32Encoding(Encoding.Default); //:TODO ?FALLBACK
        }

        //: New
        List<MetadataInfo> IGetMetadataInfo.GetInfoNodes(MetadataManager resource, FileSource file, object tag)
        {
            var nodes = new List<MetadataInfo> {
                new MetadataInfo($"{nameof(LanguageString)}: {Id:X8}", items: new List<MetadataInfo> {
                })
            };
            return nodes;
        }
    }
}
