﻿using GameSpec.Formats;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GameSpec.Metadata
{
    public static class StandardMetadataItem
    {
        /// <summary>
        /// Gets the pak files asynchronous.
        /// </summary>
        /// <param name="manager">The manager.</param>
        /// <param name="pakFile">The pak file.</param>
        /// <returns></returns>
        public static async Task<List<MetadataItem>> GetPakFilesAsync(MetadataManager manager, BinaryPakFile pakFile)
        {
            var pakMultiFile = pakFile as BinaryPakManyFile;
            var root = new List<MetadataItem>();
            string currentPath = null;
            List<MetadataItem> currentFolder = null;
            if (pakMultiFile.Files != null)
                foreach (var file in pakMultiFile.Files.OrderBy(x => x.Path))
                {
                    // skip empty
                    if (string.IsNullOrEmpty(file.Path)) continue;

                    // folder
                    var fileFolder = Path.GetDirectoryName(file.Path);
                    if (currentPath != fileFolder)
                    {
                        currentPath = fileFolder;
                        currentFolder = root;
                        if (!string.IsNullOrEmpty(fileFolder))
                            foreach (var folder in fileFolder.Split('\\'))
                            {
                                var found = currentFolder.Find(x => x.Name == folder && x.PakFile == null);
                                if (found != null) currentFolder = found.Items;
                                else { found = new MetadataItem(file, folder, manager.FolderIcon); currentFolder.Add(found); currentFolder = found.Items; }
                            }
                    }

                    // extract pak
                    if (file.Pak != null)
                    {
                        var children = await GetPakFilesAsync(manager, file.Pak);
                        currentFolder.Add(new MetadataItem(file, Path.GetFileName(file.Path), manager.PackageIcon, children: children) { PakFile = pakFile });
                        continue;
                    }

                    // file
                    var fileName = Path.GetFileName(file.Path);
                    var fileNameForIcon = pakFile.FileMask?.Invoke(fileName) ?? fileName;
                    var extentionForIcon = Path.GetExtension(fileNameForIcon);
                    if (extentionForIcon.Length > 0) extentionForIcon = extentionForIcon.Substring(1);
                    currentFolder.Add(new MetadataItem(file, fileName, manager.GetIcon(extentionForIcon)) { PakFile = pakFile });
                }
            return root;
        }
    }
}