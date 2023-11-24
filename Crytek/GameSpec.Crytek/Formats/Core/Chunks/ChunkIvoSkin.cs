﻿namespace GameSpec.Crytek.Formats.Core.Chunks
{
    public class ChunkIvoSkin : Chunk
    {
        public GeometryInfo geometryInfo;
        public ChunkMesh meshChunk;
        public ChunkMeshSubsets meshSubsetsChunk;
        public ChunkDataStream indices;
        public ChunkDataStream vertsUvs;
        public ChunkDataStream colors;
        public ChunkDataStream tangents;
    }
}
