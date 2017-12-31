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
        
        public static IReadOnlyCollection<TexturedVertex> CreateTexturedCube6(float side, float textureWidth, float h)
        {
            side = side / 2f; // half side - and other half
            const float tx10 = 0f;
            var tx11 = textureWidth / 6f;

            var tx20 = textureWidth / 6f;
            var tx21 = (textureWidth / 6f) * 2f;

            var tx30 = (textureWidth / 6f) * 2f;
            var tx31 = (textureWidth / 6f) * 3f;

            var tx40 = (textureWidth / 6f) * 3f;
            var tx41 = (textureWidth / 6f) * 4f;

            var tx50 = (textureWidth / 6f) * 4f;
            var tx51 = (textureWidth / 6f) * 5f;

            var tx60 = (textureWidth / 6f) * 5f;
            var tx61 = (textureWidth / 6f) * 6f;

            return new[]
            {
                new TexturedVertex(new Vector4(-side, -side, -side, 1.0f),  new Vector2(tx10, h)),
                new TexturedVertex(new Vector4(-side, -side, side, 1.0f),   new Vector2(tx11, h)),
                new TexturedVertex(new Vector4(-side, side, -side, 1.0f),   new Vector2(tx10, 0)),
                new TexturedVertex(new Vector4(-side, side, -side, 1.0f),   new Vector2(tx10, 0)),
                new TexturedVertex(new Vector4(-side, -side, side, 1.0f),   new Vector2(tx11, h)),
                new TexturedVertex(new Vector4(-side, side, side, 1.0f),    new Vector2(tx11, 0)),

                new TexturedVertex(new Vector4(side, -side, -side, 1.0f),   new Vector2(tx21, 0)),
                new TexturedVertex(new Vector4(side, side, -side, 1.0f),    new Vector2(tx20, 0)),
                new TexturedVertex(new Vector4(side, -side, side, 1.0f),    new Vector2(tx21, h)),
                new TexturedVertex(new Vector4(side, -side, side, 1.0f),    new Vector2(tx21, h)),
                new TexturedVertex(new Vector4(side, side, -side, 1.0f),    new Vector2(tx20, 0)),
                new TexturedVertex(new Vector4(side, side, side, 1.0f),     new Vector2(tx20, h)),

                new TexturedVertex(new Vector4(-side, -side, -side, 1.0f),  new Vector2(tx31, 0)),
                new TexturedVertex(new Vector4(side, -side, -side, 1.0f),   new Vector2(tx30, 0)),
                new TexturedVertex(new Vector4(-side, -side, side, 1.0f),   new Vector2(tx31, h)),
                new TexturedVertex(new Vector4(-side, -side, side, 1.0f),   new Vector2(tx31, h)),
                new TexturedVertex(new Vector4(side, -side, -side, 1.0f),   new Vector2(tx30, 0)),
                new TexturedVertex(new Vector4(side, -side, side, 1.0f),    new Vector2(tx30, h)),

                new TexturedVertex(new Vector4(-side, side, -side, 1.0f),   new Vector2(tx41, 0)),
                new TexturedVertex(new Vector4(-side, side, side, 1.0f),    new Vector2(tx40, 0)),
                new TexturedVertex(new Vector4(side, side, -side, 1.0f),    new Vector2(tx41, h)),
                new TexturedVertex(new Vector4(side, side, -side, 1.0f),    new Vector2(tx41, h)),
                new TexturedVertex(new Vector4(-side, side, side, 1.0f),    new Vector2(tx40, 0)),
                new TexturedVertex(new Vector4(side, side, side, 1.0f),     new Vector2(tx40, h)),

                new TexturedVertex(new Vector4(-side, -side, -side, 1.0f),  new Vector2(tx50, h)),
                new TexturedVertex(new Vector4(-side, side, -side, 1.0f),   new Vector2(tx51, h)),
                new TexturedVertex(new Vector4(side, -side, -side, 1.0f),   new Vector2(tx50, 0)),
                new TexturedVertex(new Vector4(side, -side, -side, 1.0f),   new Vector2(tx50, 0)),
                new TexturedVertex(new Vector4(-side, side, -side, 1.0f),   new Vector2(tx51, h)),
                new TexturedVertex(new Vector4(side, side, -side, 1.0f),    new Vector2(tx51, 0)),

                new TexturedVertex(new Vector4(-side, -side, side, 1.0f),   new Vector2(tx60, h)),
                new TexturedVertex(new Vector4(side, -side, side, 1.0f),    new Vector2(tx61, h)),
                new TexturedVertex(new Vector4(-side, side, side, 1.0f),    new Vector2(tx60, 0)),
                new TexturedVertex(new Vector4(-side, side, side, 1.0f),    new Vector2(tx60, 0)),
                new TexturedVertex(new Vector4(side, -side, side, 1.0f),    new Vector2(tx61, h)),
                new TexturedVertex(new Vector4(side, side, side, 1.0f),     new Vector2(tx61, 0)),

            };
        }
    }
}