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

bool redBoots = false;
bool Jesus = false;
bool Lookies = false;

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

// Combat
int playerhp = 100;
int enemyhp = 50;
int accuracy;

float playerSizeX = 66;
float playerSizeY = 53;

int tileSize = 200;

Rectangle playerRect = new Rectangle(400, -300, playerSizeY, playerSizeX);

Camera2D camera = new Camera2D();
camera.offset = new Vector2(GameX / 2, GameY / 2);
camera.rotation = 0.0f;
camera.zoom = 1.0f;

int[,] sceneData = {
{1,0,0,0,0,0,5,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
{1,0,0,0,2,0,4,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
{1,0,0,0,0,0,6,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
{1,0,0,0,0,0,3,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
{1,0,0,0,0,0,7,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
{1,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
{1,0,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3,1,0,0,1,0,0,0,0,3,1},
{1,0,0,1,1,1,1,1,1,1,0,0,0,0,2,0,0,0,0,1,1,1,1,0,1,1,1,1,1,1,1,1},
{1,0,1,1,0,0,2,1,0,1,0,0,1,1,1,1,1,1,0,0,0,0,0,0,1,0,0,0,0,0,0,1},
{1,0,1,3,0,1,0,0,0,1,0,0,1,3,0,0,0,1,1,1,1,1,1,1,1,0,0,0,0,2,0,1},
{1,0,1,1,1,1,1,1,0,1,0,2,1,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,0,0,1},
{1,0,0,2,0,0,0,0,0,1,0,0,1,1,1,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,1},
{1,1,1,1,1,1,1,1,1,1,0,0,0,0,1,1,1,0,1,1,1,0,0,1,1,1,1,0,0,0,0,1},
{1,0,0,0,0,0,0,0,0,1,0,0,0,0,1,0,0,0,0,1,0,2,0,0,1,0,1,0,1,1,1,1},
{1,0,0,0,0,0,0,2,0,1,1,1,0,0,1,0,0,0,0,1,0,0,0,0,1,0,1,2,1,0,0,4},
{1,0,1,1,1,1,1,0,0,0,0,0,0,0,1,0,0,0,0,1,1,1,1,0,0,0,1,0,1,0,0,4},
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
List<Rectangle> enemies = new();
List<Rectangle> collectibles = new();
List<Rectangle> victory = new();
List<Rectangle> redBoot = new();
List<Rectangle> Lookie = new();
List<Rectangle> Jesu = new();

{

    for (int y = 0; y < sceneData.GetLength(0); y++)
    {
        for (int x = 0; x < sceneData.GetLength(1); x++)
        {
            if (sceneData[y, x] == 1)
            {
                Rectangle r = new Rectangle(x * tileSize, y * tileSize, tileSize, tileSize);
                walls.Add(r);

            }
            if (sceneData[y, x] == 2)
            {

                Rectangle e = new Rectangle(x * tileSize, y * tileSize, tileSize, tileSize);
                enemies.Add(e);

            }
            if (sceneData[y, x] == 3)
            {

                Rectangle c = new Rectangle(x * tileSize, y * tileSize, tileSize, tileSize);
                collectibles.Add(c);

            }
            if (sceneData[y, x] == 4)
            {
                Rectangle v = new Rectangle(x * tileSize, y * tileSize, tileSize, tileSize);
                victory.Add(v);

            }
            if (sceneData[y, x] == 5)
            {
                Rectangle rb = new Rectangle(x * tileSize, y * tileSize, tileSize, tileSize);
                redBoot.Add(rb);

            }
            if (sceneData[y, x] == 6)
            {
                Rectangle l = new Rectangle(x * tileSize, y * tileSize, tileSize, tileSize);
                Lookie.Add(l);

            }
            if (sceneData[y, x] == 7)
            {
                Rectangle j = new Rectangle(x * tileSize, y * tileSize, tileSize, tileSize);
                Jesu.Add(j);

            }
        }
    }
}


Texture2D enemyImage = Raylib.LoadTexture("pictures/enemy.png");
Texture2D collectibleImage = Raylib.LoadTexture("pictures/star.png");
Texture2D playerRectImage = Raylib.LoadTexture("pictures/cryingchild3.png");
Texture2D wallImage = Raylib.LoadTexture("pictures/Bricks.png");
Texture2D backgroundImage = Raylib.LoadTexture("pictures/Background.png");
Texture2D goalImage = Raylib.LoadTexture("pictures/goalImage.png");
Texture2D jesusImage = Raylib.LoadTexture("pictures/JESUS.png");
Texture2D redbootsImage = Raylib.LoadTexture("pictures/REDBOOTS.png");
Texture2D lookiesImage = Raylib.LoadTexture("pictures/LOOKIES.png");
Vector2 movement = Vector2.Zero;

while (!Raylib.WindowShouldClose())
{

    Raylib.BeginDrawing();

    if (GameState == "Menu")
    {
        Raylib.ClearBackground(Color.BLACK);
        Raylib.DrawText("Press [SPACE] to enter.", 200, 80, 20, Color.RED);
        if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE))
        {
            GameState = "NamePick";
        }
    }

    if (GameState == "NamePick")
    {
        Raylib.DrawText("Welcome to my super scary labyrinth! [Space]", 50, 200, 20, Color.RED);
        if (waittime > 0)
        {
            waittime--;
            Console.WriteLine(waittime);
        }
        if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE) && waittime == 0 && currentDialogue == 1)
        {
            Raylib.DrawText("Don't be scared now! [Space]", 50, 240, 20, Color.RED);
            currentDialogue = 2;
            waittime = framerate / 3;
        }
        if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE) && waittime == 0 && currentDialogue == 2)
        {
            Raylib.DrawText(("(It's pretty scary) [Space]"), 50, 280, 20, Color.RED);
            currentDialogue = 3;
            waittime = framerate / 3;
        }
        if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE) && waittime == 0 && currentDialogue == 3)
        {
            GameState = "Labyrinth";
            CameraReal = true;
        }
    }

    if (GameState == "Labyrinth")
    {

        Rectangle enemiesRect = CheckCollisions(playerRect, enemies);
        if (enemiesRect.width != 0)
        {
            GameState = "Battle";
            enemies.Remove(enemiesRect);
            for (int y = 0; y < sceneData.GetLength(0); y++)
            {
                for (int x = 0; x < sceneData.GetLength(1); x++)
                {
                    if (sceneData[(int)enemiesRect.y / tileSize, (int)enemiesRect.x / tileSize] == 2)
                    {
                        sceneData[(int)enemiesRect.y / tileSize, (int)enemiesRect.x / tileSize] = 0;
                    }
                }
            }
        }

        Rectangle redBootRect = CheckCollisions(playerRect, redBoot);
        if (redBootRect.width != 0)
        {
            redBoots = true;
            redBoot.Remove(redBootRect);
            for (int y = 0; y < sceneData.GetLength(0); y++)
            {
                for (int x = 0; x < sceneData.GetLength(1); x++)
                {
                    if (sceneData[(int)redBootRect.y / tileSize, (int)redBootRect.x / tileSize] == 5)
                    {
                        sceneData[(int)redBootRect.y / tileSize, (int)redBootRect.x / tileSize] = 0;
                    }
                }
            }
        }
        Rectangle LookiesRect = CheckCollisions(playerRect, Lookie);
        if (LookiesRect.width != 0)
        {
            Lookies = true;
            Lookie.Remove(LookiesRect);
            for (int y = 0; y < sceneData.GetLength(0); y++)
            {
                for (int x = 0; x < sceneData.GetLength(1); x++)
                {
                    if (sceneData[(int)LookiesRect.y / tileSize, (int)LookiesRect.x / tileSize] == 5)
                    {
                        sceneData[(int)LookiesRect.y / tileSize, (int)LookiesRect.x / tileSize] = 0;
                    }
                }
            }
        }
        Rectangle JesusRect = CheckCollisions(playerRect, Lookie);
        if (LookiesRect.width != 0)
        {
            Lookies = true;
            Lookie.Remove(LookiesRect);
            for (int y = 0; y < sceneData.GetLength(0); y++)
            {
                for (int x = 0; x < sceneData.GetLength(1); x++)
                {
                    if (sceneData[(int)LookiesRect.y / tileSize, (int)LookiesRect.x / tileSize] == 5)
                    {
                        sceneData[(int)LookiesRect.y / tileSize, (int)LookiesRect.x / tileSize] = 0;
                    }
                }
            }
        }

        Rectangle collectibleRect = CheckCollisions(playerRect, collectibles);
        if (collectibleRect.width != 0)
        {
            points += 3;
            collectibles.Remove(collectibleRect);
            for (int y = 0; y < sceneData.GetLength(0); y++)
            {
                for (int x = 0; x < sceneData.GetLength(1); x++)
                {
                    if (sceneData[(int)collectibleRect.y / tileSize, (int)collectibleRect.x / tileSize] == 3)
                    {
                        sceneData[(int)collectibleRect.y / tileSize, (int)collectibleRect.x / tileSize] = 0;
                    }
                }
            }
        }

        Rectangle victoryRect = CheckCollisions(playerRect, victory);
        if (victoryRect.width != 0)
        {
            points += 10;
            victory.Remove(victoryRect);
            GameState = "Victory";
            for (int y = 0; y < sceneData.GetLength(0); y++)
            {
                for (int x = 0; x < sceneData.GetLength(1); x++)
                {
                    if (sceneData[(int)victoryRect.y / tileSize, (int)victoryRect.x / tileSize] == 4)
                    {
                        sceneData[(int)victoryRect.y / tileSize, (int)victoryRect.x / tileSize] = 0;
                    }
                }
            }
        }

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

        if (redBoots == false)
        {
            movement *= speed;
        }
        else if (redBoots == true)
        {
            movement *= speed + 5;
        }

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
                    if (sceneData[y, x] == 4)
                    {
                        Raylib.DrawTexture(goalImage, x * tileSize, y * tileSize, Color.WHITE);
                    }
                    if (sceneData[y, x] == 5)
                    {
                        Raylib.DrawTexture(redbootsImage, x * tileSize, y * tileSize, Color.WHITE);
                    }
                    if (sceneData[y, x] == 6)
                    {
                        Raylib.DrawTexture(lookiesImage, x * tileSize, y * tileSize, Color.WHITE);
                    }
                    if (sceneData[y, x] == 7)
                    {
                        Raylib.DrawTexture(jesusImage, x * tileSize, y * tileSize, Color.WHITE);
                    }
                }
            }

            Raylib.DrawTexture(playerRectImage, (int)playerRect.x, (int)playerRect.y, Color.WHITE);

            Raylib.DrawText(($"Points:{points}"), (int)playerRect.x - 350, (int)playerRect.y - 350, 20, Color.YELLOW);

            Raylib.EndMode2D();

        }
    }

    if (GameState == "Battle")
    {
        var random = new Random();
        Raylib.ClearBackground(Color.BLACK);
        Fighter player = new Fighter();
        Fighter enemy = new Fighter();



        if (player.hp > 0 && enemy.hp > 0)
        {
            Raylib.DrawText(($"Your health:{player.hp}"), 50, 760, 25, Color.RED);
            Raylib.DrawText(($"Foe health:{enemy.hp}"), 50, 800, 25, Color.RED);

            if (BattleState == "Menu")
            {
                Raylib.DrawText(("A life-threatening foe has picked a fight with you"), 50, 100, 25, Color.RED);
                Raylib.DrawText(("What is your decison?"), 50, 140, 25, Color.RED);
                Raylib.DrawText(("[C]arve or [P]uncture"), 50, 180, 25, Color.RED);

                if (Raylib.IsKeyPressed(KeyboardKey.KEY_C))
                {
                    BattleState = "Carve";
                }
                else if (Raylib.IsKeyPressed(KeyboardKey.KEY_P))
                {
                    BattleState = "Puncture";
                }

                if (BattleState == "Carve")
                {
                    accuracy = generator.Next(1, 10);
                    if (accuracy > 2)
                    {
                        AttackType = "CarveHit";
                        BattleState = "EnemyAttack";
                        player.LightAttack(enemy);
                    }
                    else
                    {
                        AttackType = "CarveMiss";
                        BattleState = "EnemyAttack";
                    }
                }
                else if (BattleState == "Puncture")
                {
                    accuracy = generator.Next(1, 10);
                    if (accuracy > 4)
                    {
                        AttackType = "PunctureHit";
                        BattleState = "EnemyAttack";
                        player.HeavyAttack(enemy);
                    }
                    else
                    {
                        AttackType = "PunctureMiss";
                        BattleState = "EnemyAttack";
                        player.hp -= 5;
                    }
                }
                if (BattleState == "EnemyAttack")
                {
                    enemy.LightAttack(player);
                    BattleState = "Menu";
                }

                if (AttackType == "PunctureMiss")
                {
                    Raylib.ClearBackground(Color.BLACK);
                    Raylib.DrawText(($"You thrust your sword but lack confidence, and drop it"), 50, 220, 25, Color.WHITE);
                    Raylib.DrawText(($"on your toe,dealing 5 damage to yourself."), 50, 248, 25, Color.WHITE);
                    if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE))
                    {
                        BattleState = "Menu";
                        AttackType = "None";
                    }
                }
                else if (AttackType == "PunctureHit")
                {
                    Raylib.ClearBackground(Color.BLACK);
                    Raylib.DrawText(($"You thrust your sword into the foe with confidence,"), 50, 220, 25, Color.WHITE);
                    Raylib.DrawText(($"hitting the foe in the heart, dealing heavy damage."), 50, 248, 25, Color.WHITE);
                    if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE))
                    {
                        BattleState = "Menu";
                        AttackType = "None";
                    }
                }
                else if (AttackType == "CarveMiss")
                {
                    Raylib.ClearBackground(Color.BLACK);
                    Raylib.DrawText(($"You swing your sword, but your confidence"), 50, 220, 25, Color.WHITE);
                    Raylib.DrawText(($"was lacking and you missed."), 50, 248, 25, Color.WHITE);
                    if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE))
                    {
                        BattleState = "Menu";
                        AttackType = "None";
                    }
                }
                else if (AttackType == "CarveHit")
                {
                    Raylib.ClearBackground(Color.BLACK);
                    Raylib.DrawText(($"You swing your sword confidently, dealing moderate damage."), 50, 220, 25, Color.WHITE);
                    if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE))
                    {
                        BattleState = "Menu";
                        AttackType = "None";
                    }
                }

            }
        }
        else if (enemyhp <= 0)
        {
            points += 1;
            playerhp = 100;
            enemyhp = 50;
            Raylib.ClearBackground(Color.BLACK);
            AttackType = "None";
            GameState = "Labyrinth";
            BattleState = "Menu";
        }
        else if (playerhp <= 0)
        {
            GameState = "Loser";
        }
    }
    if (GameState == "Loser")
    {
        Raylib.ClearBackground(Color.BLACK);
        Raylib.DrawText(("Man you suck ass at this, try again, or don't,"), 50, 220, 25, Color.RED);
        Raylib.DrawText(($"I couldn't care less tbh. GG's."), 50, 248, 25, Color.RED);
        Raylib.DrawText(($"I'll deduct a point for the lackluster performance."), 50, 276, 25, Color.RED);
        Raylib.DrawText(($"[Space] to respawn."), 50, 304, 25, Color.RED);

        if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE))
        {
            GameState = "Labyrinth";
            points -= 1;
            playerRect.x = 400;
            playerRect.y = -300;
        }
    }
    if (GameState == "Victory")
    {
        Raylib.ClearBackground(Color.BLACK);
        Raylib.DrawText(("+10 Points!"), 50, 170, 25, Color.WHITE);
        Raylib.DrawText(("Congratulations, you have showed at least some competence!"), 50, 220, 25, Color.RED);
        Raylib.DrawText(($"You gained a total of {points} points!"), 50, 248, 25, Color.RED);
        Raylib.DrawText(($"Uhh, anyway, you'll have to leave, you kinda stink."), 50, 276, 25, Color.RED);
        Raylib.DrawText(($"[Esc] to exit."), 50, 304, 25, Color.RED);
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

static Rectangle CheckCollisions(Rectangle playerRect, List<Rectangle> hitBoxes)
{
    foreach (Rectangle l in hitBoxes)
    {
        if (Raylib.CheckCollisionRecs(playerRect, l))
        {
            return l;
        }
    }

    foreach (Rectangle j in hitBoxes)
    {
        if (Raylib.CheckCollisionRecs(playerRect, j))
        {
            return j;
        }
    }

    foreach (Rectangle rb in hitBoxes)
    {
        if (Raylib.CheckCollisionRecs(playerRect, rb))
        {
            return rb;
        }
    }

    foreach (Rectangle v in hitBoxes)
    {
        if (Raylib.CheckCollisionRecs(playerRect, v))
        {
            return v;
        }
    }

    foreach (Rectangle c in hitBoxes)
    {
        if (Raylib.CheckCollisionRecs(playerRect, c))
        {
            return c;
        }
    }

    foreach (Rectangle e in hitBoxes)
    {
        if (Raylib.CheckCollisionRecs(playerRect, e))
        {
            return e;
        }
    }

    return new Rectangle();
}