public class Fighter
{
    public int hp = 100;
    public int LightAtkDmg = 15;
    public int HeavyAtkDmg = 25;
    public void LightAttack(Fighter target)
    {
        target.hp -= LightAtkDmg;
    }
    public void HeavyAttack(Fighter target)
    {
        target.hp -=HeavyAtkDmg;
    }
}