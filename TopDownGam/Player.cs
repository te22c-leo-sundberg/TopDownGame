using System.Numerics;
using Raylib_cs;
using System.Reflection.Metadata;
using Microsoft.VisualBasic;
using System.Security.Cryptography.X509Certificates;

public class Player
{
public int SizeX = 53;
public int SizeY = 66;
public int speed = 5;
public int points = 0;
public int tileSize = 200;
public int lookiesAccuracy = 1;
public int accuracy;
public Vector2 movement = Vector2.Zero;
public Rectangle playerRect = new Rectangle(400, -300, 53, 66);
public Boolean Lookies = false;
public Boolean Jesus = false;
public Boolean redBoots = false;
public string GameState = "Menu";
public Texture2D enemyImage = Raylib.LoadTexture("pictures/enemy.png");
public Texture2D enemyGutsImage = Raylib.LoadTexture("pictures/enemyguts.png");
public Texture2D collectibleImage = Raylib.LoadTexture("pictures/star.png");
public Texture2D wallImage = Raylib.LoadTexture("pictures/Bricks.png");
public Texture2D backgroundImage = Raylib.LoadTexture("pictures/Background.png");
public Texture2D goalImage = Raylib.LoadTexture("pictures/goalImage.png");
public Texture2D jesusImage = Raylib.LoadTexture("pictures/JESUS.png");
public Texture2D redbootsImage = Raylib.LoadTexture("pictures/REDBOOTS.png");
public Texture2D lookiesImage = Raylib.LoadTexture("pictures/LOOKIES.png");
public int[,] sceneData = {
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
    if (GameState == "Labyrinth" || GameState == "Battle")
    {
        Raylib.DrawText(($"Points:{points}"), 400, 750, 20, Color.YELLOW);
        Raylib.DrawText(($"Accuracy:{accuracy}"), 400, 780, 20, Color.GREEN);
        if (Jesus == true)
        {
            Raylib.DrawText(($"Jesus = True"), 400, 810, 20, Color.GOLD );
        }
        else
        {
            Raylib.DrawText(($"Jesus = False"), 400, 810, 20, Color.GOLD);
        }
        if (Lookies == true)
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
public void Generation(List<Rectangle> Jesu, List<Rectangle> Lookie, List<Rectangle> redBoot, List<Rectangle> victory, List<Rectangle> collectibles, List<Rectangle> enemies, List<Rectangle> walls)
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

public void Textures()
{
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
                    if (sceneData[y, x] == 8)
                    {
                        Raylib.DrawTexture(enemyGutsImage, x * tileSize, y * tileSize, Color.WHITE);
                    }
                }
            }
}

public void CheckCollision(List<Rectangle> Jesu, List<Rectangle> Lookie, List<Rectangle> redBoot, List<Rectangle> victory, List<Rectangle> collectibles, List<Rectangle> enemies, List<Rectangle> walls)
{
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

    Rectangle enemiesRect = CheckCollisions(playerRect, enemies);
        if (enemiesRect.width != 0)
        {
            if (Jesus == false)
            {
                GameState = "Battle";
            }
            else
            {
                points += 1;
            }
            enemies.Remove(enemiesRect);
            // for (int y = 0; y < sceneData.GetLength(0); y++)
            // {
            //     for (int x = 0; x < sceneData.GetLength(1); x++)
            //     {
                    if (sceneData[(int)enemiesRect.y / tileSize, (int)enemiesRect.x / tileSize] == 2)
                    {
                        sceneData[(int)enemiesRect.y / tileSize, (int)enemiesRect.x / tileSize] = 8;
                    }
            //     }
            // }
        }

        Rectangle redBootRect = CheckCollisions(playerRect, redBoot);
        if (redBootRect.width != 0)
        {
            redBoots = true;
            redBoot.Remove(redBootRect);
            // for (int y = 0; y < sceneData.GetLength(0); y++)
            // {
            //     for (int x = 0; x < sceneData.GetLength(1); x++)
            //     {
                    if (sceneData[(int)redBootRect.y / tileSize, (int)redBootRect.x / tileSize] == 5)
                    {
                        sceneData[(int)redBootRect.y / tileSize, (int)redBootRect.x / tileSize] = 0;
                    }
            //     }
            // }
        }
        Rectangle LookieRect = CheckCollisions(playerRect, Lookie);
        if (LookieRect.width != 0)
        {
            Lookies = true;
            Lookie.Remove(LookieRect);
            // for (int y = 0; y < sceneData.GetLength(0); y++)
            // {
            //     for (int x = 0; x < sceneData.GetLength(1); x++)
            //     {
                    if (sceneData[(int)LookieRect.y / tileSize, (int)LookieRect.x / tileSize] == 6)
                    {
                        sceneData[(int)LookieRect.y / tileSize, (int)LookieRect.x / tileSize] = 0;
                    }
            //     }
            // }
        }
        Rectangle JesuRect = CheckCollisions(playerRect, Jesu);
        if (JesuRect.width != 0)
        {
            Jesus = true;
            Jesu.Remove(JesuRect);
            // for (int y = 0; y < sceneData.GetLength(0); y++)
            // {
            //     for (int x = 0; x < sceneData.GetLength(1); x++)
            //     {
                    if (sceneData[(int)JesuRect.y / tileSize, (int)JesuRect.x / tileSize] == 7)
                    {
                        sceneData[(int)JesuRect.y / tileSize, (int)JesuRect.x / tileSize] = 0;
                    }
            //     }
            // }
        }

        Rectangle collectibleRect = CheckCollisions(playerRect, collectibles);
        if (collectibleRect.width != 0)
        {
            points += 3;
            collectibles.Remove(collectibleRect);
            // for (int y = 0; y < sceneData.GetLength(0); y++)
            // {
            //     for (int x = 0; x < sceneData.GetLength(1); x++)
            //     {
                    if (sceneData[(int)collectibleRect.y / tileSize, (int)collectibleRect.x / tileSize] == 3)
                    {
                        sceneData[(int)collectibleRect.y / tileSize, (int)collectibleRect.x / tileSize] = 0;
                    }
            //     }
            // }
        }

        Rectangle victoryRect = CheckCollisions(playerRect, victory);
        if (victoryRect.width != 0)
        {
            points += 10;
            victory.Remove(victoryRect);
            GameState = "Victory";
            // for (int y = 0; y < sceneData.GetLength(0); y++)
            // {
            //     for (int x = 0; x < sceneData.GetLength(1); x++)
            //     {
                    if (sceneData[(int)victoryRect.y / tileSize, (int)victoryRect.x / tileSize] == 4)
                    {
                        sceneData[(int)victoryRect.y / tileSize, (int)victoryRect.x / tileSize] = 0;
                    }
            //     }
            // }
        }
}

