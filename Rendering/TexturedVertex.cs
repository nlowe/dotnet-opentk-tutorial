using OpenTK;

namespace dotnet_opentk_tutorial.Rendering
{
    public struct TexturedVertex
    {
        public const int Size = sizeof(float) * 4 + sizeof(float) * 2;

        private readonly Vector4 _position;
        private readonly Vector2 _textureCoordinate;

        public TexturedVertex(Vector4 position, Vector2 textureCoordinate)
        {
            _position = position;
            _textureCoordinate = textureCoordinate;
        }
    }
}