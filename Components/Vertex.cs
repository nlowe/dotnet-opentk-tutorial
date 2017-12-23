using OpenTK;
using OpenTK.Graphics;

namespace dotnet_opentk_tutorial.Components
{
    public struct Vertex
    {
        public const int Size = sizeof(float) * 4 + sizeof(float) * 4;

        private readonly Vector4 _position;
        private readonly Color4 _color;

        public Vertex(Vector4 position, Color4 color)
        {
            _position = position;
            _color = color;
        }
    }
}