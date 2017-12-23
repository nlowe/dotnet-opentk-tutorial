using System;
using System.IO;
using OpenTK;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Graphics;
using OpenTK.Input;

namespace dotnet_opentk_tutorial
{
    public sealed class MainWindow : GameWindow
    {
        private readonly Color4 CLEAR_COLOR = new Color4(0.1f, 0.1f, 0.3f, 1.0f);

        private readonly string _titleBase;
        private int _program;
        private int _vao;

        public MainWindow() : base(
            1280,
            720,
            GraphicsMode.Default,
            "OpenTK in .NET Core",
            GameWindowFlags.Default,
            DisplayDevice.Default,
            4, 0, // Request OpenGL 4.0
            GraphicsContextFlags.ForwardCompatible
        )
        {
            _titleBase += $"{Title}: OpenGL Version: {GL.GetString(StringName.Version)}";
        }

        protected override void OnResize(EventArgs e)
        {
            GL.Viewport(0, 0, Width, Height);
        }

        protected override void OnLoad(EventArgs e)
        {
            CursorVisible = true;
            _program = CompileShaders();

            _vao = GL.GenVertexArray();
            GL.BindVertexArray(_vao);
            
            Closed += (s, ce) => Exit();
        }

        public override void Exit()
        {
            GL.DeleteVertexArray(_vao);
            GL.DeleteProgram(_program);
            base.Exit();
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            HandleKeyboard();
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            Title = $"{_titleBase} (VSync: {VSync}) FPS: {1f / e.Time:0}";

            GL.ClearColor(CLEAR_COLOR);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.UseProgram(_program);
            GL.DrawArrays(PrimitiveType.Points, 0, 1);
            GL.PointSize(10);
            
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

        private int CompileShaders()
        {
            var vertexShader = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(vertexShader, File.ReadAllText("Components/Shaders/vertexShader.vert"));
            GL.CompileShader(vertexShader);

            var fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(fragmentShader, File.ReadAllText("Components/Shaders/fragmentShader.frag"));
            GL.CompileShader(fragmentShader);

            var program = GL.CreateProgram();
            GL.AttachShader(program, vertexShader);
            GL.AttachShader(program, fragmentShader);
            GL.LinkProgram(program);
            
            GL.DetachShader(program, vertexShader);
            GL.DetachShader(program, fragmentShader);
            GL.DeleteShader(vertexShader);
            GL.DeleteShader(fragmentShader);

            return program;
        }
    }
}