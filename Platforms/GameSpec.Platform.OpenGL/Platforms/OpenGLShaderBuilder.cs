using OpenStack.Graphics.OpenGL;
using OpenStack.Graphics.Renderer1;
using System.Collections.Generic;

namespace GameSpec.Platforms
{
    public class OpenGLShaderBuilder : ShaderBuilderBase<Shader>
    {
        static readonly ShaderLoader _loader = new ShaderDebugLoader();

        public override Shader BuildShader(string path, IDictionary<string, bool> args) => _loader.LoadShader(path, args);
        public override Shader BuildPlaneShader(string path, IDictionary<string, bool> args) => _loader.LoadPlaneShader(path, args);
    }
}