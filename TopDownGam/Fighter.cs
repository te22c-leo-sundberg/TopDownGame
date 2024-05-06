using Raylib_cs;
public class Fighter
{
    Random generator = new Random();
    public int accuracy;
    public int hp = 100;
    public int lightAtkDmg = 20;
    public int heavyAtkDmg = 35;
    public int enemyAtkDmg;
    public int minEnemyDmg = 5;
    public int maxEnemyDmg = 15;
    bool initialEncounter = true;
    public string battleState = "Menu";
    public string attackType = "";
    public void LightAttack(Fighter target) //deduct damage from targets hp
    {
        target.hp -= lightAtkDmg;
    }
    public void HeavyAttack(Fighter target) //deduct damage from targets hp
    {
        target.hp -= heavyAtkDmg;
    }
    public void EnemyAttack(Fighter target) //deduct damage from targets hp
    {
        enemyAtkDmg = generator.Next(minEnemyDmg , maxEnemyDmg);
        target.hp -= enemyAtkDmg;
    }
public void BattleMode(Fighter player, Fighter enemy, Player player1)
{
    if (player1.gameState == "Battle")
    {
        var random = new Random(); 
        Raylib.ClearBackground(Color.BLACK);

        if (player.hp > 0 && enemy.hp > 0)//only runs the fight code if both of the peoples health is above 0
        {
            Raylib.DrawText(($"Your health:{player.hp}"), 50, 760, 25, Color.RED);
            Raylib.DrawText(($"Foe health:{enemy.hp}"), 50, 800, 25, Color.RED);//displays health
            if (battleState == "Menu")
            {
                if (initialEncounter == true)//checks if its your first encounter, and if it is, does special dialogue. Otherwise, asks what you want to do.
                {
                    Raylib.DrawText(("A life-threatening foe has picked a fight with you"), 50, 100, 25, Color.RED);
                    Raylib.DrawText(("What is your decison?"), 50, 140, 25, Color.RED);
                    Raylib.DrawText(("[C]arve or [P]uncture"), 50, 180, 25, Color.RED);
                }
                else if (initialEncounter == false)
                {
                    Raylib.DrawText(("What will you do next?"), 50, 100, 25, Color.RED);
                    Raylib.DrawText(("[C]arve or [P]uncture"), 50, 140, 25, Color.RED);
                }

                if (Raylib.IsKeyPressed(KeyboardKey.KEY_C))//checks for key input depending on what attack you chose and makes initial encounter false.
                {
                    battleState = "Carve";
                    initialEncounter = false;
                }
                else if (Raylib.IsKeyPressed(KeyboardKey.KEY_P))
                {
                    battleState = "Puncture";
                    initialEncounter = false;
                }

            }
            if (battleState == "Carve")//rolls for accuracy between 1 and 10, and if lookies is true, adds the lookie accuracy value onto your accuracy number
            {
                // makes attack type either Carve or Puncture, after, then rolls accuracy to see if you hit the attack, then Miss or Hit too to provide correct dialogue, then makes battlestate enemy attack
                player1.accuracy = generator.Next(1, 10);
                if (player1.lookies == true) {player1.accuracy += player1.lookiesAccuracy;}
                if (player1.accuracy >= 2)
                {
                    player.LightAttack(enemy);
                    attackType = "CarveHit";
                    battleState = "EnemyAttack";
                }
                else
                {
                    attackType = "CarveMiss";
                    battleState = "EnemyAttack";
                }
            }
            else if (battleState == "Puncture")
            {
                player1.accuracy = generator.Next(1, 10);
                if (player1.lookies == true) {player1.accuracy += player1.lookiesAccuracy;}
                if (player1.accuracy >= 4)
                {
                    player.HeavyAttack(enemy); //Player has targetted the enemy with this attack.
                    attackType = "PunctureHit";
                    battleState = "EnemyAttack";
                }
                else
                {
                    attackType = "PunctureMiss";
                    battleState = "EnemyAttack";
                    player.hp -= 5; //slight punishment if you mess up the risky attack cause im an asshole and i hate fun
                }
            }
            if (battleState == "EnemyAttack")
            {
                enemy.EnemyAttack(player); //enemy targets player with this one
                battleState = "WaitMode";
            }
            if (battleState == "WaitMode") //goes into wait mode to not make menu text show with your dialogue after attacking cause it'd look weird
            {
            if (attackType == "PunctureMiss")//based on if your attack hit or not and the type of attack, draws some text, then waits for space to be pressed before proceeding further
            //this is not drawn with when the player attacks as that has to be 1 frame, because otherwise, every frame player or enemy will deal damage.
            {
                Raylib.ClearBackground(Color.BLACK);
                Raylib.DrawText(($"You thrust your sword but lack confidence, and drop it"), 50, 100, 25, Color.WHITE);
                Raylib.DrawText(($"on your toe, dealing 5 damage to yourself."), 50, 140, 25, Color.WHITE);
                Raylib.DrawText(($"The foe charged recklessly into you with a headbutt,"), 50, 180, 25, Color.WHITE);
                Raylib.DrawText(($"dealing {enemy.enemyAtkDmg} damage to you."), 50, 220, 25, Color.WHITE);
                Raylib.DrawText(("Press [Space] to proceed."), 50, 300, 25, Color.WHITE);
            }
            else if (attackType == "PunctureHit")
            {
                Raylib.ClearBackground(Color.BLACK);
                Raylib.DrawText(($"You thrust your sword into the foe with confidence,"), 50, 100, 25, Color.WHITE);
                Raylib.DrawText(($"hitting the foe in the heart, dealing {player.heavyAtkDmg} damage."), 50, 140, 25, Color.WHITE);
                Raylib.DrawText(($"The foe charged recklessly into you with a headbutt,"), 50, 180, 25, Color.WHITE);
                Raylib.DrawText(($"dealing {enemy.enemyAtkDmg} damage to you."), 50, 220, 25, Color.WHITE);
                Raylib.DrawText(("Press [Space] to proceed."), 50, 300, 25, Color.WHITE);

            }
            else if (attackType == "CarveMiss")
            {
                Raylib.ClearBackground(Color.BLACK);
                Raylib.DrawText(($"You swing your sword, but your confidence"), 50, 100, 25, Color.WHITE);
                Raylib.DrawText(($"was lacking and you missed."), 50, 140, 25, Color.WHITE);
                Raylib.DrawText(($"The foe charged recklessly into you with a headbutt,"), 50, 180, 25, Color.WHITE);
                Raylib.DrawText(($"dealing {enemy.enemyAtkDmg} damage to you."), 50, 220, 25, Color.WHITE);
                Raylib.DrawText(("Press [Space] to proceed."), 50, 300, 25, Color.WHITE);

            }
            else if (attackType == "CarveHit")
            {
                Raylib.ClearBackground(Color.BLACK);
                Raylib.DrawText(($"You swing your sword confidently, dealing {player.lightAtkDmg} damage."), 50, 100, 25, Color.WHITE);
                Raylib.DrawText(($"The foe charged recklessly into you with a headbutt,"), 50, 140, 25, Color.WHITE);
                Raylib.DrawText(($"dealing {enemy.enemyAtkDmg} damage to you."), 50, 180, 25, Color.WHITE);
                Raylib.DrawText(("Press [Space] to proceed."), 50, 260, 25, Color.WHITE);

            }
                if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE))
                {
                    battleState = "Menu";
                    Raylib.ClearBackground(Color.BLACK);
                }
            }
            
        }
        else if (enemy.hp <= 0) //if enemy health drops to 0 you win
        {
            player1.gameState = "FightWon";
            player.hp = 100;
            enemy.hp = 100;
            attackType = "None";
            battleState = "Menu";
            initialEncounter = true;
            //enables initial encounter again and reverts hps to 100 again for next battle while also resetting the attackstate and battlestate
        }
        else if (player.hp <= 0)//if you drop to 0 you lose
        {
            player1.gameState = "FightLost";
            player.hp = 100;
            enemy.hp = 100;
            attackType = "None";
            battleState = "Menu";
            initialEncounter = true;
            //enables initial encounter again and reverts hps to 100 again for next battle while also resetting the attackstate and battlestate
        }
}
}
}