using Raylib_cs;
using System.Numerics;
Random generator = new Random();

Raylib.InitWindow(1600, 900, "(‿ˠ‿)");
Raylib.SetTargetFPS(60);

float speed = 5;

Texture2D playerRectImage = Raylib.LoadTexture("cryingchild3.png");
Vector2 playerRect = new Vector2(53, 65);
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

    if (playerRect.X < 0)
    {
        playerRect.X = 0;
    }
    else if (playerRect.X > 759)
    {
        playerRect.X = 759;
    }

    if (playerRect.Y < 0)
    {
        playerRect.Y = 0;
    }
    else if (playerRect.Y > 750)
    {
        playerRect.Y = 750;
    }

    //if playerRect.X/.Y = wall.X/.Y speed = 0.

    Raylib.BeginDrawing();

    Raylib.ClearBackground(Color.BLACK);
    Raylib.DrawTexture(playerRectImage,(int)playerRect.X, (int)playerRect.Y, Color.WHITE);

    Raylib.EndDrawing();
}