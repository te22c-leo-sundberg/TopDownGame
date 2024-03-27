using System.Numerics;
using Raylib_cs;
using System.Reflection.Metadata;
using Microsoft.VisualBasic;

public class Player
{
    public int SizeX = 66;
    public int SizeY = 53;
    public int speed = 5;
    public int points = 0;
    public Vector2 movement = Vector2.Zero;
    public Rectangle playerRect = new Rectangle(400, -300, 66, 53);

    public void Update(List<Rectangle> walls)
    {
        
        // läs in movement
        UpdateMovementVector();
        // Gör x-movement-grejen
        
        if (CheckWallCollision(playerRect, walls))
        {
            playerRect.x -= movement.X;
        }

        if (CheckWallCollision(playerRect, walls))
        {
            playerRect.y -= movement.Y;
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

        Draw();
        // Gör y-movement-grejen

    }

    private void UpdateMovementVector()
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

        playerRect.x += movement.X;
        playerRect.y += movement.Y;

    }

    public void Draw()
    {
        Texture2D playerRectImage = Raylib.LoadTexture("pictures/cryingchild3.png");
        Raylib.DrawTexture(playerRectImage, (int)playerRect.x, (int)playerRect.y, Color.WHITE);
    }
}