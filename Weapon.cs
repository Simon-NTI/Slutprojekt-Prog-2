/// <summary>
/// The weapon type increases damage and attackspeed
/// </summary>
public class Weapon : Item
{
    public Weapon(int rarity) : base(rarity)
    {
        Damage = rarity * 2;
        AttackSpeed = rarity * 20;
    }
}