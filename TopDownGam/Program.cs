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

int Framerate = 60;

int WaitTime = Framerate / 3;

Raylib.InitWindow((int)GameX, (int)GameY, "(‿ˠ‿)");
Raylib.SetTargetFPS(Framerate);

bool CameraReal = false;

int CurrentDialogue = 1;

Player Player1 = new Player();

Fighter Player = new Fighter();//creates 2 fighters, which will give both of them an hp int and other ints.
Fighter Enemy = new Fighter();

Rectangle playerRect = new Rectangle(400, -300, Player1.SizeY, Player1.SizeX);

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

Player1.Generation(Jesu, Lookie, redBoot, victory, collectibles, enemies, walls); //Places down hitboxes for all rectangles, neccessary to be drawn outside of labyrinth mode as otherwise, things dont dissapear after interacting with them.


while (!Raylib.WindowShouldClose())
{

    Raylib.BeginDrawing();

    if (Player1.GameState == "Menu") // Simple press [Space] to enter thingy. If key pressed then GameState = Entry
    {
        Raylib.ClearBackground(Color.BLACK);
        Raylib.DrawText("Press [SPACE] to enter.", 200, 80, 20, Color.RED);
        if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE))
        {
            Player1.GameState = "Entry";
        }
    }

    if (Player1.GameState == "Entry") //Added a countdown system to the Entry state as otherwise, pressing Space once would skip all dialogue instead of one, 20 frame cooldown on pressing space essentially and checking current dialogue
    {
        Raylib.DrawText("Welcome to my super scary labyrinth! [Space]", 50, 200, 20, Color.RED);
        if (WaitTime > 0)
        {
            WaitTime--;
            Console.WriteLine(WaitTime);
        }
        if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE) && WaitTime == 0 && CurrentDialogue == 1)
        {
            Raylib.DrawText("Don't be scared now! [Space]", 50, 240, 20, Color.RED);
            CurrentDialogue = 2;
            WaitTime = Framerate / 3;
        }
        if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE) && WaitTime == 0 && CurrentDialogue == 2)
        {
            Raylib.DrawText(("(It's pretty scary) [Space]"), 50, 280, 20, Color.RED);
            CurrentDialogue = 3;
            WaitTime = Framerate / 3;
        }
        if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE) && WaitTime == 0 && CurrentDialogue == 3) 
        {
            Player1.GameState = "Labyrinth";
            CameraReal = true;
        }
    } // After third dialogue is finished enables camera and switches to the labyrinth state

    if (Player1.GameState == "Labyrinth")
    {

        Player1.CheckCollision(Jesu, Lookie, redBoot, victory, collectibles, enemies, walls); //checks collisions between all interactable objects, and then updates the players stats etc depending on what you collided with

        camera.target = new Vector2(Player1.playerRect.x + Player1.SizeX / 2, Player1.playerRect.x + Player1.SizeY / 2);

        Raylib.ClearBackground(Color.BLACK);

        if (CameraReal == true)
        {
            // draw outside of beginmode2d to make it not affected, if player moves camera updates before player
            camera.target = new Vector2(Player1.playerRect.x + Player1.SizeX / 2, Player1.playerRect.y + Player1.SizeY / 2);
            Raylib.BeginMode2D(camera);
        
        } // creates a target for the camera to follow and enables it
            Player1.Textures(); //loads textures for all collidable things and collectibles

            Player1.Update(walls); //handles all changes to the playable character, draws texture, movement and collision with walls as it uses reverse movement

            Raylib.EndMode2D();

        
    }

    Player.BattleMode(Player, Enemy, Player1);//if player interacts with a enemy in checkcollision, this runs

    Player1.FightResult(Player1); //checks for the result of the fight, and then draws some text and adds to variables depending on the outcome, then switches gamestate back to labyrinth

    Player1.Victory(); //linked with checkcollision, if player collides with the goal, gamestate becomes victory and this code happens.

    Player1.StatDisplay(); //wanted an easy way of seeing if changes i made worked and if the items did stuff, so added this to show that.

    Raylib.EndDrawing();
}