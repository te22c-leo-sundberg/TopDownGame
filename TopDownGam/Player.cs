using System.Numerics;
using Raylib_cs;
using System.Reflection.Metadata;
using Microsoft.VisualBasic;
using System.Security.Cryptography.X509Certificates;

public class Player
{
    public int sizeX = 53;
    public int sizeY = 66;
    public int speed = 5;
    public int points = 0;
    public int tileSize = 200;
    public int lookiesAccuracy = 1;
    public int accuracy;
    public Vector2 movement = Vector2.Zero;
    public Rectangle playerRect = new Rectangle(400, -300, 53, 66);
    public Boolean lookies = false;
    public Boolean jesus = false;
    public Boolean redBoots = false;
    public string gameState = "Menu";
    public Texture2D enemyImage = Raylib.LoadTexture("pictures/enemy.png");
    public Texture2D enemyGutsImage = Raylib.LoadTexture("pictures/enemyguts.png");
    public Texture2D collectibleImage = Raylib.LoadTexture("pictures/star.png");
    public Texture2D wallImage = Raylib.LoadTexture("pictures/Bricks.png");
    public Texture2D backgroundImage = Raylib.LoadTexture("pictures/Background.png");
    public Texture2D goalImage = Raylib.LoadTexture("pictures/goalImage.png");
    public Texture2D jesusImage = Raylib.LoadTexture("pictures/JESUS.png");
    public Texture2D redbootsImage = Raylib.LoadTexture("pictures/REDBOOTS.png");
    public Texture2D lookiesImage = Raylib.LoadTexture("pictures/LOOKIES.png");
    public int[,] sceneData = { //draws a map of numbers
{1,0,0,0,0,0,5,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
{1,0,0,0,2,0,4,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
{1,0,1,0,0,0,6,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
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
{1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},};
    // Decided on using an array for the map as I wanted to be able to easily tamper with it to change the layout

    public void StatDisplay()
    {
        if (gameState == "Labyrinth" || gameState == "Battle") //draws the stats for all items and important stats
        {
            Raylib.DrawText(($"Points:{points}"), 400, 750, 20, Color.YELLOW);
            Raylib.DrawText(($"Accuracy:{accuracy}"), 400, 780, 20, Color.GREEN);
            if (jesus == true)
            {
                Raylib.DrawText(($"Jesus = True"), 400, 810, 20, Color.GOLD);
            }
            else
            {
                Raylib.DrawText(($"Jesus = False"), 400, 810, 20, Color.GOLD);
            }
            if (lookies == true)
            {
                Raylib.DrawText(($"Lookies = True"), 400, 840, 20, Color.SKYBLUE);
            }
            else
            {
                Raylib.DrawText(($"Lookies = False"), 400, 840, 20, Color.SKYBLUE);
            }
            if (redBoots == true)
            {
                Raylib.DrawText(($"Red Boots = True"), 400, 870, 20, Color.RED);
            }
            else
            {
                Raylib.DrawText(($"Red Boots = False"), 400, 870, 20, Color.RED);
            }
        }

    }
    public void Generation(List<Rectangle> jesu, List<Rectangle> lookie, List<Rectangle> redBoot, List<Rectangle> victory, List<Rectangle> collectibles, List<Rectangle> enemies, List<Rectangle> walls)
    {//based on the numbers on sceneData array, creates rectangle the size of tilesize to make them all the same and easily changable, then adds them to their list

        for (int y = 0; y < sceneData.GetLength(0); y++) //gets length and number data of the array
        {
            for (int x = 0; x < sceneData.GetLength(1); x++)
            {
                Rectangle r = new Rectangle(x * tileSize, y * tileSize, tileSize, tileSize);
        //creates a rectangle, and based on the number in the sceneData array adds said rectangle to a certain list
                if (sceneData[y, x] == 1) walls.Add(r);
                if (sceneData[y, x] == 2) enemies.Add(r);
                if (sceneData[y, x] == 3) collectibles.Add(r);
                if (sceneData[y, x] == 4) victory.Add(r);
                if (sceneData[y, x] == 5) redBoot.Add(r);
                if (sceneData[y, x] == 6) lookie.Add(r);
                if (sceneData[y, x] == 7) jesu.Add(r);
                }
            }
        }

    public void Textures() //gets length and number data of the array
    {
        for (int y = 0; y < sceneData.GetLength(0); y++)
        {
            for (int x = 0; x < sceneData.GetLength(1); x++)
            {
        //loads a texture based on the number in the sceneData array, its size being tile size
                if (sceneData[y, x] == 1) Raylib.DrawRectangle(x * tileSize, y * tileSize, tileSize, tileSize, Color.DARKGRAY);
                if (sceneData[y, x] == 2) Raylib.DrawTexture(enemyImage, x * tileSize, y * tileSize, Color.WHITE);
                if (sceneData[y, x] == 3) Raylib.DrawTexture(collectibleImage, x * tileSize, y * tileSize, Color.WHITE);
                if (sceneData[y, x] == 4) Raylib.DrawTexture(goalImage, x * tileSize, y * tileSize, Color.WHITE);
                if (sceneData[y, x] == 5) Raylib.DrawTexture(redbootsImage, x * tileSize, y * tileSize, Color.WHITE);
                if (sceneData[y, x] == 6) Raylib.DrawTexture(lookiesImage, x * tileSize, y * tileSize, Color.WHITE);
                if (sceneData[y, x] == 7) Raylib.DrawTexture(jesusImage, x * tileSize, y * tileSize, Color.WHITE);
                if (sceneData[y, x] == 8) Raylib.DrawTexture(enemyGutsImage, x * tileSize, y * tileSize, Color.WHITE);
            }
        }
    }
    static Rectangle CheckCollisions(Rectangle playerRect, List<Rectangle> hitBoxes)
    {//checks collision between playerRect and rectangle r, if playerRect and r are colliding, returns r, otherwise, returns a new rectangle
        foreach (Rectangle r in hitBoxes)
        {
            if (Raylib.CheckCollisionRecs(playerRect, r)) return r;
        }

        return new Rectangle();
    }

    public void CheckCollision(List<Rectangle> jesu, List<Rectangle> lookie, List<Rectangle> redBoot, List<Rectangle> victory, List<Rectangle> collectibles, List<Rectangle> enemies, List<Rectangle> walls)
    {

        Rectangle enemiesRect = CheckCollisions(playerRect, enemies); // creates a rectangle where playerRect and rectangle r overlap, that value is sent by the foreach above
        if (enemiesRect.width != 0)//if this is greater than 0, aka not equal to 0, runs the code below
        {
            if (jesus == false) //checks if jesus is true, cause if it is, you will one shot the demon and instantly get a point, otherwise, puts your game state to battle
            {
                gameState = "Battle";
            }
            else
            {
                points += 1;
            }
            enemies.Remove(enemiesRect); //the enemiesRect is then removed, and below i check if the coordinates divided by tilesize will be a 2 on the array, and if it is, i change it to 8, which loads a guts texture
            if (sceneData[(int)enemiesRect.y / tileSize, (int)enemiesRect.x / tileSize] == 2)
            {
                sceneData[(int)enemiesRect.y / tileSize, (int)enemiesRect.x / tileSize] = 8;
            }
        }

        Rectangle redBootRect = CheckCollisions(playerRect, redBoot); //does the same thing as it did with enemiesRect
        if (redBootRect.width != 0)
        {
            redBoots = true; // enables red boots which boosts speed
            redBoot.Remove(redBootRect);//i will only mention the different things that happen in these
            if (sceneData[(int)redBootRect.y / tileSize, (int)redBootRect.x / tileSize] == 5) //gets the sceneData array number and changes it to nothing
            {
                sceneData[(int)redBootRect.y / tileSize, (int)redBootRect.x / tileSize] = 0;
            }
        }
        Rectangle lookieRect = CheckCollisions(playerRect, lookie);
        if (lookieRect.width != 0)
        {
            lookies = true; //enables lookies
            lookie.Remove(lookieRect);
            if (sceneData[(int)lookieRect.y / tileSize, (int)lookieRect.x / tileSize] == 6)
            {
                sceneData[(int)lookieRect.y / tileSize, (int)lookieRect.x / tileSize] = 0;
            }
        }
        Rectangle jesuRect = CheckCollisions(playerRect, jesu);
        if (jesuRect.width != 0)
        {
            jesus = true; //enables jesus
            jesu.Remove(jesuRect);
            if (sceneData[(int)jesuRect.y / tileSize, (int)jesuRect.x / tileSize] == 7)
            {
                sceneData[(int)jesuRect.y / tileSize, (int)jesuRect.x / tileSize] = 0;
            }
        }

        Rectangle collectibleRect = CheckCollisions(playerRect, collectibles);
        if (collectibleRect.width != 0)
        {
            points += 3; //3 points added
            collectibles.Remove(collectibleRect);
            if (sceneData[(int)collectibleRect.y / tileSize, (int)collectibleRect.x / tileSize] == 3)
            {
                sceneData[(int)collectibleRect.y / tileSize, (int)collectibleRect.x / tileSize] = 0;
            }
        }

        Rectangle victoryRect = CheckCollisions(playerRect, victory);
        if (victoryRect.width != 0)
        {
            points += 10; //10 points and changes gamestate to victory
            victory.Remove(victoryRect);
            gameState = "Victory";
            if (sceneData[(int)victoryRect.y / tileSize, (int)victoryRect.x / tileSize] == 4)
            {
                sceneData[(int)victoryRect.y / tileSize, (int)victoryRect.x / tileSize] = 0;
            }
        }
    }

    public void Update(List<Rectangle> walls)
    {

        UpdateMovementVector(walls);

        Draw();

    }

    private void UpdateMovementVector(List<Rectangle> walls)
    {
        movement = Vector2.Zero; //makes movement a vector

        if (Raylib.IsKeyDown(KeyboardKey.KEY_S) || Raylib.IsKeyDown(KeyboardKey.KEY_DOWN)) //adds or subracts from movement based on key pressed
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
            movement = Vector2.Normalize(movement); //normalizes movement, making movement the same even if youre going diagonally
        }

        if (redBoots == false) //movement times speed, adds more to speed if redboots is true
        {
            movement *= speed;
        }
        else if (redBoots == true)
        {
            movement *= speed + 5;
        }

        playerRect.y += movement.Y;
        playerRect.x += movement.X; //adds movement y to playerrect y and same for x


        static bool CheckWallCollision(Rectangle playerRect, List<Rectangle> walls) //checks for the collision between rectangles in walls and playerRect, then returns a bool
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

        if (CheckWallCollision(playerRect, walls)) //if the static bool above checks collisions and returns true, run this code reversing movement
        {
            playerRect.x -= movement.X;
        }
        if (CheckWallCollision(playerRect, walls))
        {
            playerRect.y -= movement.Y;
        }

    }

    public void FightResult(Player Player1)
    {
        if (Player1.gameState == "FightWon") //if fight is won, runs this
        {
            Raylib.ClearBackground(Color.BLACK);
            Raylib.DrawText(("You bested the foe through your courage and sheer will,"), 50, 100, 25, Color.RED);
            Raylib.DrawText(($"slashing its body in half, exposing a morbid mess of guts"), 50, 140, 25, Color.RED);
            Raylib.DrawText(($"and blue blood. Through the foes death, you replenished"), 50, 180, 25, Color.RED);
            Raylib.DrawText(($"your lost vitality and gained 1 point!"), 50, 220, 25, Color.RED);
            Raylib.DrawText(($"[Space] to return."), 50, 260, 25, Color.WHITE);
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE))
            {
                Player1.gameState = "Labyrinth";//adds a point, also changing gamestate to labyrinth
                Player1.points += 1;
            }
        }
        if (Player1.gameState == "FightLost")//if fight is lost, runs this
        {
            Raylib.ClearBackground(Color.BLACK);
            Raylib.DrawText(("You dissapoint me, however, as I do somewhat pity you"), 50, 100, 25, Color.RED);
            Raylib.DrawText(($"I'll kill the beast for you. Oh, and heres a tip, try to"), 50, 140, 25, Color.RED);
            Raylib.DrawText(($"find the goggles item to improve your accuracy, and while"), 50, 180, 25, Color.RED);
            Raylib.DrawText(($"you don't have it, I'd suggest relying on the less rng reliant"), 50, 220, 25, Color.RED);
            Raylib.DrawText(($"attack Carve, it deals slightly less damage, but is more accurate."), 50, 260, 25, Color.RED);
            Raylib.DrawText(($"[Space] to respawn."), 50, 300, 25, Color.WHITE);
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE))
            {
                Player1.gameState = "Labyrinth"; //throws player back into spawn and deducts a point, also changing gamestate to labyrinth
                Player1.points -= 1;
                Player1.playerRect.x = 400;
                Player1.playerRect.y = -300;
            }
        }
    }
    public void Draw() //draws the texture for the player and makes it follow playerRect
    {
        Texture2D playerRectImage = Raylib.LoadTexture("pictures/cryingchild3.png");
        Raylib.DrawTexture(playerRectImage, (int)playerRect.x, (int)playerRect.y, Color.WHITE);
    }
    public void Victory()
    {
        if (gameState == "Victory") //if you enter goal this runs
    {
        Raylib.ClearBackground(Color.BLACK);
        Raylib.DrawText(("+10 Points!"), 50, 100, 25, Color.WHITE);
        Raylib.DrawText(("Congratulations, you have showed at least some competence!"), 50, 180, 25, Color.RED);
        Raylib.DrawText(($"You gained a total of {points} points! Although I was expecting"), 50, 220, 25, Color.RED);
        Raylib.DrawText(($"a bit more, next time, look around more and go the opposite"), 50, 260, 25, Color.RED);
        Raylib.DrawText(($"way of the goal to find more stars and kill all enemies to"), 50, 300, 25, Color.RED);
        Raylib.DrawText(($"get more points, who knows, maybe you'll get a reward."), 50, 340, 25, Color.RED);
        Raylib.DrawText(($"[Esc] to exit."), 50, 380, 25, Color.WHITE);
    }
    }
}