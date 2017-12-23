#version 450 core

layout (location = 0) in vec4 position;
layout (location = 1) in vec4 color;

out vec4 frag_color;

void main(void)
{
    gl_Position = position;
    frag_color = color;
}