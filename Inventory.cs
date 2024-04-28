using System.IO.Compression;
using System.Numerics;
using System.Reflection.Metadata;
using Raylib_cs;
using Slutprojekt;

class Inventory
{
    class INVENTORY_OFFSET
    {
        public const int X = 800;
        public const int Y = 200;
    }
    private (Item? item, (int x, int y) position, int? listIndex) heldItem;
    private static readonly int NEW_ROW_THRESHOLD = (int)Math.Floor((Constants.SCREEN_SIZE.X - INVENTORY_OFFSET.X - 100) / (float)ITEM_SIZE);
    private static readonly Rectangle[] ITEM_POSITIONS = CalculateItemPositions();

    //ItemGraphics are preloaded textures
    private static readonly Texture2D[] itemGraphics = new Texture2D[4]
    {
        Raylib.LoadTexture("Assets/Armor.png"),
        Raylib.LoadTexture("Assets/Necklace.png"),
        Raylib.LoadTexture("Assets/Weapon.png"),
        Raylib.LoadTexture("Assets/Unknown.png")
    };

    private enum ItemGraphics
    {
        Armor,
        Necklace,
        Weapon,
        Unknown
    }
    private readonly Item?[] equippedItems = new Item?[3]
    {
        null, null, null
    };
    private readonly Type[] equippedItemsOrder = new Type[3]
    {
        typeof(Necklace), typeof(Weapon), typeof(Armor)
    };
    private readonly Rectangle[] equippedItemPositions;
    //private (Weapon weapon, Necklace necklace, Armor armor) equippedItems;
    private const int ITEM_SIZE = 100;
    public readonly List<Item> items = new();
    
    /// <summary>
    /// Constructor
    /// </summary>
    public Inventory()
    {
        equippedItemPositions = CalculateEquippedItemPositions();
    }
    public void DrawInformation()
    {
        CheckGrab();
        DrawEquippedItems();
        DrawInventory();
    }

    private Rectangle[] CalculateEquippedItemPositions()
    {
        Rectangle[] itemPositions = new Rectangle[equippedItems.Length];
        for (int i = 0; i < equippedItems.Length; i++)
        {
            itemPositions[i] = new(
                INVENTORY_OFFSET.X + ITEM_SIZE * 1.5f * i,
                INVENTORY_OFFSET.Y - ITEM_SIZE * 1.5f,
                ITEM_SIZE,
                ITEM_SIZE
            );
        }
        return itemPositions;
    }
    private static Rectangle[] CalculateItemPositions()
    {
        Rectangle[] itemPositions = new Rectangle[NEW_ROW_THRESHOLD * NEW_ROW_THRESHOLD];

        for (int i = 0; i < itemPositions.Length; i++)
        {
            itemPositions[i] = new(
                INVENTORY_OFFSET.X + ITEM_SIZE * i - ITEM_SIZE * NEW_ROW_THRESHOLD * (int)Math.Floor(i * (1f / NEW_ROW_THRESHOLD)),
                INVENTORY_OFFSET.Y + ITEM_SIZE * (int)Math.Floor(i * (1f / NEW_ROW_THRESHOLD)),
                ITEM_SIZE,
                ITEM_SIZE
            );
        }
        return itemPositions;
    }

    private void CheckGrab()
    {
        //Check if the player has pressed left mouse on an item
        if(Raylib.IsMouseButtonPressed(MouseButton.Left))
        {
            Vector2 mousePosition = Raylib.GetMousePosition();
            for (int i = 0; i < items.Count; i++)
            {
                if(Raylib.CheckCollisionPointRec(mousePosition, ITEM_POSITIONS[i]))
                {
                    heldItem.item = items[i];
                    heldItem.listIndex = i;
                    heldItem.position = ((int)Raylib.GetMousePosition().X, (int)Raylib.GetMousePosition().Y);
                    return;
                }
            }
        }
        //Update the position of the players held item if they are currently holding left mouse
        else if(heldItem.item is not null && Raylib.IsMouseButtonDown(MouseButton.Left))
        {
            heldItem.position = ((int)Raylib.GetMousePosition().X, (int)Raylib.GetMousePosition().Y);
            return;
        }
        //Check if the player has released the held item,
        //then equip it if it's aligned with a tile in the equipped item grid and has a matching type
        else if(heldItem.item != null && Raylib.IsMouseButtonReleased(MouseButton.Left) && heldItem.listIndex != null)
        {
            for (int i = 0; i < equippedItemPositions.Length; i++)
            {
                if(Raylib.CheckCollisionPointRec(Raylib.GetMousePosition(), equippedItemPositions[i])
                    && equippedItemsOrder[i] == heldItem.item.GetType())
                {
                    if(equippedItems[i] is not null)
                    {
                        items.Add(equippedItems[i]);
                    }

                    equippedItems[i] = heldItem.item;
                    items.RemoveAt((int)heldItem.listIndex);
                    heldItem.item = null;
                    heldItem.listIndex = null;
                    return;
                }
            }
        }
        //The player did not perform any actions with the left mouse button
        else
        {
            heldItem.item = null;
            heldItem.listIndex = null;
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

            if(Enum.TryParse(typeof(ItemGraphics), items[i].GetType().ToString(), true, out object? result))
            {
                items[i].texture = itemGraphics[(int)result];
            }
            else
            {
                items[i].texture = itemGraphics[(int)ItemGraphics.Unknown];
            }
        }
    }

    private void DrawEquippedItems()
    {

    }

    private void DrawInventory()
    {
        //Inventory background
        Raylib.DrawRectangle(
            INVENTORY_OFFSET.X, 
            INVENTORY_OFFSET.Y, 
            Constants.SCREEN_SIZE.X - INVENTORY_OFFSET.X - 100, 
            Constants.SCREEN_SIZE.Y - 300, 
            Color.DarkBlue
        );

        //Inventory items
        for (int i = items.Count - 1; i >= 0 ; i--)
        {
            //Item background
            Raylib.DrawRectangleRec(
                ITEM_POSITIONS[i],
                Color.Beige
            );

            //Item icon
            Raylib.DrawTexture(
                items[i].texture,
                (int)ITEM_POSITIONS[i].X + 34,
                (int)ITEM_POSITIONS[i].Y + 34,
                Color.White
            );
        }

        //Equipped items
        for (int i = 0; i < equippedItemPositions.Length; i++)
        {
            Raylib.DrawRectangleRec(equippedItemPositions[i], Color.DarkPurple);

            Raylib.DrawTexture(
                equippedItems[i] is not null ? equippedItems[i].texture : itemGraphics[(int)ItemGraphics.Unknown],
                (int)equippedItemPositions[i].X + 34,
                (int)equippedItemPositions[i].Y + 34,
                Color.White
            );
        }

        //Held item
        if(heldItem.item is not null)
        {
            Raylib.DrawRectangle(
                heldItem.position.x - ITEM_SIZE / 2,
                heldItem.position.y - ITEM_SIZE / 2,
                ITEM_SIZE,
                ITEM_SIZE,
                Color.SkyBlue
            );

            Raylib.DrawTexture(
                heldItem.item.texture,
                heldItem.position.x - ITEM_SIZE / 2 + 34,
                heldItem.position.y - ITEM_SIZE / 2 + 34,
                Color.White
            );
        }
    }
}