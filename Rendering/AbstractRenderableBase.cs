using System;
using OpenTK.Graphics.OpenGL4;

namespace dotnet_opentk_tutorial.Rendering
{
    public class AbstractRenderableBase : IRenderable
    {
        protected readonly ShaderProgram ShaderProgram;
        protected readonly int VAO;
        protected readonly int VBO;
        protected readonly int VertexCount;
        
        public int ProjectionParameterId => ShaderProgram.ProjectionParameterId;
        public int ModelViewParameterId => ShaderProgram.ModelViewParameterId;

        protected AbstractRenderableBase(ShaderProgram shaderProgram, int vertexCount)
        {
            ShaderProgram = shaderProgram;
            VertexCount = vertexCount;

            VAO = GL.GenVertexArray();
            VBO = GL.GenBuffer();
            
            GL.BindVertexArray(VAO);
            GL.BindBuffer(BufferTarget.ArrayBuffer, VBO);
        }

        public virtual void Bind()
        {
            ShaderProgram.Bind();
            GL.BindVertexArray(VAO);
        }

        public virtual void Render() => GL.DrawArrays(PrimitiveType.Triangles, 0, VertexCount);

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing) return;
            GL.DeleteVertexArray(VAO);
            GL.DeleteBuffer(VBO);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}