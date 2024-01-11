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

int framerate = 60;

string AttackType = "";

string BattleState = "Menu";

int waittime = framerate / 3;

Raylib.InitWindow((int)GameX, (int)GameY, "(‿ˠ‿)");
Raylib.SetTargetFPS(framerate);

bool CameraReal = false;

int currentDialogue = 1;

int points = 0;

float speed = 5;

int playerhp = 100;
int enemyhp = 50;
int accuracy;
int playerdamage;
int enemydamage;

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
{1,0,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3,1,0,0,1,0,0,0,0,3,1},
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
Texture2D wallImage = Raylib.LoadTexture("Bricks.png");
Texture2D backgroundImage = Raylib.LoadTexture("Background.png");
Vector2 movement = Vector2.Zero;

while (!Raylib.WindowShouldClose())
{

    Raylib.BeginDrawing();

    if (GameState == "Menu")
    {
        Raylib.DrawText("Press [SPACE] to enter.", 200, 80, 20, Color.RED);
        if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE))
        {
                GameState = "NamePick";
        } 
    }
    
if (GameState == "NamePick")
    {
        Raylib.DrawText("What is your name, brave soul? [Space]", 50, 200, 20, Color.RED);
        if  (waittime > 0)
        {
            waittime--;
            Console.WriteLine(waittime);
        }
        if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE) && waittime == 0 && currentDialogue == 1)
        {
            Raylib.DrawText("You don't... want one? [Space]", 50, 240, 20, Color.RED);
            currentDialogue = 2;
            waittime = framerate * 2;
        }
        if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE) && waittime == 0 && currentDialogue == 2)
        {
            Raylib.DrawText(("I hope you won't regret your decision. [Space]"), 50, 280, 20, Color.RED);
            currentDialogue = 3;
            waittime = framerate * 2;
        }
        if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE) && waittime == 0 && currentDialogue == 3)
        {
            GameState = "Labyrinth";
            CameraReal = true;
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
                    if (sceneData[y, x] == 0)
                    {
                        Raylib.DrawTexture(backgroundImage, x * tileSize,y * tileSize, Color.WHITE);
                    }
                    if (sceneData[y, x] == 1)
                    {
                        Raylib.DrawTexture(wallImage, x * tileSize,y * tileSize, Color.WHITE);
                    }
                    if (sceneData[y, x] == 2)
                    {
                        Raylib.DrawTexture(backgroundImage, x * tileSize,y * tileSize, Color.WHITE);
                        Raylib.DrawTexture(enemyImage, x * tileSize, y * tileSize, Color.WHITE);
                    }
                    if (sceneData[y, x] == 3)
                    {
                        Raylib.DrawTexture(backgroundImage, x * tileSize,y * tileSize, Color.WHITE);
                        Raylib.DrawTexture(collectibleImage, x * tileSize, y * tileSize, Color.WHITE);
                    }
                }
            }

       
        
        Rectangle enemiesRect = CheckEnemyCollision(playerRect, enemies); //checkar collisions och skapar rektangel ifall collision true
        if (enemiesRect.width != 0)
        {
            GameState = "Battle";
            enemies.Remove(enemiesRect);
            for (int y = 0; y < sceneData.GetLength(0); y++)
            {
                for (int x = 0; x < sceneData.GetLength(1); x++)
                {
                    if (sceneData[(int)enemiesRect.y/tileSize,(int)enemiesRect.x/tileSize] == 2)
                    {
                    sceneData[(int)enemiesRect.y/tileSize,(int)enemiesRect.x/tileSize] = 0;
                    } 
                }
            }
        }

            Raylib.DrawTexture(playerRectImage, (int)playerRect.x, (int)playerRect.y, Color.WHITE);

            Raylib.DrawText(($"Points:{points}"), (int)playerRect.x-350, (int)playerRect.y-350, 20, Color.YELLOW);

            Raylib.EndMode2D();
            
        }
    }

