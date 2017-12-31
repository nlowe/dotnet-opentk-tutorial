using System;
using System.Collections.Generic;
using System.Linq;
using dotnet_opentk_tutorial.Rendering;
using OpenTK;

namespace dotnet_opentk_tutorial.Actors
{
    public class SpacecraftActor : AbstractActorBase, ICollidable
    {
        public bool MoveLeft { get; set; }
        public bool MoveRight { get; set; }
        
        public SpacecraftActor(IRenderable model, Vector4 position, Vector4 direction, Vector4 rotation, float velocity)
            : base(model, position, direction, rotation, velocity)
        {
        }

        public override void Update(double time, double delta)
        {
            // Asked to move left while stopped or already moving left
            if (MoveLeft && !(Direction.X > 0 && Velocity > 0))
            {
                Direction.X = -1;
                Velocity += 0.8f * (float) delta;
                MoveLeft = false;
            }
            // Asked to move right while stopped or already moving right
            else if (MoveRight && !(Direction.X < 0 && Velocity > 0))
            {
                Direction.X = 1;
                Velocity += 0.8f * (float) delta;
                MoveRight = false;
            }
            // Slow to a stop
            else
            {
                Velocity -= 0.9f * (float) delta;
            }

            if (Velocity <= 0.0f)
            {
                Rotation.Y = 0;
            }
            Velocity = Math.Clamp(Velocity, 0.0f, 0.8f);

            // Rotate the spacecraft while moving based on velocity
            if (Direction.X < 0 && Velocity > 0)
            {
                Rotation.Y = -Velocity;
            }

            if (Direction.X > 0 && Velocity > 0)
            {
                Rotation.Y = Velocity;
            }
            
            base.Update(time, delta);
        }

        public IEnumerable<IActor> CollidesWith(IEnumerable<IActor> others) =>
            others.Where(a => a is AsteroidActor)
                  .Where(a => (Position - a.Position).Length < (Scale.X + Scale.X));
    }
}