using System.Reflection.Metadata;
using Raylib_cs;
using Slutprojekt;

class Inventory
{
    Weapon equippedWeapon;
    Necklace equippedNecklace;

    (int x, int y) INVENTORY_OFFSET = (Program.SCREEN_SIZE.x - 800, 200);

    List<Item> items = new List<Item>();

    public void DrawInformation()
    {
        DrawEquippedItems();
        DrawInventory();
    }

    private void DrawEquippedItems()
    {

    }

    private void DrawInventory()
    {
        Raylib.DrawRectangle(
            INVENTORY_OFFSET.x, 
            INVENTORY_OFFSET.y, 
            Program.SCREEN_SIZE.x - INVENTORY_OFFSET.x - 100, 
            Program.SCREEN_SIZE.y - 300, 
            Color.DarkBlue
            );

        for (int i = items.Count - 1; i >= 0 ; i--)
        {
            int newRowThreshold = (int)Math.Floor((Program.SCREEN_SIZE.x - INVENTORY_OFFSET.x - 100) / 50f);
            //TODO draw an image for each item in items
            //Raylib.DrawRectangle(INVENTORY_OFFSET.x + 50 * i - 50 * newRowThreshold * (int)Math.Floor(i * (1f / newRowThreshold)), Color.Beige);

        }
    }
}