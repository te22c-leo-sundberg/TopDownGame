using Microsoft.VisualBasic;
using Raylib_cs;
using System.Numerics;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Security.Cryptography.X509Certificates;

Random generator = new Random();

float GameX = 900;
float GameY = 900;

int framerate = 60;

int waittime = framerate / 3;

Raylib.InitWindow((int)GameX, (int)GameY, "(‿ˠ‿)");
Raylib.SetTargetFPS(framerate);

bool CameraReal = false;

int currentDialogue = 1;

Player player1 = new Player();

Fighter player = new Fighter();
Fighter enemy = new Fighter();

Rectangle playerRect = new Rectangle(400, -300, player1.SizeY, player1.SizeX);

Camera2D camera = new Camera2D();
camera.offset = new Vector2(GameX / 2, GameY / 2);
camera.rotation = 0.0f;
camera.zoom = 1.0f;

List<Rectangle> walls = new();
List<Rectangle> enemies = new();
List<Rectangle> collectibles = new();
List<Rectangle> victory = new();
List<Rectangle> redBoot = new();
List<Rectangle> Lookie = new();
List<Rectangle> Jesu = new();

player1.Generation(Jesu, Lookie, redBoot, victory, collectibles, enemies, walls);


while (!Raylib.WindowShouldClose())
{

    Raylib.BeginDrawing();

    if (player1.GameState == "Menu")
    {
        Raylib.ClearBackground(Color.BLACK);
        Raylib.DrawText("Press [SPACE] to enter.", 200, 80, 20, Color.RED);
        if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE))
        {
            player1.GameState = "NamePick";
        }
    }

    if (player1.GameState == "NamePick")
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
            player1.GameState = "Labyrinth";
            CameraReal = true;
        }
    }

    if (player1.GameState == "Labyrinth")
    {

        player1.CheckCollision(Jesu, Lookie, redBoot, victory, collectibles, enemies, walls);

        camera.target = new Vector2(player1.playerRect.x + player1.SizeX / 2, player1.playerRect.x + player1.SizeY / 2);

        Raylib.ClearBackground(Color.BLACK);

        if (CameraReal == true)
        {
            // draw outside of beginmode2d to make it not affected, if player moves camera updates before player
            camera.target = new Vector2(player1.playerRect.x + player1.SizeX / 2, player1.playerRect.y + player1.SizeY / 2);
            Raylib.BeginMode2D(camera);

            player1.Textures();

            player1.Update(walls);

            Raylib.EndMode2D();

        }
    }
    
    if (player1.GameState == "Battle")
{
        player.BattleMode(player, enemy, player1);
}

    player1.FightResult(player1);

    if (player1.GameState == "Victory")
    {
        Raylib.ClearBackground(Color.BLACK);
        Raylib.DrawText(("+10 Points!"), 50, 100, 25, Color.WHITE);
        Raylib.DrawText(("Congratulations, you have showed at least some competence!"), 50, 180, 25, Color.RED);
        Raylib.DrawText(($"You gained a total of {player1.points} points!"), 50, 220, 25, Color.RED);
        Raylib.DrawText(($"Uhh, anyway, you'll have to leave, you kinda stink."), 50, 260, 25, Color.RED);
        Raylib.DrawText(($"[Esc] to exit."), 50, 340, 25, Color.WHITE);
    }

    player1.StatDisplay();

    Raylib.EndDrawing();
}