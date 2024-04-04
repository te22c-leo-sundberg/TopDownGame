// using System.Drawing;

// public class Blocks
// {

// public Boolean Lookies = false;
// public Boolean redBoots = false;

// public int tileSize = 200;
// public int[,] sceneData = {
// {1,0,0,0,0,0,5,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
// {1,0,0,0,2,0,4,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
// {1,0,1,0,0,0,6,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
// {1,0,0,0,0,0,3,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
// {1,0,0,0,0,0,7,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
// {1,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
// {1,0,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3,1,0,0,1,0,0,0,0,3,1},
// {1,0,0,1,1,1,1,1,1,1,0,0,0,0,2,0,0,0,0,1,1,1,1,0,1,1,1,1,1,1,1,1},
// {1,0,1,1,0,0,2,1,0,1,0,0,1,1,1,1,1,1,0,0,0,0,0,0,1,0,0,0,0,0,0,1},
// {1,0,1,3,0,1,0,0,0,1,0,0,1,3,0,0,0,1,1,1,1,1,1,1,1,0,0,0,0,2,0,1},
// {1,0,1,1,1,1,1,1,0,1,0,2,1,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,0,0,1},
// {1,0,0,2,0,0,0,0,0,1,0,0,1,1,1,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,1},
// {1,1,1,1,1,1,1,1,1,1,0,0,0,0,1,1,1,0,1,1,1,0,0,1,1,1,1,0,0,0,0,1},
// {1,0,0,0,0,0,0,0,0,1,0,0,0,0,1,0,0,0,0,1,0,2,0,0,1,0,1,0,1,1,1,1},
// {1,0,0,0,0,0,0,2,0,1,1,1,0,0,1,0,0,0,0,1,0,0,0,0,1,0,1,2,1,0,0,4},
// {1,0,1,1,1,1,1,0,0,0,0,0,0,0,1,0,0,0,0,1,1,1,1,0,0,0,1,0,1,0,0,4},
// {1,0,1,0,0,3,1,0,0,0,0,0,2,0,1,1,1,2,1,1,3,0,1,1,1,1,1,0,1,0,0,1},
// {1,0,1,0,1,1,1,1,1,1,1,0,0,0,0,0,0,0,0,1,0,0,1,0,0,0,1,0,1,0,0,1},
// {1,0,1,2,1,0,0,0,0,0,1,0,1,1,0,0,0,0,0,1,0,0,1,0,0,3,1,2,1,0,0,1},
// {1,0,1,0,1,0,0,0,2,0,1,0,3,1,0,0,1,0,0,1,0,0,1,2,1,1,1,0,1,0,0,1},
// {1,0,1,0,1,0,0,1,0,0,1,1,1,1,0,0,1,0,0,2,0,0,1,0,1,0,0,0,1,0,0,1},
// {1,0,1,0,0,0,0,1,0,0,0,0,0,0,0,0,1,0,1,1,1,1,1,0,1,0,1,1,1,0,0,1},
// {1,0,0,0,0,0,0,1,0,0,0,0,2,0,0,0,1,0,0,0,2,0,0,0,1,0,0,2,0,0,0,1},
// {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
// };

// List<Rectangle> walls = new();
// List<Rectangle> enemies = new();
// List<Rectangle> collectibles = new();
// List<Rectangle> victory = new();
// List<Rectangle> redBoot = new();
// List<Rectangle> Lookie = new();
// List<Rectangle> Jesu = new();

// public void Generation(List<Rectangle> Jesu, List<Rectangle> Lookie, List<Rectangle> redBoot, List<Rectangle> victory, List<Rectangle> collectibles, List<Rectangle> enemies, List<Rectangle> walls)
// {

//     for (int y = 0; y < sceneData.GetLength(0); y++)
//     {
//         for (int x = 0; x < sceneData.GetLength(1); x++)
//         {
//             if (sceneData[y, x] == 1)
//             {
//                 Rectangle r = new Rectangle(x * tileSize, y * tileSize, tileSize, tileSize);
//                 walls.Add(r);

//             }
//             if (sceneData[y, x] == 2)
//             {

//                 Rectangle e = new Rectangle(x * tileSize, y * tileSize, tileSize, tileSize);
//                 enemies.Add(e);

//             }
//             if (sceneData[y, x] == 3)
//             {

//                 Rectangle c = new Rectangle(x * tileSize, y * tileSize, tileSize, tileSize);
//                 collectibles.Add(c);

//             }
//             if (sceneData[y, x] == 4)
//             {
//                 Rectangle v = new Rectangle(x * tileSize, y * tileSize, tileSize, tileSize);
//                 victory.Add(v);

