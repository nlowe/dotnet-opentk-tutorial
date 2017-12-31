using System;
using System.Collections.Generic;
using System.Linq;
using dotnet_opentk_tutorial.Actors;
using OpenTK;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Graphics;
using OpenTK.Input;

namespace dotnet_opentk_tutorial.Components
{
    public sealed class MainWindow : GameWindow
    {
        private readonly Color4 CLEAR_COLOR = Color4.Black;

        private readonly string _titleBase;
        private Matrix4 _projection;
        private double _elapsed;

        private readonly List<IActor> _actors = new List<IActor>();
        private SpacecraftActor _player;
        
        private ActorFactory _actorFactory;
        private ShaderProgram _coloredSolidShader;
        private ShaderProgram _texturedSolidShader;

        private KeyboardState _latsKeyboardState;
        private bool _gameOver = false;
        private bool _gameOverSpawned = false;
        private int _score = 0;

        public MainWindow() : base(
            1280,
            720,
            GraphicsMode.Default,
            "OpenTK in .NET Core",
            GameWindowFlags.Default,
            DisplayDevice.Default,
            4, 5, // Request OpenGL 4.5
            GraphicsContextFlags.ForwardCompatible
        )
        {
            _titleBase += $"{Title}: OpenGL Version: {GL.GetString(StringName.Version)}";
            Title = _titleBase;
        }

        protected override void OnResize(EventArgs e)
        {
            GL.Viewport(0, 0, Width, Height);
            CreateProjection();
        }

        protected override void OnLoad(EventArgs e)
        {
            CursorVisible = true;
            VSync = VSyncMode.Off;
            CreateProjection();

            _coloredSolidShader = new ShaderProgram(new Dictionary<ShaderType, string>
            {
                {ShaderType.VertexShader, "Components/Shaders/coloredVertex.vert"},
                {ShaderType.FragmentShader, "Components/Shaders/coloredVertex.frag"}
            }, 20, 21);
            
            _texturedSolidShader = new ShaderProgram(new Dictionary<ShaderType, string>
            {
                {ShaderType.VertexShader, "Components/Shaders/texturedVertex.vert"},
                {ShaderType.FragmentShader, "Components/Shaders/texturedVertex.frag"}
            }, 20, 21);

            _actorFactory = new ActorFactory(_coloredSolidShader, _texturedSolidShader);
            _player = _actorFactory.CreateSpacecraft();
            
            _actors.AddRange(new IActor[]
            {
                _player,
                _actorFactory.CreateAsteroid(),
                _actorFactory.CreateGoldenAsteroid(),
                _actorFactory.CreateWoodenAsteroid()
            });
            
            GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);
            GL.PatchParameter(PatchParameterInt.PatchVertices, 3);
            
            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.CullFace);
            Closed += (s, ce) => Exit();
        }

        public override void Exit()
        {
            _actorFactory.Dispose();
            _coloredSolidShader.Dispose();
            _texturedSolidShader.Dispose();
            
            base.Exit();
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            _elapsed += e.Time;            

            var toRemove = new HashSet<IActor>();
            var view = new Vector4(0, 0, -2.4f, 0);

            var outOfBoundsAsteroids = 0;
            var removedAsteroids = 0;
            foreach (var actor in _actors)
            {
                actor.Update(_elapsed, e.Time);
                if ((view - actor.Position).Length > 2)
                {
                    toRemove.Add(actor);
                    if (actor is AsteroidActor)
                    {
                        outOfBoundsAsteroids++;
                    }
                }

                if (!(actor is ICollidable c)) continue;
                var collisions = c.CollidesWith(_actors).ToList();

                if (!collisions.Any()) continue;
                if (actor == _player)
                {
                    foreach (var a in _actors)
                    {
                        toRemove.Add(a);
                    }
                    
                    _gameOver = true;
                }
                else
                {
                    toRemove.Add(actor);
                    foreach (var asteroid in collisions)
                    {
                        if (!toRemove.Add(asteroid) || !(asteroid is AsteroidActor a)) continue;
                        _score += a.Points;
                        
                        Console.WriteLine($"Got one! Your score is now {_score}");
                        
                        removedAsteroids++;
                    }
                }
            }

            foreach (var actor in toRemove)
            {
                _actors.Remove(actor);
            }

            if (!_gameOver)
            {
                for (var i = 0; i < removedAsteroids; i++)
                {
                    _actors.Add(_actorFactory.CreateRandomAsteroid());
                    _actors.Add(_actorFactory.CreateRandomAsteroid());
                }
                
                for (var i = 0; i < outOfBoundsAsteroids; i++)
                {
                    _actors.Add(_actorFactory.CreateRandomAsteroid());
                }
            }
            else if(!_gameOverSpawned)
            {
                _actors.Add(_actorFactory.CreateGameOver());
                _gameOverSpawned = true;
            }
            
            HandleKeyboard();
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            GL.ClearColor(CLEAR_COLOR);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            foreach (var obj in _actors)
            {
                GL.UniformMatrix4(
                    20,             // layout location in shader
                    false,          // Transpose?
                    ref _projection // matrix to send to shader
                );
                
                obj.Render();
            }
            
            SwapBuffers();
        }

        private void HandleKeyboard()
        {
            var keyState = Keyboard.GetState();

            if (keyState.IsKeyDown(Key.Escape))
            {
                Exit();
            }

            if (keyState.IsKeyDown(Key.A))
            {
                _player.MoveLeft = true;
            }

            if (keyState.IsKeyDown(Key.D))
            {
                _player.MoveRight = true;
            }

            if (!_gameOver && keyState.IsKeyDown(Key.Space) && _latsKeyboardState.IsKeyUp(Key.Space))
            {
                _actors.Add(_actorFactory.CreateBullet(_player.Position));
            }

            _latsKeyboardState = keyState;
        }

        private void CreateProjection()
        {
            var aspectRatio = (float)Width/Height;
            _projection = Matrix4.CreatePerspectiveFieldOfView(
                60f*((float) Math.PI/180f), // FOV in radians
                aspectRatio,                // current window aspect ratio
                0.1f,                       // near plane
                4000f                        // far plane
            );
        }
    }
}