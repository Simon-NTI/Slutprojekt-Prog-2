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
        Inventory = new(this);
    }

    public override void OnDeath(Character opposingCharacter)
    {

    }

    /// <summary>
    /// Update player stats
    /// </summary>
    public void UpdateStats(int[] stats)
    {
        damage = stats[0] + 4;
        maxHealth = stats[1] + 20;
        attackSpeed = 1 - (stats[2] / 60);
        defense = stats[3];
    }
}