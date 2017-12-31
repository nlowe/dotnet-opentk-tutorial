using System.Collections.Generic;

namespace dotnet_opentk_tutorial.Actors
{
    public interface ICollidable : IActor
    {
        IEnumerable<IActor> CollidesWith(IEnumerable<IActor> others);
    }
}