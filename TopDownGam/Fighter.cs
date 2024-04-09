using Raylib_cs;
public class Fighter
{
    Random generator = new Random();
    public int accuracy;
    public int hp = 100;
    public int LightAtkDmg = 15;
    public int HeavyAtkDmg = 35;
    public int EnemyAtkDmg = 10;
    bool InitialEncounter = true;
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
public void BattleMode(Fighter player, Fighter enemy, Player player1)
{
        var random = new Random();
        Raylib.ClearBackground(Color.BLACK);

        if (player.hp > 0 && enemy.hp > 0)
        {
            Raylib.DrawText(($"Your health:{player.hp}"), 50, 760, 25, Color.RED);
            Raylib.DrawText(($"Foe health:{enemy.hp}"), 50, 800, 25, Color.RED);
            if (BattleState == "Menu")
            {
                if (InitialEncounter == true)
                {
                    Raylib.DrawText(("A life-threatening foe has picked a fight with you"), 50, 100, 25, Color.RED);
                    Raylib.DrawText(("What is your decison?"), 50, 140, 25, Color.RED);
                    Raylib.DrawText(("[C]arve or [P]uncture"), 50, 180, 25, Color.RED);
                }
                else if (InitialEncounter == false)
                {
                    Raylib.DrawText(("What will you do next?"), 50, 100, 25, Color.RED);
                    Raylib.DrawText(("[C]arve or [P]uncture"), 50, 140, 25, Color.RED);
                }

                if (Raylib.IsKeyPressed(KeyboardKey.KEY_C))
                {
                    BattleState = "Carve";
                    InitialEncounter = false;
                }
                else if (Raylib.IsKeyPressed(KeyboardKey.KEY_P))
                {
                    BattleState = "Puncture";
                    InitialEncounter = false;
                }

            }
            if (BattleState == "Carve")
            {
                player1.accuracy = generator.Next(1, 10);
                if (player1.Lookies == true)
                {
                    player1.accuracy += player1.lookiesAccuracy;
                }
                if (player1.accuracy >= 2)
                {
                    player.LightAttack(enemy);
                    AttackType = "CarveHit";
                    BattleState = "EnemyAttack";
                }
                else
                {
                    AttackType = "CarveMiss";
                    BattleState = "EnemyAttack";
                }
            }
            else if (BattleState == "Puncture")
            {
                player1.accuracy = generator.Next(1, 10);
                if (player1.Lookies == true)
                {
                    player1.accuracy += player1.lookiesAccuracy;
                }
                if (player1.accuracy >= 4)
                {
                    player.HeavyAttack(enemy);
                    AttackType = "PunctureHit";
                    BattleState = "EnemyAttack";
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
                enemy.EnemyAttack(player);
                BattleState = "WaitMode";
            }
            if (BattleState == "WaitMode")
            {
            if (AttackType == "PunctureMiss")
            {
                Raylib.ClearBackground(Color.BLACK);
                Raylib.DrawText(($"You thrust your sword but lack confidence, and drop it"), 50, 100, 25, Color.WHITE);
                Raylib.DrawText(($"on your toe, dealing 5 damage to yourself."), 50, 140, 25, Color.WHITE);
                Raylib.DrawText(($"The foe charged recklessly into you with a headbutt,"), 50, 180, 25, Color.WHITE);
                Raylib.DrawText(($"dealing {enemy.EnemyAtkDmg} damage to you."), 50, 220, 25, Color.WHITE);
                Raylib.DrawText(("Press [Space] to proceed."), 50, 300, 25, Color.WHITE);
            }
            else if (AttackType == "PunctureHit")
            {
                Raylib.ClearBackground(Color.BLACK);
                Raylib.DrawText(($"You thrust your sword into the foe with confidence,"), 50, 100, 25, Color.WHITE);
                Raylib.DrawText(($"hitting the foe in the heart, dealing {player.HeavyAtkDmg} damage."), 50, 140, 25, Color.WHITE);
                Raylib.DrawText(($"The foe charged recklessly into you with a headbutt,"), 50, 180, 25, Color.WHITE);
                Raylib.DrawText(($"dealing {enemy.EnemyAtkDmg} damage to you."), 50, 220, 25, Color.WHITE);
                Raylib.DrawText(("Press [Space] to proceed."), 50, 300, 25, Color.WHITE);

            }
            else if (AttackType == "CarveMiss")
            {
                Raylib.ClearBackground(Color.BLACK);
                Raylib.DrawText(($"You swing your sword, but your confidence"), 50, 100, 25, Color.WHITE);
                Raylib.DrawText(($"was lacking and you missed."), 50, 140, 25, Color.WHITE);
                Raylib.DrawText(($"The foe charged recklessly into you with a headbutt,"), 50, 180, 25, Color.WHITE);
                Raylib.DrawText(($"dealing {enemy.EnemyAtkDmg} damage to you."), 50, 220, 25, Color.WHITE);
                Raylib.DrawText(("Press [Space] to proceed."), 50, 300, 25, Color.WHITE);

            }
            else if (AttackType == "CarveHit")
            {
                Raylib.ClearBackground(Color.BLACK);
                Raylib.DrawText(($"You swing your sword confidently, dealing {player.LightAtkDmg} damage."), 50, 100, 25, Color.WHITE);
                Raylib.DrawText(($"The foe charged recklessly into you with a headbutt,"), 50, 140, 25, Color.WHITE);
                Raylib.DrawText(($"dealing {enemy.EnemyAtkDmg} damage to you."), 50, 180, 25, Color.WHITE);
                Raylib.DrawText(("Press [Space] to proceed."), 50, 260, 25, Color.WHITE);

            }
                if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE))
                {
                    BattleState = "Menu";
                    Raylib.ClearBackground(Color.BLACK);
                }
            }
            
        }
        else if (enemy.hp <= 0)
        {
            player1.GameState = "FightWon";
            player.hp = 100;
            enemy.hp = 100;
            AttackType = "None";
            BattleState = "Menu";
            InitialEncounter = true;
        }
        else if (player.hp <= 0)
        {
            player1.GameState = "FightLost";
            player.hp = 100;
            enemy.hp = 100;
            AttackType = "None";
            BattleState = "Menu";
            InitialEncounter = true;
        }
}
}