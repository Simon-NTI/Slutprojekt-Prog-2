class Enemy : Character
{
    LootTable lootTable;

    public Enemy(int enemyLevel)
    {
        RunOnConstruction();
        lootTable = new LootTable(enemyLevel);
        maxHealth = 10 + (enemyLevel - 1) * 10;
        health = maxHealth;
        damage = 2 + (enemyLevel - 1) * 3;

        defense = (enemyLevel - 5) * 2;
        if (defense < 0) defense = 0;

        attackSpeed = 2f - (float)(1 / Math.Ceiling((enemyLevel - 1d) * 0.2d));
        if (attackSpeed < 0) attackSpeed = 0.1f;
    }
    public override void OnDeath(Character opposingCharacter)
    {
        Console.WriteLine("Enemy is die :)");
    }
}