    public void Update(List<Rectangle> walls)
    {
        
        // läs in movement
        UpdateMovementVector(walls);
        // Gör x-movement-grejen

        Draw();
        // Gör y-movement-grejen

    }

    private void UpdateMovementVector(List<Rectangle> walls)
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

        if (redBoots == false)
        {
            movement *= speed;
        }
        else if (redBoots == true)
        {
            movement *= speed + 5;
        }

        playerRect.y += movement.Y;
        playerRect.x += movement.X;
    

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

        if (CheckWallCollision(playerRect, walls))
        {
            playerRect.x -= movement.X;
        }
        if (CheckWallCollision(playerRect, walls))
        {
            playerRect.y -= movement.Y;
        }

    }

public void FightResult(Player player1)
{
        if (player1.GameState == "FightWon")
    {
        Raylib.ClearBackground(Color.BLACK);
        Raylib.DrawText(("You bested the foe through your courage and sheer will,"), 50, 100, 25, Color.RED);
        Raylib.DrawText(($"slashing its body in half, exposing a morbid mess of guts"), 50, 140, 25, Color.RED);
        Raylib.DrawText(($"and blue blood. Through the foes death, you replenished"), 50, 180, 25, Color.RED);
        Raylib.DrawText(($"your lost vitality and gained 1 point!"), 50, 220, 25, Color.RED);
        Raylib.DrawText(($"[Space] to return."), 50, 260, 25, Color.WHITE);
        if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE))
        {
            player1.GameState = "Labyrinth";
            player1.points += 1;
        }
    }
    if (player1.GameState == "FightLost")
    {
        Raylib.ClearBackground(Color.BLACK);
        Raylib.DrawText(("Man you suck ass at this, try again, or don't,"), 50, 100, 25, Color.RED);
        Raylib.DrawText(($"I couldn't care less tbh, but to save you some trouble"), 50, 140, 25, Color.RED);
        Raylib.DrawText(($"I'll kill the foe you lost to. That will cost one point."), 50, 180, 25, Color.RED);
        Raylib.DrawText(($"Oh, and you can't say no, so be happy, ungrateful swine."), 50, 220, 25, Color.RED);
        Raylib.DrawText(($"[Space] to respawn."), 50, 260, 25, Color.WHITE);
        if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE))
        {
            player1.GameState = "Labyrinth";
            player1.points -= 1;
            player1.playerRect.x = 400;
            player1.playerRect.y = -300;
        }
    }
}
    public void Draw()
    {
        Texture2D playerRectImage = Raylib.LoadTexture("pictures/cryingchild3.png");
        Raylib.DrawTexture(playerRectImage, (int)playerRect.x, (int)playerRect.y, Color.WHITE);
    }
}