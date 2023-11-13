using Raylib_cs;
using System.Numerics;
Random generator = new Random();

int GameX = 1600;
int GameY = 900;

Raylib.InitWindow(GameX, GameY, "(‿ˠ‿)");
Raylib.SetTargetFPS(60);

float speed = 5;

int playerSizeX = 53;
int playerSizeY = 66;

Texture2D playerRectImage = Raylib.LoadTexture("cryingchild3.png");
Vector2 playerRect = new Vector2(playerSizeX, playerSizeY);
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
    else if (playerRect.X > GameX - playerSizeX)
    {
        playerRect.X = GameX - playerSizeX;
    }

    if (playerRect.Y < 0)
    {
        playerRect.Y = 0;
    }
    else if (playerRect.Y > GameY - playerSizeY)
    {
        playerRect.Y = GameY - playerSizeY;
    }

    //if playerRect.X/.Y = wall.X/.Y speed = 0.

    Raylib.BeginDrawing();

    Raylib.ClearBackground(Color.BLACK);
    Raylib.DrawTexture(playerRectImage,(int)playerRect.X, (int)playerRect.Y, Color.WHITE);

    Raylib.EndDrawing();
}