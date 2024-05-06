/// <summary>
/// The inventory class contains logic to handle items
/// </summary>

using System.Numerics;
using Raylib_cs;

class Inventory
{
    /// <summary>
    /// Defines the specifications of the dimensions of the inventory
    /// </summary>
    class INVENTORY_SIZE
    {
        public class OFFSET
        {
            public const int X = 800;
            public const int Y = 200;
        }

        public const int WIDTH = Constants.SCREEN_SIZE.X - OFFSET.X - 50;
        public const int HEIGHT = Constants.SCREEN_SIZE.Y - OFFSET.Y - 200;
    }

    private readonly Player parentPlayer; // The player who owns this inventory
    private (Item? item, (int x, int y) position, int? listIndex) heldItem;
    private static readonly int NEW_ROW_THRESHOLD = (int)Math.Floor(INVENTORY_SIZE.WIDTH / (float)ITEM_SIZE);
    private static readonly int MAX_ITEM_COUNT = NEW_ROW_THRESHOLD * NEW_ROW_THRESHOLD;
    private static readonly Rectangle[] INVENTORY_ITEM_TILES = CalculateItemTiles();
    private static readonly Texture2D deleteItemTexture = Raylib.LoadTexture("Assets/Delete.png");
    private static readonly int textureOffset = (ITEM_SIZE - deleteItemTexture.Height) / 2;
    private readonly Item[] equippedItems = new Item[3];
    private readonly Type[] equippedItemsOrder = new Type[3]
    {
        typeof(Necklace), typeof(Weapon), typeof(Armor)
    };
    private readonly Rectangle[] equippedItemTiles;
    private const int ITEM_SIZE = 100;
    public readonly List<Item> items = new();
    private readonly Rectangle deleteItemTile;

    /// <summary>
    /// Constructor
    /// </summary>
    public Inventory(Player parentPlayer)
    {
        equippedItemTiles = CalculateEquippedItemTiles();
        this.parentPlayer = parentPlayer;

        deleteItemTile = new(
            equippedItemTiles[^1].X + ITEM_SIZE * 1.5f,
            equippedItemTiles[0].Y,
            ITEM_SIZE,
            ITEM_SIZE
        );

        GiveInitialItems();
    }
    public void DrawInformation()
    {
        CheckGrab();
        DrawInventory();
    }

    /// <summary>
    /// Calculate where each equipped item is positioned
    /// </summary>
    private Rectangle[] CalculateEquippedItemTiles()
    {
        Rectangle[] itemPositions = new Rectangle[equippedItems.Length];
        for (int i = 0; i < equippedItems.Length; i++)
        {
            itemPositions[i] = new(
                INVENTORY_SIZE.OFFSET.X + ITEM_SIZE * 1.5f * i,
                INVENTORY_SIZE.OFFSET.Y - ITEM_SIZE * 1.5f,
                ITEM_SIZE,
                ITEM_SIZE
            );
        }
        return itemPositions;
    }

    /// <summary>
    /// Calculate where each inventory item is displayed based on inventory dimensions
    /// </summary>
    private static Rectangle[] CalculateItemTiles()
    {
        Rectangle[] itemPositions = new Rectangle[NEW_ROW_THRESHOLD * NEW_ROW_THRESHOLD];

        for (int i = 0; i < itemPositions.Length; i++)
        {
            itemPositions[i] = new(
                INVENTORY_SIZE.OFFSET.X + ITEM_SIZE * i - ITEM_SIZE * NEW_ROW_THRESHOLD * (int)Math.Floor(i * (1f / NEW_ROW_THRESHOLD)),
                INVENTORY_SIZE.OFFSET.Y + ITEM_SIZE * (int)Math.Floor(i * (1f / NEW_ROW_THRESHOLD)),
                ITEM_SIZE,
                ITEM_SIZE
            );
        }
        return itemPositions;
    }

