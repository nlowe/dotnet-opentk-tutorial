using System;

namespace dotnet_opentk_tutorial.Rendering
{
    public interface IRenderable : IDisposable
    {
        int ProjectionParameterId { get; }
        int ModelViewParameterId { get; }
        
        void Bind();
        void Render();
    }
}