/// <summary>
/// The necklace type increases damage and health
/// </summary>

public class Necklace : Item
{
    public Necklace(int rarity) : base(rarity)
    {
        Damage = rarity * 2;
        Health = rarity * 10;
    }
}