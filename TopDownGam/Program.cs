using Microsoft.VisualBasic;
using Raylib_cs;
using System.Numerics;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Security.Cryptography.X509Certificates;

float gameX = 900;
float gameY = 900;

int framerate = 60;

int waitTime = framerate / 3;

Raylib.InitWindow((int)gameX, (int)gameY, "(‿ˠ‿)");
Raylib.SetTargetFPS(framerate);

bool cameraReal = false;

int dialogueState = 1;

Player player1 = new Player();

Fighter player = new Fighter();//creates 2 fighters, which will give both of them an hp int and other ints.
Fighter enemy = new Fighter();

Rectangle playerRect = new Rectangle(400, -300, player1.sizeY, player1.sizeX);

Camera2D camera = new Camera2D();
camera.offset = new Vector2(gameX / 2, gameY / 2);
camera.rotation = 0.0f;
camera.zoom = 1.0f;

List<Rectangle> walls = new();
List<Rectangle> enemies = new();
List<Rectangle> collectibles = new();
List<Rectangle> victory = new();
List<Rectangle> redBoot = new();
List<Rectangle> lookie = new();
List<Rectangle> jesu = new();

player1.Generation(jesu, lookie, redBoot, victory, collectibles, enemies, walls); //Places down hitboxes for all rectangles, neccessary to be drawn outside of labyrinth mode as otherwise, things dont dissapear after interacting with them.


while (!Raylib.WindowShouldClose())
{

    Raylib.BeginDrawing();

    if (player1.gameState == "Menu") // Simple press [Space] to enter thingy. If key pressed then GameState = Entry
    {
        Raylib.ClearBackground(Color.BLACK);
        Raylib.DrawText("Press [SPACE] to enter.", 200, 80, 20, Color.RED);
        if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE))
        {
            player1.gameState = "Entry";
        }
    }

    if (player1.gameState == "Entry") //Added a countdown system to the Entry state as otherwise, pressing Space once would skip all dialogue instead of one, 20 frame cooldown on pressing space essentially and checking current dialogue
    {
        Raylib.ClearBackground(Color.BLACK);
        if (waitTime > 0)
        {
            waitTime--;
            Console.WriteLine(waitTime);
        }
        if (dialogueState == 1)
        {
            Raylib.DrawText("Welcome to my super scary labyrinth!", 50, 200, 20, Color.RED);
            Raylib.DrawText("Your goal is to pave your way through the labyrinth,", 50, 240, 20, Color.RED);
            Raylib.DrawText("picking up items to speed up your progression.", 50, 280, 20, Color.RED);
            Raylib.DrawText("Picking up stars will increase your points, so try", 50, 320, 20, Color.RED);
            Raylib.DrawText("to aim for a high point score to feel good about yourself", 50, 360, 20, Color.RED);
            Raylib.DrawText("Press [SPACE] to proceed.", 50, 400, 20, Color.WHITE);
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE) && waitTime == 0)
            {
                waitTime += 20;
                dialogueState = 2;
            }
        }
        if (dialogueState == 2)
        {
            Raylib.DrawText("Use [W] [A] [S] [D] or the [Arrow Keys] to move around.", 50, 200, 20, Color.RED);
            Raylib.DrawText("Press [SPACE] to proceed.", 50, 240, 20, Color.WHITE);
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE) && waitTime == 0)
            {
                player1.gameState = "Labyrinth";
                cameraReal = true;
            }
        }
    } // After third dialogue is finished enables camera and switches to the labyrinth state

    if (player1.gameState == "Labyrinth")
    {

        player1.CheckCollision(jesu, lookie, redBoot, victory, collectibles, enemies, walls); //checks collisions between all interactable objects, and then updates the players stats etc depending on what you collided with

        camera.target = new Vector2(player1.playerRect.x + player1.sizeX / 2, player1.playerRect.x + player1.sizeY / 2);

        Raylib.ClearBackground(Color.BLACK);

        if (cameraReal == true)
        {
            // draw outside of beginmode2d to make it not affected, if player moves camera updates before player
            camera.target = new Vector2(player1.playerRect.x + player1.sizeX / 2, player1.playerRect.y + player1.sizeY / 2);
            Raylib.BeginMode2D(camera);
        
        } // creates a target for the camera to follow and enables it
            player1.Textures(); //loads textures for all collidable things and collectibles

            player1.Update(walls); //handles all changes to the playable character, draws texture, movement and collision with walls as it uses reverse movement

            Raylib.EndMode2D();

        
    }

    player.BattleMode(player, enemy, player1);//if player interacts with a enemy in checkcollision, this runs

    player1.FightResult(player1); //checks for the result of the fight, and then draws some text and adds to variables depending on the outcome, then switches gamestate back to labyrinth

    player1.Victory(); //linked with checkcollision, if player collides with the goal, gamestate becomes victory and this code happens.

    player1.StatDisplay(); //wanted an easy way of seeing if changes i made worked and if the items did stuff, so added this to show that.

    Raylib.EndDrawing();
}