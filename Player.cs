
class Player : Character
{
    public Player(int maxHealth, int damage, int defense, float attackSpeed) : base(maxHealth, damage, defense, attackSpeed)
    {

    }

    public override void OnDeath(Character opposingCharacter)
    {
        Console.WriteLine("Player is die :(");
    }
}