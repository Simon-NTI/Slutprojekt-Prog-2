using System.IO.Compression;
using System.Numerics;
using Raylib_cs;

public class Item
{
    public bool isHeld = false;
    (int x, int y) position;

    public Texture2D texture;
}