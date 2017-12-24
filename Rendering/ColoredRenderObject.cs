using System;
using System.Collections.Generic;
using System.Linq;
using OpenTK.Graphics.OpenGL4;

namespace dotnet_opentk_tutorial.Rendering
{
    public class ColoredRenderObject : AbstractRenderableBase
    {
        public ColoredRenderObject(IReadOnlyCollection<ColoredVertex> vertices, ShaderProgram shaderProgram) 
            : base(shaderProgram, vertices.Count)
        {
            GL.NamedBufferStorage(
                VBO,
                ColoredVertex.Size * vertices.Count,
                vertices.ToArray(),
                BufferStorageFlags.MapWriteBit
            );
            
            GL.VertexArrayAttribBinding(VAO, 0, 0);
            GL.EnableVertexArrayAttrib(VAO, 0);
            GL.VertexArrayAttribFormat(
                VAO,
                0,                      // Attribute index
                4,                      // Number of elements in attribute
                VertexAttribType.Float, // Type of attribute
                false,                  // Normalize?
                0                       // Relative offset from first item
            );
            
            GL.VertexArrayAttribBinding(VAO, 1, 0);
            GL.EnableVertexArrayAttrib(VAO, 1);
            GL.VertexArrayAttribFormat(
                VAO,
                1,                      // Attribute index
                4,                      // Number of elements in attribute
                VertexAttribType.Float, // Type of Attribute
                false,                  // Normalize?
                sizeof(float) * 4       // Relative offset from first item
            );
            
            // Link vao to vbo
            GL.VertexArrayVertexBuffer(VAO, 0, VBO, IntPtr.Zero, ColoredVertex.Size);
        }
    }
}