    /// <summary>
    /// Perform logic for grabbing items
    /// The player can drag and drop items to perform various actions
    /// </summary>
    private void CheckGrab()
    {
        //Check if the player has pressed left mouse on an item
        if (Raylib.IsMouseButtonPressed(MouseButton.Left))
        {
            Vector2 mousePosition = Raylib.GetMousePosition();
            for (int i = 0; i < items.Count; i++)
            {
                if (Raylib.CheckCollisionPointRec(mousePosition, INVENTORY_ITEM_TILES[i]))
                {
                    heldItem.item = items[i];
                    heldItem.listIndex = i;
                    heldItem.position = (
                        (int)Raylib.GetMousePosition().X - ITEM_SIZE / 2,
                        (int)Raylib.GetMousePosition().Y - ITEM_SIZE / 2
                    );
                    return;
                }
            }
        }
        //Update the position of the players held item if they are currently holding left mouse
        else if (heldItem.item is not null && Raylib.IsMouseButtonDown(MouseButton.Left))
        {
            heldItem.position = (
                (int)Raylib.GetMousePosition().X - ITEM_SIZE / 2,
                (int)Raylib.GetMousePosition().Y - ITEM_SIZE / 2
            );
            return;
        }
        //Check if the player has released the held item,
        //then equip it if it's aligned with a tile in the equipped item grid and has a matching type
        else if (heldItem.item != null && Raylib.IsMouseButtonReleased(MouseButton.Left) && heldItem.listIndex != null)
        {
            for (int i = 0; i < equippedItemTiles.Length; i++)
            {
                if (Raylib.CheckCollisionPointRec(Raylib.GetMousePosition(), equippedItemTiles[i])
                    && equippedItemsOrder[i] == heldItem.item.GetType())
                {
                    if (equippedItems[i] is not null)
                    {
                        items.Add(equippedItems[i]);
                    }

                    equippedItems[i] = heldItem.item;
                    items.RemoveAt((int)heldItem.listIndex);
                    heldItem.item = null;
                    heldItem.listIndex = null;
                    CalculateEquippedItemStats();
                    return;
                }
            }

            //Check if the players cursor aligns with the delete item tile
            if (Raylib.CheckCollisionPointRec(Raylib.GetMousePosition(), deleteItemTile))
            {
                items.RemoveAt((int)heldItem.listIndex);
                heldItem.item = null;
                heldItem.listIndex = null;
                return;
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

    /// <summary>
    /// Gives and equips an item for each equippable item slot the player has
    /// </summary>
    public void GiveInitialItems()
    {
        for (int i = 0; i < equippedItems.Length; i++)
        {
            Item? item = (Item?)Activator.CreateInstance(equippedItemsOrder[i], new object[] { 0 });
            if(item is null)
            {
                throw new Exception("Item was null");
            }
            else
            {
                equippedItems[i] = item;
            }
        }

        CalculateEquippedItemStats();
    }
    
    /// <summary>
    /// Calculates the total stats of all equipped items, then feeds it to the player
    /// </summary>
    public void CalculateEquippedItemStats()
    {
        int[] totalStats = new int[4];
        foreach(Item item in equippedItems)
        {
            for (int i = 0; i < totalStats.Length; i++)
            {
                totalStats[i] += item.stats[i];
            }
        }
        parentPlayer.UpdateStats(totalStats);
    }

    public void AddItem(Item item)
    {
        if(items.Count >= MAX_ITEM_COUNT)
        {
            return;
        }
        else
        {
            items.Add(item);
            return;
        }
    }

    
    private void DrawInventory()
    {
        //Inventory background
        Raylib.DrawRectangle(
            INVENTORY_SIZE.OFFSET.X,
            INVENTORY_SIZE.OFFSET.Y,
            INVENTORY_SIZE.WIDTH,
            INVENTORY_SIZE.HEIGHT,
            Color.DarkBlue
        );

        //Inventory items
        for (int i = items.Count - 1; i >= 0; i--)
        {
            //Item background
            Raylib.DrawRectangleRec(
                INVENTORY_ITEM_TILES[i],
                Item.rarityColors[items[i].rarity]
            );

            //Item icon
            Raylib.DrawTexture(
                items[i].texture,
                (int)INVENTORY_ITEM_TILES[i].X + textureOffset,
                (int)INVENTORY_ITEM_TILES[i].Y + textureOffset,
                Color.White
            );
        }

        //Equipped items
        for (int i = 0; i < equippedItemTiles.Length; i++)
        {
            //Item background
            Raylib.DrawRectangleRec(
                equippedItemTiles[i],
                equippedItems[i] is not null ? Item.rarityColors[equippedItems[i].rarity] : Color.DarkPurple
            );

            //Item icon
            Raylib.DrawTexture(
                equippedItems[i] is not null ? equippedItems[i].texture : Item.itemTextures[(int)Item.ItemTextures.Unknown],
                (int)equippedItemTiles[i].X + textureOffset,
                (int)equippedItemTiles[i].Y + textureOffset,
                Color.White
            );
        }

        //Delete item background
        Raylib.DrawRectangleRec(
            deleteItemTile,
            Color.Red
        );

        //Delete item icon
        Raylib.DrawTexture(
            deleteItemTexture,
            (int)deleteItemTile.X + textureOffset,
            (int)deleteItemTile.Y + textureOffset,
            Color.White
        );

        //Held item
        if (heldItem.item is not null)
        {
            //Held item background
            Raylib.DrawRectangle(
                heldItem.position.x,
                heldItem.position.y,
                ITEM_SIZE,
                ITEM_SIZE,
                Item.rarityColors[heldItem.item.rarity]
            );

            //Held item icon
            Raylib.DrawTexture(
                heldItem.item.texture,
                heldItem.position.x + textureOffset,
                heldItem.position.y + textureOffset,
                Color.White
            );
        }
    }
}