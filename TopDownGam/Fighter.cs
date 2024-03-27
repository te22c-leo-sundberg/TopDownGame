public class Fighter
{
    public int hp = 100;
    public int LightAtkDmg = 15;
    public int HeavyAtkDmg = 25;
    public int EnemyAtkDmg = 10;
    public string BattleState = "Menu";
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

    public void BattleState();

}