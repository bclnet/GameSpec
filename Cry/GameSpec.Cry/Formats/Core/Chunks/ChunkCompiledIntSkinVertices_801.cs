﻿using System.IO;
using System.Linq;

namespace GameSpec.Cry.Formats.Core.Chunks
{
    public unsafe class ChunkCompiledIntSkinVertices_801 : ChunkCompiledIntSkinVertices
    {
        public override void Read(BinaryReader r)
        {
            base.Read(r);
            SkipBytes(r, 32); // Padding between the chunk header and the first IntVertex.

            // Size of the IntSkinVertex is 40 bytes
            NumIntVertices = (int)((Size - 32) / 40);
            IntSkinVertices = new IntSkinVertex[NumIntVertices];
            for (var i = 0; i < NumIntVertices; i++)
            {
                IntSkinVertices[i].Position = r.ReadVector3();
                IntSkinVertices[i].BoneIDs = r.ReadTArray<ushort>(sizeof(ushort), 4); // Read 4 bone IDs
                IntSkinVertices[i].Weights = r.ReadTArray<float>(sizeof(float), 4); // Read the weights for those bone IDs
                IntSkinVertices[i].Color.value = r.ReadInt32(); // Read the color
            }

            // Add to SkinningInfo
            var skin = GetSkinningInfo();
            skin.IntVertices = IntSkinVertices.ToList();
        }
    }
}