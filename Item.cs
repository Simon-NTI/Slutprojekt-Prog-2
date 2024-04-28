using System.IO.Compression;
using System.Numerics;
using Raylib_cs;

public abstract class Item
{
    public Texture2D texture;
    public int rarity = 0;

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