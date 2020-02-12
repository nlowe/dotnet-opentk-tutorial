using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using OpenTK.Graphics.OpenGL4;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Advanced;
using SixLabors.ImageSharp.PixelFormats;

namespace dotnet_opentk_tutorial.Rendering
{
    public class TexturedRenderObject : AbstractRenderableBase
    {
        private readonly int _textureId;

        private static int MIN_MIPMAPS = 0;
        private static int MIPMAP_LEVEL = 4;

        public TexturedRenderObject(IReadOnlyCollection<TexturedVertex> vertices, ShaderProgram shaderProgram, string texturePath)
            : base(shaderProgram, vertices.Count)
        {
            GL.NamedBufferStorage(
                VBO,
                TexturedVertex.Size * vertices.Count,
                vertices.ToArray(),
                BufferStorageFlags.MapWriteBit
            );

            GL.VertexArrayAttribBinding(VAO, 0, 0);
            GL.EnableVertexArrayAttrib(VAO, 0);
            GL.VertexArrayAttribFormat(
                VAO,
                0,
                4,
                VertexAttribType.Float,
                false,
                0
            );

            GL.VertexArrayAttribBinding(VAO, 1, 0);
            GL.EnableVertexArrayAttrib(VAO, 1);
            GL.VertexArrayAttribFormat(
                VAO,
                1,
                2,
                VertexAttribType.Float,
                false,
                sizeof(float) * 4
            );

            GL.VertexArrayVertexBuffer(VAO, 0, VBO, IntPtr.Zero, TexturedVertex.Size);

            _textureId = InitTextures(texturePath);
        }

        private static int InitTextures(string texturePath)
        {
            var data = LoadTexture(texturePath, out var w, out var h);

            GL.CreateTextures(TextureTarget.Texture2D, 1, out int tid);
            GL.TextureStorage2D(
                tid,
                MIPMAP_LEVEL,
                SizedInternalFormat.Rgba32f,
                w,
                h
            );

            GL.BindTexture(TextureTarget.Texture2D, tid);
            GL.TextureSubImage2D(
                tid,
                0,
                0,
                0,
                w,
                h,
                PixelFormat.Rgba,
                PixelType.Float,
                data
            );

            GL.GenerateTextureMipmap(tid);
            GL.TextureParameterI(tid, All.TextureBaseLevel, ref MIN_MIPMAPS);
            GL.TextureParameterI(tid, All.TextureMaxLevel, ref MIPMAP_LEVEL);

            var textureMinFilter = (int)TextureMinFilter.Linear;
            GL.TextureParameterI(tid, All.TextureMinFilter, ref textureMinFilter);

            var textureMagFilter = (int) TextureMagFilter.Linear;
            GL.TextureParameterI(tid, All.TextureMagFilter, ref textureMagFilter);

            return tid;
        }

        private static float[] LoadTexture(string texturePath, out int w, out int h)
        {
            using (var stream = File.OpenRead(texturePath))
            using (var image = Image.Load<Rgba32>(stream))
            {
                w = image.Width;
                h = image.Height;

                var result = new float[4 * w * h];
                var i = 0;
                foreach (var pixel in image.GetPixelSpan())
                {
                    result[i++] = pixel.R / 255f;
                    result[i++] = pixel.G / 255f;
                    result[i++] = pixel.B / 255f;
                    result[i++] = pixel.A / 255f;
                }

                return result;
            }
        }

        public override void Bind()
        {
            base.Bind();
            GL.BindTexture(TextureTarget.Texture2D, _textureId);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                GL.DeleteTexture(_textureId);
            }
            base.Dispose(disposing);
        }
    }
}
