using GameSpec.WbB.Formats.Props;
using GameSpec.Metadata;
using GameSpec.Formats;
using System.Collections.Generic;
using System.IO;

namespace GameSpec.WbB.Formats.Entity
{
    public class BspTree : IHaveMetaInfo
    {
        public readonly BspNode RootNode;

        public BspTree(BinaryReader r, BSPType treeType)
            => RootNode = BspNode.Factory(r, treeType);

        //: Entity.BSPTree
        List<MetaInfo> IHaveMetaInfo.GetInfoNodes(MetaManager resource, FileSource file, object tag)
        {
            var nodes = new List<MetaInfo> {
                new MetaInfo($"Root", items: (RootNode as IHaveMetaInfo).GetInfoNodes(tag: tag)),
            };
            return nodes;
        }
    }
}
