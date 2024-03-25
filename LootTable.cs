public class  LootTable
{
    Item[] lootTable;
    float[] lootTableOdds;  // The odds in the table are expressed in %    

    public LootTable(float[] lootTableOdds)
    {
        this.lootTable = GenerateLootTable();
        this.lootTableOdds = lootTableOdds;
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