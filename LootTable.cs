public class  LootTable
{
    Item[] lootTable;
    float[] lootTableOdds;  // The odds in the table are expressed as a fraction 

    public LootTable(int enemyLevel)
    {
        (Item[], float[]) items = GenerateLootTable(enemyLevel);
        lootTable = items.Item1;
        lootTableOdds = items.Item2;
    }

    // The sum of all items in the loot table must equal 100
    private void FormatLootTable()
    {
        for (var i = 0; i < lootTableOdds.Length; i++)
        {
            lootTableOdds[i] /= 100;
        }
    }

    private (Item[], float[]) GenerateLootTable(int enemyLevel)
    {
        var items = (new Item[1], new float[1]);
        return items;
    }
}