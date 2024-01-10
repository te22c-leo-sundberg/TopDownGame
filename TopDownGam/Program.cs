using Microsoft.VisualBasic;
using Raylib_cs;
using System.Numerics;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Security.Cryptography.X509Certificates;

Random generator = new Random();

string GameState = "Menu";

float GameX = 900;
float GameY = 900;

Raylib.InitWindow((int)GameX, (int)GameY, "(‿ˠ‿)");
Raylib.SetTargetFPS(60);

bool CameraReal = false;

int points = 0;

float speed = 5;

float playerSizeX = 66;
float playerSizeY = 53;

int tileSize = 200;

Rectangle playerRect = new Rectangle( 400, -300, playerSizeY, playerSizeX);

Camera2D camera = new Camera2D();
camera.offset = new Vector2(GameX / 2, GameY / 2);
camera.rotation = 0.0f;
camera.zoom = 1.0f;

int[,] sceneData = {
{1,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
{1,0,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3,1,0,0,1,0,0,0,0,3,1},
{1,0,0,1,1,1,1,1,1,1,0,0,0,0,2,0,0,0,0,1,1,1,1,0,1,1,1,1,1,1,1,1},
{1,0,1,1,0,0,2,1,0,1,0,0,1,1,1,1,1,1,0,0,0,0,0,0,1,0,0,0,0,0,0,1},
{1,0,1,3,0,1,0,0,0,1,0,0,1,3,0,0,0,1,1,1,1,1,1,1,1,0,0,0,0,2,0,1},
{1,0,1,1,1,1,1,1,0,1,0,2,1,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,0,0,1},
{1,0,0,2,0,0,0,0,0,1,0,0,1,1,1,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,1},
{1,1,1,1,1,1,1,1,1,1,0,0,0,0,1,1,1,0,1,1,1,0,0,1,1,1,1,0,0,0,0,1},
{1,0,0,0,0,0,0,0,0,1,0,0,0,0,1,0,0,0,0,1,0,2,0,0,1,0,1,0,1,1,1,1},
{1,0,0,0,0,0,0,2,0,1,1,1,0,0,1,0,0,0,0,1,0,0,0,0,1,0,1,2,1,0,0,0},
{1,0,1,1,1,1,1,0,0,0,0,0,0,0,1,0,0,0,0,1,1,1,1,0,0,0,1,0,1,0,0,0},
{1,0,1,0,0,3,1,0,0,0,0,0,2,0,1,1,1,2,1,1,3,0,1,1,1,1,1,0,1,0,0,1},
{1,0,1,0,1,1,1,1,1,1,1,0,0,0,0,0,0,0,0,1,0,0,1,0,0,0,1,0,1,0,0,1},
{1,0,1,2,1,0,0,0,0,0,1,0,1,1,0,0,0,0,0,1,0,0,1,0,0,3,1,2,1,0,0,1},
{1,0,1,0,1,0,0,0,2,0,1,0,3,1,0,0,1,0,0,1,0,0,1,2,1,1,1,0,1,0,0,1},
{1,0,1,0,1,0,0,1,0,0,1,1,1,1,0,0,1,0,0,2,0,0,1,0,1,0,0,0,1,0,0,1},
{1,0,1,0,0,0,0,1,0,0,0,0,0,0,0,0,1,0,1,1,1,1,1,0,1,0,1,1,1,0,0,1},
{1,0,0,0,0,0,0,1,0,0,0,0,2,0,0,0,1,0,0,0,2,0,0,0,1,0,0,2,0,0,0,1},
{1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
};

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

List<Rectangle> enemies = new();
{

    for (int y = 0; y < sceneData.GetLength(0); y++)
    {
        for (int x = 0; x < sceneData.GetLength(1); x++)
        {
            if (sceneData[y, x] == 2)
            {
                // Skapa en rektangel
                Rectangle e = new Rectangle(x * tileSize, y * tileSize, tileSize, tileSize);
                enemies.Add(e);
                // Lägg till den i listan
                // Raylib.DrawRectangle(x * tileSize, y * tileSize, tileSize, tileSize, Color.DARKGRAY);
            }
        }
    }
}

List<Rectangle> collectibles = new();
{

    for (int y = 0; y < sceneData.GetLength(0); y++)
    {
        for (int x = 0; x < sceneData.GetLength(1); x++)
        {
            if (sceneData[y, x] == 3)
            {
                // Skapa en rektangel
                Rectangle c = new Rectangle(x * tileSize, y * tileSize, tileSize, tileSize);
                collectibles.Add(c);
                // Lägg till den i listan
                // Raylib.DrawRectangle(x * tileSize, y * tileSize, tileSize, tileSize, Color.DARKGRAY);
            }
        }
    }
}

Texture2D enemyImage = Raylib.LoadTexture("enemy.png");
Texture2D collectibleImage = Raylib.LoadTexture("star.png");
Texture2D playerRectImage = Raylib.LoadTexture("cryingchild3.png");
Vector2 movement = Vector2.Zero;

while (!Raylib.WindowShouldClose())
{

    Raylib.BeginDrawing();

    if (GameState == "Menu")
    {
        Raylib.DrawText("Press [ENTER] to enter.", 200, 80, 20, Color.RED);
        if (Raylib.IsKeyPressed(KeyboardKey.KEY_ENTER))
        {
            GameState = "NamePick";
        }
    }

    if (GameState == "NamePick")
    {
        Raylib.DrawText("What is your name, brave soul? [Space]", 50, 200, 20, Color.RED);
        if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE))
        {
            Raylib.DrawText("You don't... want one? [Space]", 50, 200, 20, Color.RED);
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE))
            {
                Raylib.DrawText(("I hope you won't regret your decision. [Space]"), 50, 200, 20, Color.RED);
                if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE))
                {
                    GameState = "Labyrinth";
                    CameraReal = true;
                }
            }
        }
    }

    if (GameState == "Labyrinth")
    {

        camera.target = new Vector2(playerRect.x + playerSizeX / 2, playerRect.x + playerSizeY / 2);

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

        playerRect.x += movement.X;

        if (CheckWallCollision(playerRect, walls))
        {
            playerRect.x -= movement.X;
        }

        playerRect.y += movement.Y;

        if (CheckWallCollision(playerRect, walls))
        {
            playerRect.y -= movement.Y;
        }

        Rectangle collectibleRect = CheckCollectibleCollision(playerRect, collectibles); //checkar collisions och skapar rektangel ifall collision true
        if (collectibleRect.width != 0)
        {
            points += 3;
            collectibles.Remove(collectibleRect);
            for (int y = 0; y < sceneData.GetLength(0); y++)
            {
                for (int x = 0; x < sceneData.GetLength(1); x++)
                {
                    if (sceneData[(int)collectibleRect.y/tileSize,(int)collectibleRect.x/tileSize] == 3)
                    {
                    sceneData[(int)collectibleRect.y/tileSize,(int)collectibleRect.x/tileSize] = 0;
                    } 
                }
            }
        }

        Raylib.ClearBackground(Color.BLACK);

        if (CameraReal == true)
        {
            camera.target = new Vector2(playerRect.x + playerSizeX / 2, playerRect.y + playerSizeY / 2);
            Raylib.BeginMode2D(camera);

            for (int y = 0; y < sceneData.GetLength(0); y++)
            {
                for (int x = 0; x < sceneData.GetLength(1); x++)
                {
                    if (sceneData[y, x] == 1)
                    {
                        Raylib.DrawRectangle(x * tileSize, y * tileSize, tileSize, tileSize, Color.DARKGRAY);
                    }
                    if (sceneData[y, x] == 2)
                    {
                        Raylib.DrawTexture(enemyImage, x * tileSize, y * tileSize, Color.WHITE);
                    }
                    if (sceneData[y, x] == 3)
                    {
                        Raylib.DrawTexture(collectibleImage, x * tileSize, y * tileSize, Color.WHITE);
                    }
                }
            }

       
        
        if (CheckEnemyCollision(playerRect, enemies))
        {
            GameState = "Battle";
        }

            Raylib.DrawTexture(playerRectImage, (int)playerRect.x, (int)playerRect.y, Color.WHITE);

            Raylib.DrawText(($"Points:{points}"), (int)playerRect.x-350, (int)playerRect.y-350, 20, Color.YELLOW);

            Raylib.EndMode2D();
            
        }
    }

    if (GameState == "Battle")
    {
        Raylib.EndDrawing();
        // int playerhp = 100;
        // int enemyhp = 50;
        // int accuracy = 0;

        // var random = new Random();


        // Raylib.ClearBackground(Color.BLACK);

        // if (playerhp > 0 && enemyhp > 0)
        // {
        //     Raylib.DrawText(($"Your health:{playerhp}"), 100, 760, 25, Color.RED);
        //     Raylib.DrawText(($"Foe health:{enemyhp}"), 100, 800, 25, Color.RED);
        //     Raylib.DrawText(("A life-threatening foe has picked a fight with you"), 100, 100, 25, Color.RED);
        //     Raylib.DrawText(("What is your decison?"), 100, 140, 25, Color.RED);
        //     Raylib.DrawText(("[C]arve or [P]uncture"), 100, 180, 25, Color.RED);

        //     if(Raylib.IsKeyPressed(KeyboardKey.KEY_C))
        //     {
        //         Raylib.ClearBackground(Color.BLACK);
        //         accuracy = generator.Next(1,10);
        //         if (accuracy <2)
        //         {
        //             Raylib.DrawText(("You swing your sword but miss miserably."), 100, 100, 25, Color.RED);
        //         }
        //         else
        //         {
        //             int playerdamage = generator.Next(3,15);
        //             enemyhp -= playerdamage;
        //             enemyhp = Math.Max(0, enemyhp);
        //             Raylib.DrawText(($"You swing your sword confidently, damaging the foe for {playerdamage} damage."), 100, 100, 25, Color.RED);
        //         }
        //     }
        //     else if (Raylib.IsKeyPressed(KeyboardKey.KEY_P))
        //     {
        //         Raylib.ClearBackground(Color.BLACK);
        //         accuracy = generator.Next(1,10);
        //         if (accuracy < 5)
        //         {
        //         int playerdamage = generator.Next(7,20);
        //         enemyhp -= playerdamage;
        //         enemyhp = Math.Max(0, enemyhp);
        //         Raylib.DrawText(($"You thrust your sword, aiming for the foes weak point, your sword connects and deals {playerdamage} damage."), 100, 100, 25, Color.RED);
        //         }
        //         else
        //         {   
        //             playerhp -= 5;
        //             playerhp = Math.Max(0, playerhp);
        //             Raylib.DrawText(("You thrust your sword, aiming for the foes weak point, but lose your grip and take 5 damage"), 100, 100, 25, Color.RED);
        //         }
        //     }
        //     int enemydamage = generator.Next(2,13);
        //     playerhp -= enemydamage;
        //     playerhp = Math.Max(0, playerhp);
        //     Raylib.DrawText(($"The foe charges at you, dealing {enemydamage} damage before hastly retreating"), 100, 100, 25, Color.RED);

        // }
        // else
        // {
        //     points += 1;
        //     GameState = "Labyrinth";
        // }


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

static bool CheckEnemyCollision(Rectangle playerRect, List<Rectangle> enemies)
{
    foreach (Rectangle e in enemies)
    {
        if (Raylib.CheckCollisionRecs(playerRect, e))
        {
            return true;
        }
    }

    return false;
}

static Rectangle CheckCollectibleCollision(Rectangle playerRect, List<Rectangle> collectibles) //returnera rektangel
{
    foreach (Rectangle c in collectibles)
    {
        if (Raylib.CheckCollisionRecs(playerRect, c))
        {
            return c;
        }
    }

    return new Rectangle();
}