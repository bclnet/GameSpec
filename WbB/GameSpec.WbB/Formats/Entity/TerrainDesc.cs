using GameSpec.Meta;
using GameSpec.Formats;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GameSpec.WbB.Formats.Entity
{
    public class TerrainDesc : IHaveMetaInfo
    {
        public readonly TerrainType[] TerrainTypes;
        public readonly LandSurf LandSurfaces;

        public TerrainDesc(BinaryReader r)
        {
            TerrainTypes = r.ReadL32FArray(x => new TerrainType(x));
            LandSurfaces = new LandSurf(r);
        }

        //: Entity.TerrainDesc
        List<MetaInfo> IHaveMetaInfo.GetInfoNodes(MetaManager resource, FileSource file, object tag)
        {
            var nodes = new List<MetaInfo> {
                new MetaInfo("TerrainTypes", items: TerrainTypes.Select(x => {
                    var items = (x as IHaveMetaInfo).GetInfoNodes();
                    var name = items[0].Name.Replace("TerrainName: ", "");
                    items.RemoveAt(0);
                    return new MetaInfo(name, items: items);
                })),
                new MetaInfo($"LandSurf", items: (LandSurfaces as IHaveMetaInfo).GetInfoNodes()),
            };
            return nodes;
        }
    }
}
