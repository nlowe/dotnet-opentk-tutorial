using System;
using dotnet_opentk_tutorial.Rendering;
using OpenTK;

namespace dotnet_opentk_tutorial.Actors
{
    public class GameOverActor : AbstractActorBase
    {
        public GameOverActor(IRenderable model, Vector4 position, Vector4 direction, Vector4 rotation, float velocity) : base(model, position, direction, rotation, velocity)
        {
        }

        public override void Update(double time, double delta)
        {
            var k = (float) time * 0.4f;
            Rotation.X = k * 0.7f;
            Rotation.Y = k * 0.5f;
            Rotation.Z = (float) Math.Sin(k * 0.4f);
            
            base.Update(time, delta);
        }
    }
}