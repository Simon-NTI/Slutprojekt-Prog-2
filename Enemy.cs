class Enemy : Character
{
    readonly Item? itemReward;

    public Enemy(int enemyLevel)
    {
        RunOnConstruction();
        itemReward = ILoot.GenerateLoot(enemyLevel);
        maxHealth = 10 + (enemyLevel - 1) * 10;
        health = maxHealth;
        damage = 2 + (enemyLevel - 1) * 3;
        defense = (enemyLevel - 5) * 2;
        if (defense < 0) defense = 0;

        attackSpeed = 3f - (float)(Math.Floor((enemyLevel - 1) * 0.2d) * 0.1d);
        if (attackSpeed < 0) attackSpeed = 0.1f;
    }
    public override void OnDeath(Character opposingCharacter)
    {
        try
        {
            Player player = (Player)opposingCharacter;

            if(itemReward is not null)
            {
                player.Inventory.AddItem(itemReward);
            }
        }
        catch(Exception e)
        {
            Console.WriteLine("Unable to cast opposing character\n" + e.Message);
        }
    }
}