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

        Random generator = new Random();
        double rolledValue = generator.NextDouble();
        Console.WriteLine("Rolled Value: " + rolledValue);
        for (int i = 0; i < weightedLootTable.Length; i++)
        {
            Console.WriteLine("Compare: " + (float)weightedLootTable[i] / weightedLootTable.Sum());
            if(rolledValue <= (float)weightedLootTable[i] / weightedLootTable.Sum())
            {
                Console.WriteLine("Hit Value: " + (float)weightedLootTable[i] / weightedLootTable.Sum());
                switch(generator.Next(3))
                {
                    case 0:
                        return new Necklace(weightedLootTable.Length - i - 1);
                    
                    case 1:
                        return new Weapon(weightedLootTable.Length - i - 1);

                    case 2:
                        return new Armor(weightedLootTable.Length - i - 1);
                }
            }
        }

        return generator.Next(2) switch
        {
            0 => new Necklace(0),
            1 => new Weapon(0),
            2 => new Armor(0),
            _ => null,
        };
    }
}