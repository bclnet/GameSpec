﻿using GameSpec.Formats;
using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace GameSpec.Bethesda.Formats
{
    public unsafe class PakBinaryBethesdaBsa : PakBinary
    {
        public static readonly PakBinary Instance = new PakBinaryBethesdaBsa();

        // Header : TES3
        #region Header : TES3
        // http://en.uesp.net/wiki/Bethesda3Mod:BSA_File_Format

        // Default header data
        const uint MW_BSAHEADER_FILEID = 0x00000100; // Magic for Morrowind BSA

        [StructLayout(LayoutKind.Sequential, Pack = 0x1)]
        struct MW_Header
        {
            public uint HashOffset;         // Offset of hash table minus header size (12)
            public uint FileCount;          // Number of files in the archive
        }

        [StructLayout(LayoutKind.Sequential, Pack = 0x1)]
        struct MW_HeaderFile
        {
            public uint FileSize;           // File size
            public uint FileOffset;         // File offset relative to data position
            public uint Size => FileSize > 0 ? FileSize & 0x3FFFFFFF : 0; // The size of the file inside the BSA
        }

        #endregion

        // Header : TES4
        #region Header : TES4
        // http://en.uesp.net/wiki/Bethesda4Mod:BSA_File_Format

        // Default header data
        const uint OB_BSAHEADER_FILEID = 0x00415342;    // Magic for Oblivion BSA, the literal string "BSA\0".
        const uint OB_BSAHEADER_VERSION = 0x67;         // Version number of an Oblivion BSA
        const uint F3_BSAHEADER_VERSION = 0x68;         // Version number of a Fallout 3 BSA
        const uint SSE_BSAHEADER_VERSION = 0x69;        // Version number of a Skyrim SE BSA

        // Archive flags
        const ushort OB_BSAARCHIVE_PATHNAMES = 0x0001;  // Whether the BSA has names for paths
        const ushort OB_BSAARCHIVE_FILENAMES = 0x0002;  // Whether the BSA has names for files
        const ushort OB_BSAARCHIVE_COMPRESSFILES = 0x0004; // Whether the files are compressed
        const ushort F3_BSAARCHIVE_PREFIXFULLFILENAMES = 0x0100; // Whether the name is prefixed to the data?

        // File flags
        //const ushort OB_BSAFILE_NIF = 0x0001; // Set when the BSA contains NIF files (Meshes)
        //const ushort OB_BSAFILE_DDS = 0x0002; // Set when the BSA contains DDS files (Textures)
        //const ushort OB_BSAFILE_XML = 0x0004; // Set when the BSA contains XML files (Menus)
        //const ushort OB_BSAFILE_WAV = 0x0008; // Set when the BSA contains WAV files (Sounds)
        //const ushort OB_BSAFILE_MP3 = 0x0010; // Set when the BSA contains MP3 files (Voices)
        //const ushort OB_BSAFILE_TXT = 0x0020; // Set when the BSA contains TXT files (Shaders)
        //const ushort OB_BSAFILE_HTML = 0x0020; // Set when the BSA contains HTML files
        //const ushort OB_BSAFILE_BAT = 0x0020; // Set when the BSA contains BAT files
        //const ushort OB_BSAFILE_SCC = 0x0020; // Set when the BSA contains SCC files
        //const ushort OB_BSAFILE_SPT = 0x0040; // Set when the BSA contains SPT files (Trees)
        //const ushort OB_BSAFILE_TEX = 0x0080; // Set when the BSA contains TEX files
        //const ushort OB_BSAFILE_FNT = 0x0080; // Set when the BSA contains FNT files (Fonts)
        //const ushort OB_BSAFILE_CTL = 0x0100; // Set when the BSA contains CTL files (Miscellaneous)

        // Bitmasks for the size field in the header
        const uint OB_BSAFILE_SIZEMASK = 0x3fffffff; // Bit mask with OB_HeaderFile:SizeFlags to get the compression status
        const uint OB_BSAFILE_SIZECOMPRESS = 0xC0000000; // Bit mask with OB_HeaderFile:SizeFlags to get the compression status

        [StructLayout(LayoutKind.Sequential, Pack = 0x1)]
        struct OB_Header
        {
            public uint Version;            // 04
            public uint FolderRecordOffset; // Offset of beginning of folder records
            public uint ArchiveFlags;       // Archive flags
            public uint FolderCount;        // Total number of folder records (OBBSAFolderInfo)
            public uint FileCount;          // Total number of file records (OBBSAFileInfo)
            public uint FolderNameLength;   // Total length of folder names
            public uint FileNameLength;     // Total length of file names
            public uint FileFlags;          // File flags
        }

        [StructLayout(LayoutKind.Sequential, Pack = 0x1)]
        struct OB_HeaderFolder
        {
            public ulong Hash;              // Hash of the folder name
            public uint FileCount;          // Number of files in folder
            public uint Offset;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 0x1)]
        struct OB_HeaderFolderSSE
        {
            public ulong Hash;              // Hash of the folder name
            public uint FileCount;          // Number of files in folder
            public uint Unk;
            public ulong Offset;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 0x1)]
        struct OB_HeaderFile
        {
            public ulong Hash;              // Hash of the filename
            public uint Size;               // Size of the data, possibly with OB_BSAFILE_SIZECOMPRESS set
            public uint Offset;             // Offset to raw file data
        }

        #endregion

        public override Task ReadAsync(BinaryPakFile source, BinaryReader r, object tag)
        {
            if (!(source is BinaryPakManyFile multiSource)) throw new NotSupportedException();
            FileSource[] files;

            // Fallout 4
            var magic = source.Magic = r.ReadUInt32();
            
            // Oblivion - Skyrim
            if (magic == OB_BSAHEADER_FILEID)
            {
                var header = r.ReadT<OB_Header>(sizeof(OB_Header));
                if (header.Version != OB_BSAHEADER_VERSION && header.Version != F3_BSAHEADER_VERSION && header.Version != SSE_BSAHEADER_VERSION) throw new FormatException("BAD MAGIC");
                if ((header.ArchiveFlags & OB_BSAARCHIVE_PATHNAMES) == 0 || (header.ArchiveFlags & OB_BSAARCHIVE_FILENAMES) == 0) throw new FormatException("HEADER FLAGS");
                source.Version = header.Version;

                // calculate some useful values
                var compressedToggle = (header.ArchiveFlags & OB_BSAARCHIVE_COMPRESSFILES) > 0;
                if (header.Version == F3_BSAHEADER_VERSION || header.Version == SSE_BSAHEADER_VERSION) source.Params["namePrefix"] = (header.ArchiveFlags & F3_BSAARCHIVE_PREFIXFULLFILENAMES) > 0 ? "Y" : "N";

                // read-all folders
                var foldersFiles = header.Version == SSE_BSAHEADER_VERSION
                    ? r.ReadTArray<OB_HeaderFolderSSE>(sizeof(OB_HeaderFolderSSE), (int)header.FolderCount).Select(x => x.FileCount).ToArray()
                    : r.ReadTArray<OB_HeaderFolder>(sizeof(OB_HeaderFolder), (int)header.FolderCount).Select(x => x.FileCount).ToArray();

                // read-all folder files
                var fileIdx = 0U;
                multiSource.Files = files = new FileSource[header.FileCount];
                for (var i = 0; i < header.FolderCount; i++)
                {
                    var folder_name = r.ReadFString(r.ReadByte() - 1).Replace('\\', '/'); r.Skip(1);
                    var headerFiles = r.ReadTArray<OB_HeaderFile>(sizeof(OB_HeaderFile), (int)foldersFiles[i]);
                    foreach (var headerFile in headerFiles)
                    {
                        var compressed = (headerFile.Size & OB_BSAFILE_SIZECOMPRESS) != 0;
                        files[fileIdx++] = new FileSource
                        {
                            Path = folder_name,
                            Position = headerFile.Offset,
                            Compressed = compressed ^ compressedToggle ? 1 : 0,
                            PackedSize = compressed ? headerFile.Size ^ OB_BSAFILE_SIZECOMPRESS : headerFile.Size,
                        };
                    };
                }

                // read-all names
                foreach (var file in files) file.Path = $"{file.Path}/{r.ReadCString()}";
            }
            // Morrowind
            else if (magic == MW_BSAHEADER_FILEID)
            {
                var header = r.ReadT<MW_Header>(sizeof(MW_Header));
                var dataOffset = 12 + header.HashOffset + (8 * header.FileCount);

                // Create file metadatas
                multiSource.Files = files = new FileSource[header.FileCount];
                var headerFiles = r.ReadTArray<MW_HeaderFile>(sizeof(MW_HeaderFile), (int)header.FileCount);
                for (var i = 0; i < headerFiles.Length; i++)
                {
                    var headerFile = headerFiles[i];
                    files[i] = new FileSource
                    {
                        PackedSize = headerFile.Size,
                        Position = dataOffset + headerFile.FileOffset,
                    };
                }

                // Read filename offsets
                var filenameOffsets = r.ReadTArray<uint>(sizeof(uint), (int)header.FileCount); // relative offset in filenames section

                // Read filenames
                var filenamesPosition = r.Position();
                for (var i = 0; i < files.Length; i++)
                {
                    r.Seek(filenamesPosition + filenameOffsets[i]);
                    files[i].Path = r.ReadZAString(1000).Replace('\\', '/');
                }
            }
            else throw new InvalidOperationException("BAD MAGIC");
            return Task.CompletedTask;
        }

        public override Task<Stream> ReadDataAsync(BinaryPakFile source, BinaryReader r, FileSource file, DataOption option = 0, Action<FileSource, string> exception = null)
        {
            Stream fileData = null;
            var magic = source.Magic;
            // BSA
            if (magic == OB_BSAHEADER_FILEID || magic == MW_BSAHEADER_FILEID)
            {
                // position
                var fileSize = (int)(source.Version == SSE_BSAHEADER_VERSION
                    ? file.PackedSize & OB_BSAFILE_SIZEMASK
                    : file.PackedSize);
                r.Seek(file.Position);
                if (source.Params.TryGetValue("namePrefix", out var z2) && z2 == "Y")
                {
                    var prefixLength = r.ReadByte() + 1;
                    if (source.Version == SSE_BSAHEADER_VERSION)
                        fileSize -= prefixLength;
                    r.Seek(file.Position + prefixLength);
                }

                // not compressed
                if (fileSize <= 0 || file.Compressed == 0)
                    fileData = new MemoryStream(r.ReadBytes(fileSize));
                // compressed
                else
                {
                    var newFileSize = (int)r.ReadUInt32(); fileSize -= 4;
                    fileData = source.Version == SSE_BSAHEADER_VERSION
                        ? new MemoryStream(r.DecompressLz4(fileSize, newFileSize))
                        : new MemoryStream(r.DecompressZlib2(fileSize, newFileSize));
                }
            }
            else throw new InvalidOperationException("BAD MAGIC");
            return Task.FromResult(fileData);
        }
    }
}