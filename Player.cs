
class Player : Character
{
    public Inventory inventory { get; }
    public Player(int maxHealth, int damage, int defense, float attackSpeed)
    {
        RunOnConstruction();
        this.maxHealth = maxHealth;
        health = maxHealth;
        this.damage = damage;
        this.defense = defense;
        this.attackSpeed = attackSpeed;
        attackCooldown = 0;
        inventory = new();
    }

    public override void OnDeath(Character opposingCharacter)
    {
        Console.WriteLine("Player is die :(");
    }
}