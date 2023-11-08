using Raylib_cs;
using System.Numerics;
Random generator = new Random();

Raylib.InitWindow(800, 800, "(‿ˠ‿)");
Raylib.SetTargetFPS(60);

float speed = 5;

Texture2D playerRectImage = Raylib.LoadTexture("cryingchild.png");
Vector2 playerRect = new Vector2(41, 50);
Vector2 movement = Vector2.Zero;

while (!Raylib.WindowShouldClose())
{

    movement = Vector2.Zero;

    if (Raylib.IsKeyDown(KeyboardKey.KEY_S) || Raylib.IsKeyDown(KeyboardKey.KEY_DOWN))
    {
        movement.Y += 1;
    }
    else if (Raylib.IsKeyDown(KeyboardKey.KEY_W) || Raylib.IsKeyDown(KeyboardKey.KEY_UP))
    {
        movement.Y -= 1;
    }

    if (Raylib.IsKeyDown(KeyboardKey.KEY_D) || Raylib.IsKeyDown(KeyboardKey.KEY_RIGHT))
    {
        movement.X += 1;
    }
    else if (Raylib.IsKeyDown(KeyboardKey.KEY_A) || Raylib.IsKeyDown(KeyboardKey.KEY_LEFT))
    {
        movement.X -= 1;
    }

    if (movement.Length() > 0)
    {
        movement = Vector2.Normalize(movement);
    }

    movement *= speed;

    playerRect.X += movement.X;
    playerRect.Y += movement.Y;


    Raylib.BeginDrawing();
    Raylib.ClearBackground(Color.VIOLET);
    Raylib.DrawTexture(playerRectImage,(int)playerRect.X, (int)playerRect.Y, Color.WHITE);
    Raylib.EndDrawing();
}