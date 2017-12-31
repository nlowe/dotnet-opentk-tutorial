#version 450 core

in vec2 vs_textureCoordinate;

uniform sampler2D tex;

out vec4 color;

void main(void)
{
    color = texture(tex, vs_textureCoordinate);
}