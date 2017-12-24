using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using OpenTK.Graphics.OpenGL4;

namespace dotnet_opentk_tutorial
{
    public class ShaderProgram : IDisposable
    {
        public readonly int Id;

        public ShaderProgram(Dictionary<ShaderType, string> shaders)
        {
            Id = GL.CreateProgram();

            var shaderComponentIds = shaders.Select(s =>
            {
                var shaderId = GL.CreateShader(s.Key);
                var src = File.ReadAllText(s.Value);

                GL.ShaderSource(shaderId, src);
                GL.CompileShader(shaderId);

                var shaderInfo = GL.GetShaderInfoLog(shaderId);
                if (!string.IsNullOrEmpty(shaderInfo))
                {
                    Console.WriteLine($"GL.CompileShader [{s.Key}] had info log: {shaderInfo}");
                }

                GL.AttachShader(Id, shaderId);
                return shaderId;
            }).ToArray(); // Need to force linq to iterate over the collection so we actually create shaders...
            
            GL.LinkProgram(Id);
            var programInfo = GL.GetProgramInfoLog(Id);
            if (!string.IsNullOrEmpty(programInfo))
            {
                Console.WriteLine($"GL.LinkProgram had info log {programInfo}");
            }

            foreach (var shaderId in shaderComponentIds)
            {
                GL.DetachShader(Id, shaderId);
                GL.DeleteShader(shaderId);
            }
        }

        public void Bind() => GL.UseProgram(Id);

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                GL.DeleteProgram(Id);
            }
        }
    }
}