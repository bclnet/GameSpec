using OpenStack.Graphics;
using System;
using UnityEngine;
using static System.Net.Mime.MediaTypeNames;

namespace GameSpec.Graphics
{
    public class UnityTextureBuilder : AbstractTextureBuilder<Texture2D>
    {
        Texture2D _defaultTexture;
        public override Texture2D DefaultTexture => _defaultTexture ??= BuildAutoTexture();

        Texture2D BuildAutoTexture() => new Texture2D(4, 4);

        public override Texture2D BuildTexture(ITexture info, Range? range = null)
        {
            Texture2D tex;
            var bytes = info.Begin((int)FamilyPlatform.Type.Unity, out var format, out _, out _);
            if (format is TextureUnityFormat unityFormat)
            {
                var textureFormat = (TextureFormat)unityFormat;
                tex = new Texture2D(info.Width, info.Height, textureFormat, info.NumMipMaps, false);
                tex.LoadRawTextureData(bytes);
                tex.Apply();
                tex.Compress(true);
            }
            else if (format is ValueTuple<TextureUnityFormat> unityPixelFormat)
            {
                var textureFormat = (TextureFormat)unityPixelFormat.Item1;
                tex = new Texture2D(info.Width, info.Height, textureFormat, info.NumMipMaps, false);
            }
            else throw new NotImplementedException();

            return tex;
        }

        public override Texture2D BuildSolidTexture(int width, int height, float[] rgba) => throw new NotImplementedException();

        public override Texture2D BuildNormalMap(Texture2D source, float strength)
        {
            strength = Mathf.Clamp(strength, 0.0F, 1.0F);
            float xLeft, xRight, yUp, yDown, yDelta, xDelta;
            var normalTexture = new Texture2D(source.width, source.height, TextureFormat.ARGB32, true);
            for (var y = 0; y < normalTexture.height; y++)
                for (var x = 0; x < normalTexture.width; x++)
                {
                    xLeft = source.GetPixel(x - 1, y).grayscale * strength;
                    xRight = source.GetPixel(x + 1, y).grayscale * strength;
                    yUp = source.GetPixel(x, y - 1).grayscale * strength;
                    yDown = source.GetPixel(x, y + 1).grayscale * strength;
                    xDelta = (xLeft - xRight + 1) * 0.5f;
                    yDelta = (yUp - yDown + 1) * 0.5f;
                    normalTexture.SetPixel(x, y, new UnityEngine.Color(xDelta, yDelta, 1.0f, yDelta));
                }
            normalTexture.Apply();
            return normalTexture;
        }

        public override void DeleteTexture(Texture2D id) => UnityEngine.Object.Destroy(id);
    }
}