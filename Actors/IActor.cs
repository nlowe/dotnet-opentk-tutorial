using OpenTK;

namespace dotnet_opentk_tutorial.Actors
{
    public interface IActor
    {
        Vector4 Position { get; set; }
        Vector3 Scale { get; set; }
        void Update(double time, double delta);
        void Render();
    }
}