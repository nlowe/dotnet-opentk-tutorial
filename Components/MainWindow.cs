using System;
using System.Collections.Generic;
using dotnet_opentk_tutorial.Rendering;
using OpenTK;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Graphics;
using OpenTK.Input;

namespace dotnet_opentk_tutorial.Components
{
    public sealed class MainWindow : GameWindow
    {
        private readonly Color4 CLEAR_COLOR = new Color4(0.1f, 0.1f, 0.3f, 1.0f);

        private readonly string _titleBase;
        private Matrix4 _projection;
        private double _elapsed;
        private readonly List<IRenderable> _renderObjects = new List<IRenderable>();

        private ShaderProgram _coloredSolidShader;
        private ShaderProgram _texturedSolidShader;

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
            });
            
            _texturedSolidShader = new ShaderProgram(new Dictionary<ShaderType, string>
            {
                {ShaderType.VertexShader, "Components/Shaders/texturedVertex.vert"},
                {ShaderType.FragmentShader, "Components/Shaders/texturedVertex.frag"}
            });

            _renderObjects.Add(new TexturedRenderObject(ObjectFactory.CreateTexturedCube(0.2f, 256f, 256f), _texturedSolidShader, "Components/Textures/dotted2.png"));
            _renderObjects.Add(new TexturedRenderObject(ObjectFactory.CreateTexturedCube(0.2f, 256f, 256f), _texturedSolidShader, "Components/Textures/wooden.png"));
            _renderObjects.Add(new ColoredRenderObject(ObjectFactory.CreateSolidCube(0.2f, Color4.HotPink), _coloredSolidShader));
            _renderObjects.Add(new TexturedRenderObject(ObjectFactory.CreateTexturedCube(0.2f, 256f, 256f), _texturedSolidShader, "Components/Textures/dotted.png"));
            
            GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);
            GL.PatchParameter(PatchParameterInt.PatchVertices, 3);
            
            GL.Enable(EnableCap.DepthTest);
            Closed += (s, ce) => Exit();
        }

        public override void Exit()
        {
            foreach (var obj in _renderObjects)
            {
                obj.Dispose();
            }
            
            _coloredSolidShader.Dispose();
            _texturedSolidShader.Dispose();
            
            base.Exit();
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            HandleKeyboard();
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            _elapsed += e.Time;            
            GL.ClearColor(CLEAR_COLOR);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            var c = 0f;
            foreach (var obj in _renderObjects)
            {
                obj.Bind();
                GL.UniformMatrix4(
                    20,             // layout location in shader
                    false,          // Transpose?
                    ref _projection // matrix to send to shader
                );
                
                for (var i = 0; i < 5; i++)
                {
                    var k = i + (float) (_elapsed * (0.05f + (0.1 * c)));
                    var t = Matrix4.CreateTranslation(
                        (float)(Math.Sin(k * 5f) * (c + 0.5f)),
                        (float)(Math.Cos(k * 5f) * (c + 0.5f)),
                        -2.7f + (c + 0.1f)
                    );
                    
                    var rX = Matrix4.CreateRotationX(k * 13.0f + i);
                    var rY = Matrix4.CreateRotationY(k * 13.0f + i);
                    var rZ = Matrix4.CreateRotationZ(k * 3.0f + i);
                    var modelView = rX * rY * rZ * t;
                
                    GL.UniformMatrix4(
                        21,            // layout location in shader
                        false,         // Transpose?
                        ref modelView  // matrix to send to shader
                    );
                    obj.Render();
                }

                c += 0.3f;
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