﻿using GameSpec.Formats;
using GameSpec.Formats.Unknown;
using GameSpec.Ubisoft.Formats;
using GameSpec.Ubisoft.Transforms;
using GameSpec.Metadata;
using GameSpec.Transforms;
using System;
using System.IO;
using System.Threading.Tasks;

namespace GameSpec.Ubisoft
{
    /// <summary>
    /// UbisoftPakFile
    /// </summary>
    /// <seealso cref="GameSpec.Formats.BinaryPakFile" />
    public class UbisoftPakFile : BinaryPakFile, ITransformFileObject<IUnknownFileModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UbisoftPakFile" /> class.
        /// </summary>
        /// <param name="game">The game.</param>
        /// <param name="fileSystem">The file system.</param>
        /// <param name="filePath">The file path.</param>
        /// <param name="tag">The tag.</param>
        public UbisoftPakFile(FamilyGame game, IFileSystem fileSystem, string filePath, object tag = default) : base(game, fileSystem, filePath, GetPakBinary(game, filePath), tag)
        {
            GetObjectFactoryFactory = FormatExtensions.GetObjectFactoryFactory;
        }

        #region GetPakBinary

        static PakBinary GetPakBinary(FamilyGame game, string filePath)
            => filePath == null || Path.GetExtension(filePath).ToLowerInvariant() != ".zip"
                ? PakBinaryLith.Instance
                : PakBinary_Zip.GetPakBinary(game);

        #endregion

        #region Transforms

        bool ITransformFileObject<IUnknownFileModel>.CanTransformFileObject(PakFile transformTo, object source) => UnknownTransform.CanTransformFileObject(this, transformTo, source);
        Task<IUnknownFileModel> ITransformFileObject<IUnknownFileModel>.TransformFileObjectAsync(PakFile transformTo, object source) => UnknownTransform.TransformFileObjectAsync(this, transformTo, source);

        #endregion
    }
}