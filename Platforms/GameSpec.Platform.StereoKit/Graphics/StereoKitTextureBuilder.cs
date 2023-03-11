using OpenStack.Graphics;
using StereoKit;
using System;

namespace GameSpec.Graphics
{
    public class StereoKitTextureBuilder : AbstractTextureBuilder<Tex>
    {
        public void Release()
        {
        }

        Tex _defaultTexture;
        public override Tex DefaultTexture => _defaultTexture != null ? _defaultTexture : _defaultTexture = BuildAutoTexture();

        Tex BuildAutoTexture() => BuildSolidTexture(4, 4, new[]
        {
            0.9f, 0.2f, 0.8f, 1f,
            0f, 0.9f, 0f, 1f,
            0.9f, 0.2f, 0.8f, 1f,
            0f, 0.9f, 0f, 1f,

            0f, 0.9f, 0f, 1f,
            0.9f, 0.2f, 0.8f, 1f,
            0f, 0.9f, 0f, 1f,
            0.9f, 0.2f, 0.8f, 1f,

            0.9f, 0.2f, 0.8f, 1f,
            0f, 0.9f, 0f, 1f,
            0.9f, 0.2f, 0.8f, 1f,
            0f, 0.9f, 0f, 1f,

            0f, 0.9f, 0f, 1f,
            0.9f, 0.2f, 0.8f, 1f,
            0f, 0.9f, 0f, 1f,
            0.9f, 0.2f, 0.8f, 1f,
        });

        public override Tex BuildTexture(ITexture info)
        {
            return default;
        }

        public override Tex BuildSolidTexture(int width, int height, float[] rgba)
        {
            return default;
        }

        public override Tex BuildNormalMap(Tex source, float strength) => throw new NotImplementedException();
    }
}