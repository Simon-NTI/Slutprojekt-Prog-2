class Enemy : Character
{
    LootTable lootTable;

    public Enemy(int health, int damage, int defense, float attackSpeed, int enemyLevel) : base(health, damage, defense, attackSpeed)
    {
        lootTable = new LootTable(enemyLevel);
    }
    public override void OnDeath(Character opposingCharacter)
    {
        Console.WriteLine("Enemy is die :)");
    }
}