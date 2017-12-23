using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics;

namespace dotnet_opentk_tutorial.Components
{
    public static class ObjectFactory
    {
        public static IEnumerable<Vertex> CreateSolidCube(float side, Color4 color)
        {
            side = side / 2f; // half side - and other half
            return new[]
            {
                new Vertex(new Vector4(-side, -side, -side, 1.0f), color),
                new Vertex(new Vector4(-side, -side, side, 1.0f), color),
                new Vertex(new Vector4(-side, side, -side, 1.0f), color),
                new Vertex(new Vector4(-side, side, -side, 1.0f), color),
                new Vertex(new Vector4(-side, -side, side, 1.0f), color),
                new Vertex(new Vector4(-side, side, side, 1.0f), color),

                new Vertex(new Vector4(side, -side, -side, 1.0f), color),
                new Vertex(new Vector4(side, side, -side, 1.0f), color),
                new Vertex(new Vector4(side, -side, side, 1.0f), color),
                new Vertex(new Vector4(side, -side, side, 1.0f), color),
                new Vertex(new Vector4(side, side, -side, 1.0f), color),
                new Vertex(new Vector4(side, side, side, 1.0f), color),

                new Vertex(new Vector4(-side, -side, -side, 1.0f), color),
                new Vertex(new Vector4(side, -side, -side, 1.0f), color),
                new Vertex(new Vector4(-side, -side, side, 1.0f), color),
                new Vertex(new Vector4(-side, -side, side, 1.0f), color),
                new Vertex(new Vector4(side, -side, -side, 1.0f), color),
                new Vertex(new Vector4(side, -side, side, 1.0f), color),

                new Vertex(new Vector4(-side, side, -side, 1.0f), color),
                new Vertex(new Vector4(-side, side, side, 1.0f), color),
                new Vertex(new Vector4(side, side, -side, 1.0f), color),
                new Vertex(new Vector4(side, side, -side, 1.0f), color),
                new Vertex(new Vector4(-side, side, side, 1.0f), color),
                new Vertex(new Vector4(side, side, side, 1.0f), color),

                new Vertex(new Vector4(-side, -side, -side, 1.0f), color),
                new Vertex(new Vector4(-side, side, -side, 1.0f), color),
                new Vertex(new Vector4(side, -side, -side, 1.0f), color),
                new Vertex(new Vector4(side, -side, -side, 1.0f), color),
                new Vertex(new Vector4(-side, side, -side, 1.0f), color),
                new Vertex(new Vector4(side, side, -side, 1.0f), color),

                new Vertex(new Vector4(-side, -side, side, 1.0f), color),
                new Vertex(new Vector4(side, -side, side, 1.0f), color),
                new Vertex(new Vector4(-side, side, side, 1.0f), color),
                new Vertex(new Vector4(-side, side, side, 1.0f), color),
                new Vertex(new Vector4(side, -side, side, 1.0f), color),
                new Vertex(new Vector4(side, side, side, 1.0f), color),
            };
        }
    }
}