//             }
//             if (sceneData[y, x] == 5)
//             {
//                 Rectangle rb = new Rectangle(x * tileSize, y * tileSize, tileSize, tileSize);
//                 redBoot.Add(rb);

//             }
//             if (sceneData[y, x] == 6)
//             {
//                 Rectangle l = new Rectangle(x * tileSize, y * tileSize, tileSize, tileSize);
//                 Lookie.Add(l);

//             }
//             if (sceneData[y, x] == 7)
//             {
//                 Rectangle j = new Rectangle(x * tileSize, y * tileSize, tileSize, tileSize);
//                 Jesu.Add(j);

//             }
//         }
//     }
// }

// public void CollisionCheck()
// {
//     Rectangle enemiesRect = CheckCollisions(player1.playerRect, enemies);
//         if (enemiesRect.width != 0)
//         {
//             GameState = "Battle";
//             enemies.Remove(enemiesRect);
//             for (int y = 0; y < sceneData.GetLength(0); y++)
//             {
//                 for (int x = 0; x < sceneData.GetLength(1); x++)
//                 {
//                     if (sceneData[(int)enemiesRect.y / tileSize, (int)enemiesRect.x / tileSize] == 2)
//                     {
//                         sceneData[(int)enemiesRect.y / tileSize, (int)enemiesRect.x / tileSize] = 0;
//                     }
//                 }
//             }
//         }

//         Rectangle redBootRect = CheckCollisions(player1.playerRect, redBoot);
//         if (redBootRect.width != 0)
//         {
//             redBoots = true;
//             redBoot.Remove(redBootRect);
//             for (int y = 0; y < sceneData.GetLength(0); y++)
//             {
//                 for (int x = 0; x < sceneData.GetLength(1); x++)
//                 {
//                     if (sceneData[(int)redBootRect.y / tileSize, (int)redBootRect.x / tileSize] == 5)
//                     {
//                         sceneData[(int)redBootRect.y / tileSize, (int)redBootRect.x / tileSize] = 0;
//                     }
//                 }
//             }
//         }
//         Rectangle LookiesRect = CheckCollisions(player1.playerRect, Lookie);
//         if (LookiesRect.width != 0)
//         {
//             Lookies = true;
//             Lookie.Remove(LookiesRect);
//             for (int y = 0; y < sceneData.GetLength(0); y++)
//             {
//                 for (int x = 0; x < sceneData.GetLength(1); x++)
//                 {
//                     if (sceneData[(int)LookiesRect.y / tileSize, (int)LookiesRect.x / tileSize] == 5)
//                     {
//                         sceneData[(int)LookiesRect.y / tileSize, (int)LookiesRect.x / tileSize] = 0;
//                     }
//                 }
//             }
//         }
//         Rectangle JesusRect = CheckCollisions(player1.playerRect, Lookie);
//         if (LookiesRect.width != 0)
//         {
//             Lookies = true;
//             Lookie.Remove(LookiesRect);
//             for (int y = 0; y < sceneData.GetLength(0); y++)
//             {
//                 for (int x = 0; x < sceneData.GetLength(1); x++)
//                 {
//                     if (sceneData[(int)LookiesRect.y / tileSize, (int)LookiesRect.x / tileSize] == 5)
//                     {
//                         sceneData[(int)LookiesRect.y / tileSize, (int)LookiesRect.x / tileSize] = 0;
//                     }
//                 }
//             }
//         }

//         Rectangle collectibleRect = CheckCollisions(player1.playerRect, collectibles);
//         if (collectibleRect.width != 0)
//         {
//             player1.points += 3;
//             collectibles.Remove(collectibleRect);
//             for (int y = 0; y < sceneData.GetLength(0); y++)
//             {
//                 for (int x = 0; x < sceneData.GetLength(1); x++)
//                 {
//                     if (sceneData[(int)collectibleRect.y / tileSize, (int)collectibleRect.x / tileSize] == 3)
//                     {
//                         sceneData[(int)collectibleRect.y / tileSize, (int)collectibleRect.x / tileSize] = 0;
//                     }
//                 }
//             }
//         }

//         Rectangle victoryRect = CheckCollisions(player1.playerRect, victory);
//         if (victoryRect.width != 0)
//         {
//             player1.points += 10;
//             victory.Remove(victoryRect);
//             GameState = "Victory";
//             for (int y = 0; y < sceneData.GetLength(0); y++)
//             {
//                 for (int x = 0; x < sceneData.GetLength(1); x++)
//                 {
//                     if (sceneData[(int)victoryRect.y / tileSize, (int)victoryRect.x / tileSize] == 4)
//                     {
//                         sceneData[(int)victoryRect.y / tileSize, (int)victoryRect.x / tileSize] = 0;
//                     }
//                 }
//             }
//         }
// }

// }