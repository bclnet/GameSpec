﻿using GameSpec.Cig.Apps.DataForge;
using GameSpec.Metadata;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace GameSpec.Cig.Apps
{
    /// <summary>
    /// DataForgeApp
    /// </summary>
    /// <seealso cref="FamilyApp" />
    public class DataForgeApp : FamilyApp
    {
        public readonly Database Db = new Database();

        public DataForgeApp(Family family, string id, JsonElement elem) : base(family, id, elem) { }

        public override async Task OpenAsync(Type explorerType, MetadataManager manager)
        {
            await Db.OpenAsync(manager);
            await base.OpenAsync(explorerType, manager);
        }
    }
}