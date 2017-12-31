using System;
using System.Collections.Generic;
using dotnet_opentk_tutorial.Components;
using dotnet_opentk_tutorial.Rendering;
using OpenTK;
using OpenTK.Graphics;

namespace dotnet_opentk_tutorial.Actors
{
    public class ActorFactory : IDisposable
    {
        public static readonly string WOODEN_ASTEROID = "Wooden";
        public static readonly string GOLDEN_ASTEROID = "Golden";
        public static readonly string ASTEROID = "Asteroid";
        public static readonly string SPACECRAFT = "Spacecraft";
        public static readonly string GAME_OVER = "Gameover";
        public static readonly string BULLET = "Bullet";
        
        private const float ZPlane = -2.7f;
        private readonly Random _random = new Random();
        private readonly Dictionary<string, IRenderable> _models;

        public ActorFactory(ShaderProgram coloredSolidShader, ShaderProgram texturedSolidShader)
        {
            _models = new Dictionary<string, IRenderable>()
            {
                {
                    WOODEN_ASTEROID,
                    new TexturedRenderObject(ObjectFactory.CreateTexturedCube(1, 256, 256), texturedSolidShader, "Components/Textures/wooden.png")
                },
                {
                    GOLDEN_ASTEROID,
                    new TexturedRenderObject(ObjectFactory.CreateTexturedCube(1, 256, 256), texturedSolidShader, "Components/Textures/golden.bmp")
                },
                {
                    ASTEROID,
                    new TexturedRenderObject(ObjectFactory.CreateTexturedCube(1, 256, 256), texturedSolidShader, "Components/Textures/asteroid.bmp")
                },
                {
                    SPACECRAFT,
                    new TexturedRenderObject(ObjectFactory.CreateTexturedCube6(1, 1536, 256), texturedSolidShader, "Components/Textures/spacecraft.png")
                },
                {
                    GAME_OVER,
                    new TexturedRenderObject(ObjectFactory.CreateTexturedCube6(1, 1536, 256), texturedSolidShader, "Components/Textures/gameover.png")
                },
                {
                    BULLET,
                    new ColoredRenderObject(ObjectFactory.CreateSolidCube(1, Color4.HotPink), coloredSolidShader)
                }
            };
        }

        public SpacecraftActor CreateSpacecraft() =>
            new SpacecraftActor(_models[SPACECRAFT], new Vector4(0, -1f, ZPlane, 0), Vector4.Zero, Vector4.Zero, 0)
            {
                Scale = new Vector3(0.2f, 0.2f, 0.01f)
            };

        public AsteroidActor CreateAsteroid() => CreateAsteroid(ASTEROID, GetRandomPosition());
        public AsteroidActor CreateGoldenAsteroid() => CreateAsteroid(GOLDEN_ASTEROID, GetRandomPosition());
        public AsteroidActor CreateWoodenAsteroid() => CreateAsteroid(WOODEN_ASTEROID, GetRandomPosition());
        
        public AsteroidActor CreateAsteroid(string model, Vector4 position) =>
            new AsteroidActor(_models[model], position, Vector4.Zero, Vector4.Zero, 0.1f)
            {
                Scale = new Vector3(0.2f),
                Points = model == GOLDEN_ASTEROID ? 50 : (model == WOODEN_ASTEROID ? 10 : 1)
            };

        public AsteroidActor CreateRandomAsteroid()
        {
            var rnd = _random.NextDouble();
            var position = GetRandomPosition();

            return  CreateAsteroid(rnd < 0.01 ? GOLDEN_ASTEROID : (rnd < 0.2 ? WOODEN_ASTEROID : ASTEROID), position);
        }

        public BulletActor CreateBullet(Vector4 position) =>
            new BulletActor(_models[BULLET], position + new Vector4(0, 0.1f, 0, 0), Vector4.UnitY, Vector4.Zero, 0.8f)
            {
                Scale = new Vector3(0.05f)
            };

        public GameOverActor CreateGameOver() =>
            new GameOverActor(_models[GAME_OVER], new Vector4(0, 0, ZPlane, 0), Vector4.Zero, Vector4.Zero, 0.0f)
            {
                Scale = new Vector3(0.8f)
            };

        private Vector4 GetRandomPosition() =>
            new Vector4(
                ((float) _random.NextDouble() - 0.5f) * 1.1f,
                ((float) _random.NextDouble() - 0.5f) * 1.1f,
                ZPlane,
                0
            );

        public void Dispose()
        {
            foreach (var model in _models)
            {
                model.Value.Dispose();
            }
        }
    }
}