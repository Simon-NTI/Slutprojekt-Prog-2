/// <summary>
/// Armor is an item type which increases health and defense
/// </summary>

class Armor : Item
{
    public Armor(int rarity) : base(rarity)
    {
        Health = rarity * 10;
        Defense = rarity;
    }
}