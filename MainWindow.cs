using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        private double _elapsed;

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
            _elapsed += e.Time;
            Title = $"{_titleBase} (VSync: {VSync}) FPS: {1f / e.Time:0}";

            GL.ClearColor(CLEAR_COLOR);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.UseProgram(_program);
            
            var position = new Vector4(
                (float) Math.Sin(_elapsed) * 0.5f,
                (float) Math.Cos(_elapsed) * 0.5f,
                0.0f,
                1.0f
            );
            
            GL.VertexAttrib1(0, _elapsed);
            GL.VertexAttrib4(1, position);
            
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