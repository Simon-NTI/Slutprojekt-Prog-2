class Enemy
{
    LootTable lootTable;
    int health, damage;
    float attackSpeed, attackCooldown; // attackSpeed is given in attacks per second

    public Enemy(int health, int damage, float attackSpeed, float attackCooldown)
    {
        lootTable = new LootTable();
        this.health = health;
        this.damage = damage;
        this.attackSpeed = attackSpeed;
        this.attackCooldown = attackCooldown;
    }

    public void PerformActions()
    {

    }
}