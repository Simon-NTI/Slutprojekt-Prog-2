/// <summary>
/// The Item type contains statistical increases scaling with rarity
/// </summary>

using Raylib_cs;

public abstract class Item
{
    //Textures to represent each item type
    public static readonly Texture2D[] itemTextures = new Texture2D[4]
    {
        Raylib.LoadTexture("Assets/Armor.png"),
        Raylib.LoadTexture("Assets/Necklace.png"),
        Raylib.LoadTexture("Assets/Weapon.png"),
        Raylib.LoadTexture("Assets/Unknown.png")
    };
    public enum ItemTextures
    {
        Armor,
        Necklace,
        Weapon,
        Unknown
    }
    public Texture2D texture; //The texture of this item
    public int rarity;

    public int[] stats = new int[4];

    public int Damage
    {
        get => stats[0];
        set => stats[0] = value;
    }
    public int Health
    {
        get => stats[1];
        set => stats[1] = value;
    }
    public int AttackSpeed //Attackspeed is given in attack per minute
    {
        get => stats[2];
        set => stats[2] = value;
    }
    public int Defense
    {
        get => stats[3];
        set => stats[3] = value;
    }

    /// <summary>
    /// Constructor
    /// Each item starts with zero stats
    /// each item type individually decides the stats of the item in their corresponding constructor
    /// </summary>
    public Item(int rarity)
    {
        Damage = 0;
        Health = 0;
        AttackSpeed = 0;
        Defense = 0;
        this.rarity = rarity;

        if (Enum.TryParse(typeof(ItemTextures), GetType().ToString(), true, out object? result))
        {
            texture = itemTextures[(int)result];
        }
        else
        {
            texture = itemTextures[(int)ItemTextures.Unknown];
        }
    }

    public static readonly Color[] rarityColors = new Color[5]
    {
        Color.White,
        Color.Green,
        Color.Blue,
        Color.Purple,
        Color.Orange
    };
    public enum Rarities : int
    {
        Common,
        Uncommon,
        Rare,
        Epic,
        Legendary
    }
}