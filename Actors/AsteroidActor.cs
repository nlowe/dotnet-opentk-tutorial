using System;
using dotnet_opentk_tutorial.Rendering;
using OpenTK;

namespace dotnet_opentk_tutorial.Actors
{
    public class AsteroidActor : AbstractActorBase
    {
        public int Points { get; set; }
        
        public AsteroidActor(IRenderable model, Vector4 position, Vector4 direction, Vector4 rotation, float velocity)
            : base(model, position, direction, rotation, velocity)
        {
        }

        public override void Update(double time, double delta)
        {
            Rotation.X = (float) Math.Sin((time + Id) * 0.3);
            Rotation.Y = (float) Math.Cos((time + Id) * 0.5);
            Rotation.Z = (float) Math.Cos((time + Id) * 0.2);
            
            Direction = new Vector4(Rotation.X, Rotation.Y, 0, 0).Normalized();
            
            base.Update(time, delta);
        }
    }
}