using dotnet_opentk_tutorial.Rendering;
using OpenTK;
using OpenTK.Graphics.OpenGL4;

namespace dotnet_opentk_tutorial.Actors
{
    public abstract class AbstractActorBase : IActor
    {
        private static int _nextObjectId = 0;
        
        public int Id { get; }
        public IRenderable Model { get; }
        public Vector4 Position { get; set; }
        public Vector3 Scale { get; set; }

        protected float Velocity;
        protected Vector4 Rotation;
        protected Vector4 Direction;
        protected Matrix4 ModelView;

        protected AbstractActorBase(IRenderable model, Vector4 position, Vector4 direction, Vector4 rotation,
            float velocity)
        {
            Model = model;
            Position = position;
            Direction = direction;
            Rotation = rotation;
            Velocity = velocity;
            Scale = new Vector3(1);
            Id = _nextObjectId++;
        }

        public virtual void Update(double time, double delta) =>
            Position += Direction * (Velocity * (float) delta);

        public virtual void Render()
        {
            Model.Bind();

            var t = Matrix4.CreateTranslation(Position.X, Position.Y, Position.Z);
            var rX = Matrix4.CreateRotationX(Rotation.X);
            var rY = Matrix4.CreateRotationY(Rotation.Y);
            var rZ = Matrix4.CreateRotationZ(Rotation.Z);
            var s = Matrix4.CreateScale(Scale);
            
            ModelView = rX * rY * rZ * s * t;

            GL.UniformMatrix4(Model.ModelViewParameterId, false, ref ModelView);
            
            Model.Render();
        }
    }
}