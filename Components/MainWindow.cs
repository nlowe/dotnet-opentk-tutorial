using System;
using System.Collections.Generic;
using System.IO;
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
        private int _program;
        private Matrix4 _modelView;
        private double _elapsed;
        private readonly List<RenderObject> _renderObjects = new List<RenderObject>();

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
        }

        protected override void OnLoad(EventArgs e)
        {
            CursorVisible = true;
            _program = CompileShaders();

            _renderObjects.Add(new RenderObject(ObjectFactory.CreateSolidCube(0.2f, Color4.HotPink)));
            
            GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);
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
            
            GL.DeleteProgram(_program);
            base.Exit();
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            _elapsed += e.Time;

            var k = (float) _elapsed * 0.05f;
            var rX = Matrix4.CreateRotationX(k * 13.0f);
            var rY = Matrix4.CreateRotationY(k * 13.0f);
            var rZ = Matrix4.CreateRotationZ(k * 3.0f);

            var t = Matrix4.CreateTranslation(
                (float) (Math.Sin(k * 5f) * 0.5f),
                (float) (Math.Cos(k * 5f) * 0.5f),
                0f
            );
            
            _modelView = rX * rY * rZ * t;
            
            HandleKeyboard();
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            GL.ClearColor(CLEAR_COLOR);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.UseProgram(_program);
            GL.UniformMatrix4(
                20,            // layout location in shader
                false,         // Transpose?
                ref _modelView // matrix to send to shader
            );
            foreach (var obj in _renderObjects)
            {
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
        }

        private int CompileShader(ShaderType type, string path)
        {
            var shader = GL.CreateShader(type);
            var src = File.ReadAllText(path);
            
            GL.ShaderSource(shader, src);
            GL.CompileShader(shader);

            var info = GL.GetShaderInfoLog(shader);
            if (!string.IsNullOrEmpty(info))
            {
                Console.WriteLine($"GL.CompileShader [{type}] had info log: {info}");
            }

            return shader;
        }

        private int CompileShaders()
        {
            var program = GL.CreateProgram();

            var shaders = new List<int>
            {
                CompileShader(ShaderType.VertexShader, "Components/Shaders/vertexShader.vert"),
                CompileShader(ShaderType.FragmentShader, "Components/Shaders/fragmentShader.frag")
            };

            foreach (var s in shaders)
            {
                GL.AttachShader(program, s);
            }
            
            GL.LinkProgram(program);
            var info = GL.GetProgramInfoLog(program);
            if (!string.IsNullOrEmpty(info))
            {
                Console.WriteLine($"GL.LinkProgram had info log: {info}");
            }
            
            foreach (var s in shaders)
            {
                GL.DetachShader(program, s);
                GL.DeleteShader(s);
            }

            return program;
        }
    }
}