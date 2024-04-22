using System.Numerics;
using System.Reflection.Metadata;
using Raylib_cs;
using Slutprojekt;

sealed class Inventory
{
    private (Weapon weapon, Necklace necklace, Armor armor) equippedItems;
    private bool isHoldingItem = false;
    private Item heldItem;


    private (int x, int y) INVENTORY_OFFSET = (Program.SCREEN_SIZE.x - 800, 200);
    private const int NEW_ROW_THRESHOLD = (int)Math.Floor((Program.SCREEN_SIZE.x - INVENTORY_OFFSET.x - 100) / (float)ITEM_SIZE);
    private const int ITEM_SIZE = 100;

    List<Item> items = new List<Item>();

    public void DrawInformation()
    {
        DrawEquippedItems();
        DrawInventory();
        CheckGrab();
    }

    private void CheckGrab()
    {
        if(Raylib.IsMouseButtonPressed(MouseButton.Left))
        {
            Vector2 mousePosition = Raylib.GetMousePosition();
            for (int i = 0; i < items.Count; i++)
            {
                Raylib.CheckCollisionPointRec
            }
        }
    }

    private void WhileGrabbing()
    {
        if(isHoldingItem)
        {

        }
    }

    public void GiveInitialItems()
    {
        Random generator = new Random();
        for (int i = 0; i < 20; i++)
        {
            switch(generator.Next(3))
            {
                case 0:
                    items.Add(new Armor());
                    break;

                case 1:
                    items.Add(new Necklace());
                    break;

                case 2:
                    items.Add(new Weapon("Jeff", 2, 0.5f));
                    break;
            }
        }
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
            //TODO draw an image for each item in items
            Raylib.DrawRectangle(
                INVENTORY_OFFSET.x + ITEM_SIZE * i - ITEM_SIZE * NEW_ROW_THRESHOLD * (int)Math.Floor(i * (1f / NEW_ROW_THRESHOLD)),
                INVENTORY_OFFSET.y + ITEM_SIZE * (int)Math.Floor(i * (1f / NEW_ROW_THRESHOLD)),
                ITEM_SIZE, 
                ITEM_SIZE,
                Color.Beige
            );

            Utils.DrawCenteredText(
                i.ToString(),
                Program.DEFAULT_FONT_SIZE / 2 + INVENTORY_OFFSET.x + ITEM_SIZE * i - ITEM_SIZE * NEW_ROW_THRESHOLD * (int)Math.Floor(i * (1f / newRowThreshold)),
                Program.DEFAULT_FONT_SIZE / 2 + INVENTORY_OFFSET.y + ITEM_SIZE * (int)Math.Floor(i * (1f / NEW_ROW_THRESHOLD)),
                Program.DEFAULT_FONT_SIZE,
                Color.White
            );
        }
    }
}