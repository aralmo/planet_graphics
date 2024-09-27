global using Raylib_cs;
global using System.Numerics;
global using static Raylib_cs.Raylib;
internal partial class Program
{
    private static unsafe void Main(string[] args)
    {
        InitWindow(1000, 1000, "sim");
        Camera3D camera = new Camera3D();
        camera.Position = new Vector3(0.0f,0f, -5.0f);
        camera.Target = new Vector3(0f, 0f, 0f);
        camera.Up = new Vector3(0.0f, 1.0f, 0.0f);
        camera.FovY = 60.0f;                              
        camera.Projection = CameraProjection.Perspective;  

        PlanetGenerationSettings[] planetSettings = new PlanetGenerationSettings[]
        {
            PlanetSettings.EarthLike,
            PlanetSettings.Moon,
            PlanetSettings.MarsLike,
            PlanetSettings.VenusLike,
            PlanetSettings.IcePlanet,
            PlanetSettings.LavaPlanet
        };

        int currentPlanetIndex = 0;
        var planet = PlanetGenerator.GeneratePlanet(planetSettings[currentPlanetIndex]);
        float rotationAngle = 0.0f;
        float cameraDistance = 5.0f; // Closer camera distance
        SetTargetFPS(60);
        while (!WindowShouldClose())
        {
            if (IsKeyDown(KeyboardKey.Right))
            {
                rotationAngle -= 0.05f;
            if (IsKeyDown(KeyboardKey.Left))
            {
                rotationAngle += 0.05f;
            }
            if (IsKeyPressed(KeyboardKey.Space))
            {
                currentPlanetIndex = (currentPlanetIndex + 1) % planetSettings.Length;
                planet = PlanetGenerator.GeneratePlanet(planetSettings[currentPlanetIndex]);
            }
            cameraDistance += GetMouseWheelMove() * 0.5f; // Adjust this value to control the zoom speed
            camera.Position.X = MathF.Sin(rotationAngle) * cameraDistance;
            camera.Position.Z = MathF.Cos(rotationAngle) * cameraDistance;
            BeginDrawing();
            ClearBackground(Color.Black);
            BeginMode3D(camera);
            DrawModel(planet, Vector3.Zero, 1.0f, Color.White);
            EndMode3D();
            EndDrawing();
        }
        CloseWindow();
    }
}
