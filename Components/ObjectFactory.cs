using System.Collections.Generic;
using dotnet_opentk_tutorial.Rendering;
using OpenTK;
using OpenTK.Graphics;

namespace dotnet_opentk_tutorial.Components
{
    public static class ObjectFactory
    {
        public static IReadOnlyCollection<ColoredVertex> CreateSolidCube(float side, Color4 color)
        {
            side = side / 2f; // half side - and other half
            return new[]
            {
                new ColoredVertex(new Vector4(-side, -side, -side, 1.0f), color),
                new ColoredVertex(new Vector4(-side, -side, side, 1.0f), color),
                new ColoredVertex(new Vector4(-side, side, -side, 1.0f), color),
                new ColoredVertex(new Vector4(-side, side, -side, 1.0f), color),
                new ColoredVertex(new Vector4(-side, -side, side, 1.0f), color),
                new ColoredVertex(new Vector4(-side, side, side, 1.0f), color),

                new ColoredVertex(new Vector4(side, -side, -side, 1.0f), color),
                new ColoredVertex(new Vector4(side, side, -side, 1.0f), color),
                new ColoredVertex(new Vector4(side, -side, side, 1.0f), color),
                new ColoredVertex(new Vector4(side, -side, side, 1.0f), color),
                new ColoredVertex(new Vector4(side, side, -side, 1.0f), color),
                new ColoredVertex(new Vector4(side, side, side, 1.0f), color),

                new ColoredVertex(new Vector4(-side, -side, -side, 1.0f), color),
                new ColoredVertex(new Vector4(side, -side, -side, 1.0f), color),
                new ColoredVertex(new Vector4(-side, -side, side, 1.0f), color),
                new ColoredVertex(new Vector4(-side, -side, side, 1.0f), color),
                new ColoredVertex(new Vector4(side, -side, -side, 1.0f), color),
                new ColoredVertex(new Vector4(side, -side, side, 1.0f), color),

                new ColoredVertex(new Vector4(-side, side, -side, 1.0f), color),
                new ColoredVertex(new Vector4(-side, side, side, 1.0f), color),
                new ColoredVertex(new Vector4(side, side, -side, 1.0f), color),
                new ColoredVertex(new Vector4(side, side, -side, 1.0f), color),
                new ColoredVertex(new Vector4(-side, side, side, 1.0f), color),
                new ColoredVertex(new Vector4(side, side, side, 1.0f), color),

                new ColoredVertex(new Vector4(-side, -side, -side, 1.0f), color),
                new ColoredVertex(new Vector4(-side, side, -side, 1.0f), color),
                new ColoredVertex(new Vector4(side, -side, -side, 1.0f), color),
                new ColoredVertex(new Vector4(side, -side, -side, 1.0f), color),
                new ColoredVertex(new Vector4(-side, side, -side, 1.0f), color),
                new ColoredVertex(new Vector4(side, side, -side, 1.0f), color),

                new ColoredVertex(new Vector4(-side, -side, side, 1.0f), color),
                new ColoredVertex(new Vector4(side, -side, side, 1.0f), color),
                new ColoredVertex(new Vector4(-side, side, side, 1.0f), color),
                new ColoredVertex(new Vector4(-side, side, side, 1.0f), color),
                new ColoredVertex(new Vector4(side, -side, side, 1.0f), color),
                new ColoredVertex(new Vector4(side, side, side, 1.0f), color),
            };
        }
        
        public static IReadOnlyCollection<TexturedVertex> CreateTexturedCube(float side, float w, float h)
        {
            side = side / 2f; // half side - and other half

            return new[]
            {
                new TexturedVertex(new Vector4(-side, -side, -side, 1.0f),   new Vector2(0, h)),
                new TexturedVertex(new Vector4(-side, -side, side, 1.0f),    new Vector2(w, h)),
                new TexturedVertex(new Vector4(-side, side, -side, 1.0f),    new Vector2(0, 0)),
                new TexturedVertex(new Vector4(-side, side, -side, 1.0f),    new Vector2(0, 0)),
                new TexturedVertex(new Vector4(-side, -side, side, 1.0f),    new Vector2(w, h)),
                new TexturedVertex(new Vector4(-side, side, side, 1.0f),     new Vector2(w, 0)),

                new TexturedVertex(new Vector4(side, -side, -side, 1.0f),    new Vector2(w, 0)),
                new TexturedVertex(new Vector4(side, side, -side, 1.0f),     new Vector2(0, 0)),
                new TexturedVertex(new Vector4(side, -side, side, 1.0f),     new Vector2(w, h)),
                new TexturedVertex(new Vector4(side, -side, side, 1.0f),     new Vector2(w, h)),
                new TexturedVertex(new Vector4(side, side, -side, 1.0f),     new Vector2(0, 0)),
                new TexturedVertex(new Vector4(side, side, side, 1.0f),      new Vector2(0, h)),

                new TexturedVertex(new Vector4(-side, -side, -side, 1.0f),   new Vector2(w, 0)),
                new TexturedVertex(new Vector4(side, -side, -side, 1.0f),    new Vector2(0, 0)),
                new TexturedVertex(new Vector4(-side, -side, side, 1.0f),    new Vector2(w, h)),
                new TexturedVertex(new Vector4(-side, -side, side, 1.0f),    new Vector2(w, h)),
                new TexturedVertex(new Vector4(side, -side, -side, 1.0f),    new Vector2(0, 0)),
                new TexturedVertex(new Vector4(side, -side, side, 1.0f),     new Vector2(0, h)),

                new TexturedVertex(new Vector4(-side, side, -side, 1.0f),    new Vector2(w, 0)),
                new TexturedVertex(new Vector4(-side, side, side, 1.0f),     new Vector2(0, 0)),
                new TexturedVertex(new Vector4(side, side, -side, 1.0f),     new Vector2(w, h)),
                new TexturedVertex(new Vector4(side, side, -side, 1.0f),     new Vector2(w, h)),
                new TexturedVertex(new Vector4(-side, side, side, 1.0f),     new Vector2(0, 0)),
                new TexturedVertex(new Vector4(side, side, side, 1.0f),      new Vector2(0, h)),

                new TexturedVertex(new Vector4(-side, -side, -side, 1.0f),   new Vector2(0, h)),
                new TexturedVertex(new Vector4(-side, side, -side, 1.0f),    new Vector2(w, h)),
                new TexturedVertex(new Vector4(side, -side, -side, 1.0f),    new Vector2(0, 0)),
                new TexturedVertex(new Vector4(side, -side, -side, 1.0f),    new Vector2(0, 0)),
                new TexturedVertex(new Vector4(-side, side, -side, 1.0f),    new Vector2(w, h)),
                new TexturedVertex(new Vector4(side, side, -side, 1.0f),     new Vector2(w, 0)),

                new TexturedVertex(new Vector4(-side, -side, side, 1.0f),    new Vector2(0, h)),
                new TexturedVertex(new Vector4(side, -side, side, 1.0f),     new Vector2(w, h)),
                new TexturedVertex(new Vector4(-side, side, side, 1.0f),     new Vector2(0, 0)),
                new TexturedVertex(new Vector4(-side, side, side, 1.0f),     new Vector2(0, 0)),
                new TexturedVertex(new Vector4(side, -side, side, 1.0f),     new Vector2(w, h)),
                new TexturedVertex(new Vector4(side, side, side, 1.0f),      new Vector2(w, 0)),
            };
        }
    }
}