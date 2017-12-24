using System.Runtime.InteropServices;
using OpenTK;
using OpenTK.Graphics;

namespace dotnet_opentk_tutorial.Rendering
{
    public struct ColoredVertex
    {
        public const int Size = sizeof(float) * 4 + sizeof(float) * 4;

        private readonly Vector4 _position;
        private readonly Color4 _color;

        public ColoredVertex(Vector4 position, Color4 color)
        {
            _position = position;
            _color = color;
        }
    }
}