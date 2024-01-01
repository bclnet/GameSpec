using GameSpec.Metadata;
using GameSpec.Formats;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GameSpec.WbB.Formats.Entity
{
    public class SceneDesc : IHaveMetaInfo
    {
        public readonly SceneType[] SceneTypes;

        public SceneDesc(BinaryReader r)
            => SceneTypes = r.ReadL32Array(x => new SceneType(x));

        //: Entity.SceneDesc
        List<MetaInfo> IHaveMetaInfo.GetInfoNodes(MetaManager resource, FileSource file, object tag)
        {
            var nodes = new List<MetaInfo> {
                new MetaInfo("SceneTypes", items: SceneTypes.Select((x, i) => new MetaInfo($"{i}", items: (x as IHaveMetaInfo).GetInfoNodes()))),
            };
            return nodes;
        }
    }
}
