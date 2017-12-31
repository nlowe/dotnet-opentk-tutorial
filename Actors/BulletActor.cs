using System;
using System.Collections.Generic;
using System.Linq;
using dotnet_opentk_tutorial.Rendering;
using OpenTK;

namespace dotnet_opentk_tutorial.Actors
{
    public class BulletActor : AbstractActorBase, ICollidable
    {
        public BulletActor(IRenderable model, Vector4 position, Vector4 direction, Vector4 rotation, float velocity) 
            : base(model, position, direction, rotation, velocity)
        {
        }

        public override void Update(double time, double delta)
        {
            Rotation.X = (float) Math.Sin(time * 15 + Id);
            Rotation.Y = (float) Math.Cos(time * 15 + Id);
            Rotation.Z = (float) Math.Cos(time * 15 + Id);
            
            base.Update(time, delta);
        }

        public IEnumerable<IActor> CollidesWith(IEnumerable<IActor> others) =>
            others.Where(a => a is AsteroidActor)
                  .Where(a => (Position - a.Position).Length < a.Scale.X);
    }
}