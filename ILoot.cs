public interface ILoot
{
    //TODO shift the loot table based on enemy level OR reroll with the original table
    public static Item? GenerateLoot(int enemyLevel)
    {
        int[] weightedLootTable = new int[Enum.GetNames(typeof(Item.Rarities)).Length];
        
        for (int i = 0; i < weightedLootTable.Length; i++)
        {
            weightedLootTable[i] = (int)Math.Pow(3, i);
        }

        Random generator = new();
        double rolledValue = generator.NextDouble();
        for (int i = 0; i < weightedLootTable.Length; i++)
        {
            Console.WriteLine("Compare: " + (float)weightedLootTable[i] / weightedLootTable.Sum());
            
            if(rolledValue <= (float)weightedLootTable[i] / weightedLootTable.Sum())
            {
                Console.WriteLine("Hit Value: " + (float)weightedLootTable[i] / weightedLootTable.Sum());
                return generator.Next(3) switch
                {
                    0 => new Necklace(weightedLootTable.Length - i - 1),
                    1 => new Weapon(weightedLootTable.Length - i - 1),
                    2 => new Armor(weightedLootTable.Length - i - 1),
                    _ => null
                };
            }
        }

        return generator.Next(3) switch
        {
            0 => new Necklace(0),
            1 => new Weapon(0),
            2 => new Armor(0),
            _ => null
        };
    }
}