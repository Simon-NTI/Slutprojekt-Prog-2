using System.Numerics;
using System.Reflection.Metadata;
using Raylib_cs;
using Slutprojekt;

class Inventory
{
    class INVENTORY_OFFSET
    {
        public const int X = 800;
        public const int Y = 800;
    }

    private (Item? item, (int x, int y) position) heldItem;
    private static readonly Rectangle[] ITEM_POSITIONS = CalculateItemPositions();
    private (Weapon weapon, Necklace necklace, Armor armor) equippedItems;
    private const int ITEM_SIZE = 100;
    private static readonly int NEW_ROW_THRESHOLD = (int)Math.Floor((Constants.SCREEN_SIZE.X - INVENTORY_OFFSET.X - 100) / (float)ITEM_SIZE);
    List<Item> items = new List<Item>();

    public void DrawInformation()
    {
        DrawEquippedItems();
        DrawInventory();
        CheckGrab();
    }

    private static Rectangle[] CalculateItemPositions()
    {
        Rectangle[] itemRectangles = new Rectangle[NEW_ROW_THRESHOLD * NEW_ROW_THRESHOLD];
        for (int i = 0; i < itemRectangles.Length; i++)
        {
            itemRectangles[i] = new(
                INVENTORY_OFFSET.X + ITEM_SIZE * i - ITEM_SIZE * NEW_ROW_THRESHOLD * (int)Math.Floor(i * (1f / NEW_ROW_THRESHOLD)),
                INVENTORY_OFFSET.Y + ITEM_SIZE * (int)Math.Floor(i * (1f / NEW_ROW_THRESHOLD)),
                ITEM_SIZE,
                ITEM_SIZE
            );
        }
        return itemRectangles;
    }

    private Item? CheckGrab()
    {
        if(Raylib.IsMouseButtonPressed(MouseButton.Left))
        {
            Vector2 mousePosition = Raylib.GetMousePosition();
            for (int i = 0; i < items.Count; i++)
            {
                if(Raylib.CheckCollisionPointRec(mousePosition, ITEM_POSITIONS[i]))
                {
                    return items[i];
                }
            }
        }
        return null;
    }

    private void WhileGrabbing()
    {
        if(heldItem.item != null && Raylib.IsMouseButtonDown(MouseButton.Left))
        {
            heldItem.position = ((int)Raylib.GetMousePosition().X, (int)Raylib.GetMousePosition().Y);
        }
        else
        {
            return;
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
            INVENTORY_OFFSET.X, 
            INVENTORY_OFFSET.Y, 
            Constants.SCREEN_SIZE.X - INVENTORY_OFFSET.X - 100, 
            Constants.SCREEN_SIZE.Y - 300, 
            Color.DarkBlue
        );

        for (int i = items.Count - 1; i >= 0 ; i--)
        {
            //TODO draw an image for each item in items
            Raylib.DrawRectangleRec(
                ITEM_POSITIONS[i],
                Color.Beige
            );

            Utils.DrawCenteredText(
                i.ToString(),
                Constants.DEFAULT_FONT_SIZE / 2 + (int)ITEM_POSITIONS[i].X,
                Constants.DEFAULT_FONT_SIZE / 2 + (int)ITEM_POSITIONS[i].Y,
                Constants.DEFAULT_FONT_SIZE,
                Color.White
            );
        }
    }
}