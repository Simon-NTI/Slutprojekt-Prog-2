class Armor : Item
{
    public Armor(int rarity) : base(rarity)
    {
        Health = rarity * 10;
        Defense = rarity;
    }
}