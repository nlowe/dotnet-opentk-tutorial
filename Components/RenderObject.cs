using System;
using System.Collections.Generic;
using System.Linq;
using OpenTK.Graphics.OpenGL4;

namespace dotnet_opentk_tutorial.Components
{
    public class RenderObject : IDisposable
    {
        private bool _initialized;
        private readonly int _vao;
        private readonly int _vbo;
        private readonly int _vertexCount;

        public RenderObject(IEnumerable<Vertex> vertices)
        {
            var vs = vertices.ToArray();
            
            _vertexCount = vs.Length;
            _vao = GL.GenVertexArray();
            _vbo = GL.GenBuffer();

            GL.BindVertexArray(_vao);
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vao);
            
            GL.NamedBufferStorage(
                _vbo,
                Vertex.Size * vs.Length, // Size of buffer
                vs,                      // Buffer data
                BufferStorageFlags.MapWriteBit // Write-Only buffer
            );
            
            GL.VertexArrayAttribBinding(_vao, 0, 0);
            GL.EnableVertexArrayAttrib(_vao, 0);
            GL.VertexArrayAttribFormat(
                _vao,
                0,                      // Attribute index
                4,                      // Number of elements in attribute
                VertexAttribType.Float, // Type of attribute
                false,                  // Normalize?
                0                       // Relative offset from first item
            );
            
            GL.VertexArrayAttribBinding(_vao, 1, 0);
            GL.EnableVertexArrayAttrib(_vao, 1);
            GL.VertexArrayAttribFormat(
                _vao,
                1,                      // Attribute index
                4,                      // Number of elements in attribute
                VertexAttribType.Float, // Type of Attribute
                false,                  // Normalize?
                sizeof(float) * 4       // Relative offset from first item
            );
            
            // Link vao to vbo
            GL.VertexArrayVertexBuffer(_vao, 0, _vbo, IntPtr.Zero, Vertex.Size);

            _initialized = true;
        }

        public void Render()
        {
            GL.DrawArrays(PrimitiveType.Triangles, 0, _vertexCount);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing) return;
            if (!_initialized) return;
            
            GL.DeleteVertexArray(_vao);
            GL.DeleteBuffer(_vbo);
            _initialized = false;
        }

        public void Bind()
        {
            GL.BindVertexArray(_vao);
        }
    }
}