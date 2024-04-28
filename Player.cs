
class Player : Character
{
    public Inventory Inventory { get; }
    public Player(int maxHealth, int damage, int defense, float attackSpeed)
    {
        RunOnConstruction();
        this.maxHealth = maxHealth;
        health = maxHealth;
        this.damage = damage;
        this.defense = defense;
        this.attackSpeed = attackSpeed;
        attackCooldown = 0;
        Inventory = new();
    }

    public override void OnDeath(Character opposingCharacter)
    {
        Console.WriteLine("Player is die :(");
    }
}