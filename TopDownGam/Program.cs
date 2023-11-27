using Raylib_cs;
using System.Numerics;
Random generator = new Random();

float GameX = 1600;
float GameY = 900;

Raylib.InitWindow((int)GameX, (int)GameY, "(‿ˠ‿)");
Raylib.SetTargetFPS(60);

float speed = 5;

float playerRectX = 0;
float playerRectY = 0;

float playerSizeX = 53;
float playerSizeY = 66;

int[,] sceneData = {
{0,0,0,0,0,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
{0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
{0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
{0,0,0,0,0,1,1,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
{0,0,0,0,0,0,1,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
{0,0,0,0,0,0,1,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
{0,1,1,1,1,1,1,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
{0,1,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
{0,1,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
{0,1,1,1,1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
};

Texture2D playerRectImage = Raylib.LoadTexture("cryingchild3.png");
Vector2 playerRect = new Vector2(playerSizeX, playerSizeY);
Vector2 movement = Vector2.Zero;

int tileSize = 50;

while (!Raylib.WindowShouldClose())
{

    Camera2D camera = { 0 };
    camera.target = new Vector2(playerRectX - playerSizeX/2, playerRectY - playerSizeY);
    camera.offset = new Vector2(GameX/2,GameY/2);
    camera.rotation = 0.0f;
    camera.zoom = 1.0f;

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

    playerRectX += movement.X;
    playerRectY += movement.Y;

    if (playerRectX < 0)
    {
        playerRectX = 0;
    }
    else if (playerRectX > GameX - playerSizeX)
    {
        playerRectX = GameX - playerSizeX;
    }

    if (playerRectY < 0)
    {
        playerRectY = 0;
    }
    else if (playerRectY > GameY - playerSizeY)
    {
        playerRectY = GameY - playerSizeY;
    }

    //if playerRectX/Y = wall.X/.Y speed = 0.

    Raylib.BeginDrawing();

    Raylib.ClearBackground(Color.BLACK);

    for (int y = 0; y < sceneData.GetLength(0); y++)
    {
        for (int x = 0; x < sceneData.GetLength(1); x++)
        {
            if (sceneData[y, x] == 1)
            {

                Raylib.DrawRectangle(x * tileSize, y * tileSize, tileSize, tileSize, Color.DARKGRAY);
            }
        }
    }

    Raylib.DrawTexture(playerRectImage, (int)playerRectX, (int)playerRectY, Color.WHITE);

    Raylib.EndDrawing();
}