global using Raylib_cs;
global using System.Numerics;
global using static Raylib_cs.Raylib;
internal class Program
{
    private static unsafe void Main(string[] args)
    {
        InitWindow(1000, 1000, "sim");
        Camera3D camera = new Camera3D();
        camera.Position = new Vector3(0.0f, 2.0f, -10.0f);    // Camera position
        camera.Target = new Vector3(0f, 0f, 0f);      // Camera looking at point
        camera.Up = new Vector3(0.0f, 1.0f, 0.0f);          // Camera up vector (rotation towards target)
        camera.FovY = 60.0f;                                // Camera field-of-view Y
        camera.Projection = CameraProjection.Perspective;             // Camera projection type

        // Generate a random height map
        Image heightMap = GenImagePerlinNoise(256, 256, 0, 0, 8.0f);
        Texture2D heightMapTexture = LoadTextureFromImage(heightMap);

        // Generate the icosphere mesh
        Mesh icosphereMesh = Icosphere.GenerateIcosphere(3, 1.0f, heightMap);
        Model icosphereModel = LoadModelFromMesh(icosphereMesh);

        // Define a light
        Light light = new Light();
        light.Position = new Vector3(0.0f, 10.0f, -10.0f);
        light.Target = new Vector3(0.0f, 0.0f, 0.0f);
        light.Color = Color.White;

        while (!WindowShouldClose())
        {
            BeginDrawing();
            ClearBackground(Color.Black);
            BeginMode3D(camera);

            // Draw the planet
            DrawModel(icosphereModel, Vector3.Zero, 1.0f, Color.White);

            // Draw the light source
            DrawSphere(light.Position, 0.2f, Color.Yellow);

            EndMode3D();
            EndDrawing();
        }

        UnloadTexture(heightMapTexture);
        UnloadModel(icosphereModel);
        CloseWindow();
    }

    private struct Light
    {
        public Vector3 Position;
        public Vector3 Target;
        public Color Color;
    }
}