using Raylib_cs;
public class Fighter
{
    Random generator = new Random();
    public int accuracy;
    public int hp = 100;
    public int LightAtkDmg = 15;
    public int HeavyAtkDmg = 25;
    public int EnemyAtkDmg = 10;
    public string BattleState = "Menu"; // migrate all combat stuff into this
    public string AttackType = "";
    public void LightAttack(Fighter target)
    {
        target.hp -= LightAtkDmg;
    }
    public void HeavyAttack(Fighter target)
    {
        target.hp -= HeavyAtkDmg;
    }
    public void EnemyAttack(Fighter target)
    {
        target.hp -= EnemyAtkDmg;
    }
//     public void Battle()
//     {
//         var random = new Random();
//         Raylib.ClearBackground(Color.BLACK);

//         if (hp > 0)
//         {
//             Raylib.DrawText(($"Your health:{hp}"), 50, 760, 25, Color.RED);
//             Raylib.DrawText(($"Foe health:{hp}"), 50, 800, 25, Color.RED);

//             if (BattleState == "Menu")
//             {
//                 Raylib.DrawText(("A life-threatening foe has picked a fight with you"), 50, 100, 25, Color.RED);
//                 Raylib.DrawText(("What is your decison?"), 50, 140, 25, Color.RED);
//                 Raylib.DrawText(("[C]arve or [P]uncture"), 50, 180, 25, Color.RED);

//                 if (Raylib.IsKeyPressed(KeyboardKey.KEY_C))
//                 {
//                     BattleState = "Carve";
//                 }
//                 else if (Raylib.IsKeyPressed(KeyboardKey.KEY_P))
//                 {
//                     BattleState = "Puncture";
//                 }

//             }
//             if (BattleState == "Carve")
//             {
//                 accuracy = generator.Next(1, 10);
//                 if (accuracy > 2)
//                 {
//                     AttackType = "CarveHit";
//                     BattleState = "EnemyAttack";
//                 }
//                 else
//                 {
//                     AttackType = "CarveMiss";
//                     BattleState = "EnemyAttack";
//                 }
//             }
//             else if (BattleState == "Puncture")
//             {
//                 accuracy = generator.Next(1, 10);
//                 if (accuracy > 4)
//                 {
//                     player.HeavyAttack(enemy);
//                     AttackType = "PunctureHit";
//                     BattleState = "EnemyAttack";
//                 }
//                 else
//                 {
//                     AttackType = "PunctureMiss";
//                     BattleState = "EnemyAttack";
//                     player.hp -= 5;
//                 }
//             }
//             if (BattleState == "EnemyAttack")
//             {
//                 enemy.EnemyAttack(player);
//                 BattleState = "WaitMode";
//             }
//             if (BattleState == "WaitMode")
//             {
//                 if (AttackType == "CarveHit")
//                 {
//                 Raylib.DrawText(("Press [Space] to proceed."), 50, 140, 25, Color.WHITE);
//                 }
//                 else if (AttackType == "PunctureHit" || AttackType == "CarveMiss" || AttackType == "PunctureMiss")
//                 {
//                 Raylib.DrawText(("Press [Space] to proceed."), 50, 180, 25, Color.WHITE);
//                 }
//                 if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE))
//                 {
//                     BattleState = "Menu";
//                     Raylib.ClearBackground(Color.BLACK);
//                 }
//             }
//             if (AttackType == "PunctureMiss")
//             {
//                 Raylib.ClearBackground(Color.BLACK);
//                 Raylib.DrawText(($"You thrust your sword but lack confidence, and drop it"), 50, 100, 25, Color.WHITE);
//                 Raylib.DrawText(($"on your toe,dealing 5 damage to yourself."), 50, 140, 25, Color.WHITE);
//                 // if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE))
//                 // {
//                 //     BattleState = "Menu";
//                 //     AttackType = "None";
//                 // }
//             }
//             else if (AttackType == "PunctureHit")
//             {
//                 Raylib.ClearBackground(Color.BLACK);
//                 Raylib.DrawText(($"You thrust your sword into the foe with confidence,"), 50, 100, 25, Color.WHITE);
//                 Raylib.DrawText(($"hitting the foe in the heart, dealing heavy damage."), 50, 140, 25, Color.WHITE);
//                 // if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE))
//                 // {
//                 //     BattleState = "Menu";
//                 //     AttackType = "None";
//                 // }
//             }
//             else if (AttackType == "CarveMiss")
//             {
//                 Raylib.ClearBackground(Color.BLACK);
//                 Raylib.DrawText(($"You swing your sword, but your confidence"), 50, 100, 25, Color.WHITE);
//                 Raylib.DrawText(($"was lacking and you missed."), 50, 140, 25, Color.WHITE);
//                 // if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE))
//                 // {
//                 //     BattleState = "Menu";
//                 //     AttackType = "None";
//                 // }
//             }
//             else if (AttackType == "CarveHit")
//             {
//                 Raylib.ClearBackground(Color.BLACK);
//                 Raylib.DrawText(($"You swing your sword confidently, dealing moderate damage."), 50, 100, 25, Color.WHITE);
//                 // if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE))
//                 // {
//                 //     BattleState = "Menu";
//                 //     AttackType = "None";
//                 // }
//             }
//         }
//         else if (enemy.hp <= 0)
//         {
//             player1.points += 1;
//             hp = 100;
//             Raylib.ClearBackground(Color.BLACK);
//             AttackType = "None";
//             player1.GameState = "Labyrinth";
//             BattleState = "Menu";
//         }
//         else if (player.hp <= 0)
//         {
//             player.hp = 100;
//             enemy.hp = 100;
//             player1.GameState = "Loser";
//             BattleState = "Menu";
//         }
//     if (GameState == "Loser")
//     {
//         Raylib.ClearBackground(Color.BLACK);
//         Raylib.DrawText(("Man you suck ass at this, try again, or don't,"), 50, 220, 25, Color.RED);
//         Raylib.DrawText(($"I couldn't care less tbh. GG's."), 50, 248, 25, Color.RED);
//         Raylib.DrawText(($"I'll deduct a point for the lackluster performance."), 50, 276, 25, Color.RED);
//         Raylib.DrawText(($"[Space] to respawn."), 50, 304, 25, Color.RED);

//         if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE))
//         {
//             GameState = "Labyrinth";
//             player1.points -= 1;
//             playerRect.x = 400;
//             playerRect.y = -300;
//         }
//     }
//     if (player1.GameState == "Victory")
//     {
//         Raylib.ClearBackground(Color.BLACK);
//         Raylib.DrawText(("+10 Points!"), 50, 170, 25, Color.WHITE);
//         Raylib.DrawText(("Congratulations, you have showed at least some competence!"), 50, 220, 25, Color.RED);
//         Raylib.DrawText(($"You gained a total of {player1.points} points!"), 50, 248, 25, Color.RED);
//         Raylib.DrawText(($"Uhh, anyway, you'll have to leave, you kinda stink."), 50, 276, 25, Color.RED);
//         Raylib.DrawText(($"[Esc] to exit."), 50, 304, 25, Color.RED);
//     }
// }
}