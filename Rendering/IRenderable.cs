using System;

namespace dotnet_opentk_tutorial.Rendering
{
    public interface IRenderable : IDisposable
    {
        void Bind();
        void Render();
    }
}