if (GameState == "Battle")
{

    

    var random = new Random();


    Raylib.ClearBackground(Color.BLACK);

    if (playerhp > 0 && enemyhp > 0)
    {
    Raylib.DrawText(($"Your health:{playerhp}"), 50, 760, 25, Color.RED);
    Raylib.DrawText(($"Foe health:{enemyhp}"), 50, 800, 25, Color.RED);

    if (BattleState == "Menu")
    {
        Raylib.DrawText(("A life-threatening foe has picked a fight with you"), 50, 100, 25, Color.RED);
        Raylib.DrawText(("What is your decison?"), 50, 140, 25, Color.RED);
        Raylib.DrawText(("[C]arve or [P]uncture"), 50, 180, 25, Color.RED);

        if (Raylib.IsKeyPressed(KeyboardKey.KEY_C))
        {
            BattleState = "CAttack";
        }
        else if (Raylib.IsKeyPressed(KeyboardKey.KEY_P))
        {
            BattleState = "PAttack";
        }
        
        if (BattleState == "CAttack")
        {
            accuracy = generator.Next(1,10);
            if (accuracy > 2)
            {
                playerdamage = generator.Next(3,15);
                enemyhp -= playerdamage;
                // enemyhp = Math.Max(0, enemyhp);
                BattleState = "EnemyAttack";
                AttackType = "CAttackHit";
            }
            else
            {
                BattleState = "EnemyAttack";
                AttackType = "CAttackMiss";
            }
        }
            else if (BattleState == "PAttack")
            {
                accuracy = generator.Next(1,10); //try to use waiting time and if space is pressed waiting time = 0, or make generator run only once so you dont deal multiple instances of damage instantly cuz thats bad
                if (accuracy > 5)
                {
                    playerdamage = generator.Next(5,22);
                    enemyhp -= playerdamage;
                    // enemyhp = Math.Max(0, enemyhp);
                    BattleState = "EnemyAttack";
                    AttackType = "PAttackHit";
                }
                else
                {
                    playerhp -= 5;
                    // playerhp = Math.Max(0, playerhp);
                    BattleState = "EnemyAttack";
                    AttackType = "PAttackMiss";
                }
            }
            if (BattleState == "EnemyAttack")
            {
                BattleState = "Menu";
                enemydamage = generator.Next(2,13);
                playerhp -= enemydamage;
                // playerhp = Math.Max(0, playerhp);
                // BattleState = "RoundSummary";
            }
            if (BattleState == "RoundSummary")
            {

                if (AttackType == "PAttackMiss")
                {
                    Raylib.DrawText(($"You thrust your sword but lack confidence, and drop it on your toe,/ndealing 5 damage to yourself./n[SPACE] to proceed."), 50, 100, 25, Color.RED);
                    if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE))
                    {
                        BattleState = "Menu";
                    }
                }
                else if (AttackType == "PAttackHit")
                {
                    Raylib.DrawText(($"You thrust your sword into the foe with confidence,/nhitting the foe in the heart, dealing heavy damage./n[SPACE] to proceed."), 50, 100, 25, Color.RED);
                    if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE))
                    {
                        BattleState = "Menu";
                    }
                }
                else if (AttackType == "CAttackMiss")
                {
                    Raylib.DrawText(($"You swing your sword, but your confidence was lacking and you missed./n[SPACE] to proceed."), 50, 100, 25, Color.RED); 
                    if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE))
                    {
                        BattleState = "Menu";
                    }
                }
                else if (AttackType == "CAttackHit")
                {
                    Raylib.DrawText(($"You swing your sword confidently, dealing moderate damage./n[SPACE] to proceed."), 50, 100, 25, Color.RED);
                    if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE))
                    {
                        BattleState = "Menu";
                    }
                }
            }
    }
    else
    {
        points += 1;
        playerhp = 100;
        enemyhp = 50;
        BattleState = "Menu";
        GameState = "Labyrinth";
    }
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

static Rectangle CheckEnemyCollision(Rectangle playerRect, List<Rectangle> enemies)
{
    foreach (Rectangle e in enemies)
    {
        if (Raylib.CheckCollisionRecs(playerRect, e))
        {
            return e;
        }
    }

    return new Rectangle();
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