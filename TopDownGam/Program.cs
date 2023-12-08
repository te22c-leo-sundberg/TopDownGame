using Microsoft.VisualBasic;
using Raylib_cs;
using System.Numerics;
using System.Reflection.Metadata;
Random generator = new Random();

string GameState = "Menu";

float GameX = 1600;
float GameY = 900;

Raylib.InitWindow((int)GameX, (int)GameY, "(‿ˠ‿)");
Raylib.SetTargetFPS(60);

bool CameraReal = false;

float speed = 5;

float playerRectX = 0;
float playerRectY = 0;

float playerSizeX = 53;
float playerSizeY = 66;

int tileSize = 50;

Camera2D camera = new Camera2D();
camera.offset = new Vector2(GameX / 2, GameY / 2);
camera.rotation = 0.0f;
camera.zoom = 1.0f;
camera.target = new Vector2(playerRectX + playerSizeX / 2, playerRectY + playerSizeY / 2);

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

Rectangle playerRect = new Rectangle(playerRectX, playerRectY, playerSizeY, playerSizeX);

List<Rectangle> walls = new();
{

    for (int y = 0; y < sceneData.GetLength(0); y++)
    {
        for (int x = 0; x < sceneData.GetLength(1); x++)
        {
            if (sceneData[y, x] == 1)
            {
                // Skapa en rektangel
                Rectangle r = new Rectangle(x * tileSize, y * tileSize, tileSize, tileSize);
                walls.Add(r);
                // Lägg till den i listan
                // Raylib.DrawRectangle(x * tileSize, y * tileSize, tileSize, tileSize, Color.DARKGRAY);
            }
        }
    }
}



Texture2D playerRectImage = Raylib.LoadTexture("cryingchild3.png");
Vector2 movement = Vector2.Zero;

while (!Raylib.WindowShouldClose())
{

    //if playerRectX/Y = wall.X/.Y speed = 0.

    Raylib.BeginDrawing();

    if (GameState == "Menu")
    {
        Raylib.DrawText(("Press [ENTER] to enter."), 200, 80, 20, Color.RED);
        if (Raylib.IsKeyPressed(KeyboardKey.KEY_ENTER))
        {
            GameState = "Labyrinth";
            CameraReal = true;
        }
    }

    if (GameState == "Labyrinth")
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

        playerRectX += movement.X;
        playerRectY += movement.Y;

        if (CheckWallCollision(playerRect, walls))
        {
            movement.X -= movement.X;
        }

        if (CheckWallCollision(playerRect, walls))
        {
            movement.Y -= movement.Y;
        }

        Raylib.ClearBackground(Color.BLACK);

        if (CameraReal == true)
        {
            camera.target = new Vector2(playerRectX + playerSizeX / 2, playerRectY + playerSizeY / 2);
            Raylib.BeginMode2D(camera);
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

            Raylib.EndMode2D();
        }
    }

    Raylib.EndDrawing();
}

static bool CheckWallCollision(Rectangle playerRect, List<Rectangle> walls)
{
    foreach (Rectangle r in walls)
    {
        if (Raylib.CheckCollisionRecs(playerRect, r))
        {
            return true;
        }
    }

    return false;
}