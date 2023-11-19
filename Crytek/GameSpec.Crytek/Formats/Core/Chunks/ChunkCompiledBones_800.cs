﻿using GameSpec.Cry.Formats.Models;
using System.IO;

namespace GameSpec.Cry.Formats.Core.Chunks
{
    public class ChunkCompiledBones_800 : ChunkCompiledBones
    {
        public override void Read(BinaryReader r)
        {
            base.Read(r);
            SkipBytes(r, 32); // Padding between the chunk header and the first bone.

            // Read the first bone with ReadCompiledBone, then recursively grab all the children for each bone you find.
            // Each bone structure is 584 bytes, so will need to seek childOffset * 584 each time, and go back.
            NumBones = (int)((Size - 32) / 584);
            for (var i = 0; i < NumBones; i++)
            {
                var bone = new CompiledBone();
                bone.ReadCompiledBone_800(r);
                // First bone read is root bone
                if (RootBone == null) RootBone = bone;
                if (bone.offsetParent != 0) bone.ParentBone = BoneList[i + bone.offsetParent];
                bone.parentID = bone.ParentBone != null ? bone.ParentBone.ControllerID : 0;
                BoneList.Add(bone);
            }
            // Add the ChildID to the parent bone. This will help with navigation. Also set up the TransformSoFar
            foreach (var bone in BoneList) AddChildIDToParent(bone);

            // Add to SkinningInfo
            var skin = GetSkinningInfo();
            skin.HasSkinningInfo = true;
            skin.CompiledBones = BoneList;
        }
    }
}
