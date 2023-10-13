using Raylib_cs;
using System.Numerics;

Raylib.InitWindow(800, 800, "hej");
Raylib.SetTargetFPS(60);

Vector2 position = new Vector2(50, 50);

while (!Raylib.WindowShouldClose())
{
    Raylib.BeginDrawing();
    Raylib.ClearBackground(Color.VIOLET);
    Raylib.DrawRectangle((int)position.X, (int)position.Y, 50, 50, Color.WHITE);

    if (Raylib.IsKeyDown(KeyboardKey.KEY_DOWN))
    {
        position.Y += 5;
    }
    else if (Raylib.IsKeyDown(KeyboardKey.KEY_UP))
    {
        position.Y -= 5;
    }

    if (Raylib.IsKeyDown(KeyboardKey.KEY_RIGHT))
    {
        position.X += 5;
    }
    else if (Raylib.IsKeyDown(KeyboardKey.KEY_LEFT))
    {
        position.X -= 5;
    }

    while (position.X < 0)
    {
        position.X += 5;
    }
    while (position.Y < 0)
    {
        position.Y += 5;
    }
    while (position.X > 750)
    {
        position.X -= 5;
    }
    while (position.Y > 750)
    {
        position.Y -= 5;
    }
    Raylib.EndDrawing();
}