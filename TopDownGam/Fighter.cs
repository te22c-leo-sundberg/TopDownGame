using Raylib_cs;
public class Fighter
{
    Random generator = new Random();
    public int Accuracy;
    public int hp = 100;
    public int LightAtkDmg = 20;
    public int HeavyAtkDmg = 35;
    public int EnemyAtkDmg;
    public int MinEnemyDmg = 5;
    public int MaxEnemyDmg = 15;
    bool InitialEncounter = true;
    public string BattleState = "Menu";
    public string AttackType = "";
    public void LightAttack(Fighter target) //deduct damage from targets hp
    {
        target.hp -= LightAtkDmg;
    }
    public void HeavyAttack(Fighter target) //deduct damage from targets hp
    {
        target.hp -= HeavyAtkDmg;
    }
    public void EnemyAttack(Fighter target) //deduct damage from targets hp
    {
        EnemyAtkDmg = generator.Next(MinEnemyDmg , MaxEnemyDmg);
        target.hp -= EnemyAtkDmg;
    }
public void BattleMode(Fighter Player, Fighter Enemy, Player Player1)
{
    if (Player1.GameState == "Battle")
    {
        var random = new Random(); 
        Raylib.ClearBackground(Color.BLACK);

        if (Player.hp > 0 && Enemy.hp > 0)//only runs the fight code if both of the peoples health is above 0
        {
            Raylib.DrawText(($"Your health:{Player.hp}"), 50, 760, 25, Color.RED);
            Raylib.DrawText(($"Foe health:{Enemy.hp}"), 50, 800, 25, Color.RED);//displays health
            if (BattleState == "Menu")
            {
                if (InitialEncounter == true)//checks if its your first encounter, and if it is, does special dialogue. Otherwise, asks what you want to do.
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

                if (Raylib.IsKeyPressed(KeyboardKey.KEY_C))//checks for key input depending on what attack you chose and makes initial encounter false.
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
            if (BattleState == "Carve")//rolls for accuracy between 1 and 10, and if lookies is true, adds the lookie accuracy value onto your accuracy number
            {
                // makes attack type either Carve or Puncture, after, then rolls accuracy to see if you hit the attack, then Miss or Hit too to provide correct dialogue, then makes battlestate enemy attack
                Player1.Accuracy = generator.Next(1, 10);
                if (Player1.Lookies == true) {Player1.Accuracy += Player1.LookiesAccuracy;}
                if (Player1.Accuracy >= 2)
                {
                    Player.LightAttack(Enemy);
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
                Player1.Accuracy = generator.Next(1, 10);
                if (Player1.Lookies == true) {Player1.Accuracy += Player1.LookiesAccuracy;}
                if (Player1.Accuracy >= 4)
                {
                    Player.HeavyAttack(Enemy); //Player has targetted the enemy with this attack.
                    AttackType = "PunctureHit";
                    BattleState = "EnemyAttack";
                }
                else
                {
                    AttackType = "PunctureMiss";
                    BattleState = "EnemyAttack";
                    Player.hp -= 5; //slight punishment if you mess up the risky attack cause im an asshole and i hate fun
                }
            }
            if (BattleState == "EnemyAttack")
            {
                Enemy.EnemyAttack(Player); //enemy targets player with this one
                BattleState = "WaitMode";
            }
            if (BattleState == "WaitMode") //goes into wait mode to not make menu text show with your dialogue after attacking cause it'd look weird
            {
            if (AttackType == "PunctureMiss")//based on if your attack hit or not and the type of attack, draws some text, then waits for space to be pressed before proceeding further
            //this is not drawn with when the player attacks as that has to be 1 frame, because otherwise, every frame player or enemy will deal damage.
            {
                Raylib.ClearBackground(Color.BLACK);
                Raylib.DrawText(($"You thrust your sword but lack confidence, and drop it"), 50, 100, 25, Color.WHITE);
                Raylib.DrawText(($"on your toe, dealing 5 damage to yourself."), 50, 140, 25, Color.WHITE);
                Raylib.DrawText(($"The foe charged recklessly into you with a headbutt,"), 50, 180, 25, Color.WHITE);
                Raylib.DrawText(($"dealing {Enemy.EnemyAtkDmg} damage to you."), 50, 220, 25, Color.WHITE);
                Raylib.DrawText(("Press [Space] to proceed."), 50, 300, 25, Color.WHITE);
            }
            else if (AttackType == "PunctureHit")
            {
                Raylib.ClearBackground(Color.BLACK);
                Raylib.DrawText(($"You thrust your sword into the foe with confidence,"), 50, 100, 25, Color.WHITE);
                Raylib.DrawText(($"hitting the foe in the heart, dealing {Player.HeavyAtkDmg} damage."), 50, 140, 25, Color.WHITE);
                Raylib.DrawText(($"The foe charged recklessly into you with a headbutt,"), 50, 180, 25, Color.WHITE);
                Raylib.DrawText(($"dealing {Enemy.EnemyAtkDmg} damage to you."), 50, 220, 25, Color.WHITE);
                Raylib.DrawText(("Press [Space] to proceed."), 50, 300, 25, Color.WHITE);

            }
            else if (AttackType == "CarveMiss")
            {
                Raylib.ClearBackground(Color.BLACK);
                Raylib.DrawText(($"You swing your sword, but your confidence"), 50, 100, 25, Color.WHITE);
                Raylib.DrawText(($"was lacking and you missed."), 50, 140, 25, Color.WHITE);
                Raylib.DrawText(($"The foe charged recklessly into you with a headbutt,"), 50, 180, 25, Color.WHITE);
                Raylib.DrawText(($"dealing {Enemy.EnemyAtkDmg} damage to you."), 50, 220, 25, Color.WHITE);
                Raylib.DrawText(("Press [Space] to proceed."), 50, 300, 25, Color.WHITE);

            }
            else if (AttackType == "CarveHit")
            {
                Raylib.ClearBackground(Color.BLACK);
                Raylib.DrawText(($"You swing your sword confidently, dealing {Player.LightAtkDmg} damage."), 50, 100, 25, Color.WHITE);
                Raylib.DrawText(($"The foe charged recklessly into you with a headbutt,"), 50, 140, 25, Color.WHITE);
                Raylib.DrawText(($"dealing {Enemy.EnemyAtkDmg} damage to you."), 50, 180, 25, Color.WHITE);
                Raylib.DrawText(("Press [Space] to proceed."), 50, 260, 25, Color.WHITE);

            }
                if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE))
                {
                    BattleState = "Menu";
                    Raylib.ClearBackground(Color.BLACK);
                }
            }
            
        }
        else if (Enemy.hp <= 0) //if enemy health drops to 0 you win
        {
            Player1.GameState = "FightWon";
            Player.hp = 100;
            Enemy.hp = 100;
            AttackType = "None";
            BattleState = "Menu";
            InitialEncounter = true;
            //enables initial encounter again and reverts hps to 100 again for next battle while also resetting the attackstate and battlestate
        }
        else if (Player.hp <= 0)//if you drop to 0 you lose
        {
            Player1.GameState = "FightLost";
            Player.hp = 100;
            Enemy.hp = 100;
            AttackType = "None";
            BattleState = "Menu";
            InitialEncounter = true;
            //enables initial encounter again and reverts hps to 100 again for next battle while also resetting the attackstate and battlestate
        }
}
}
}