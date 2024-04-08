using System.Numerics;
using Raylib_cs;
using Slutprojekt;

public class Utils
{
    public static void DrawCenteredText(string text, int x, int y, int textSize, Color color)
    {
        Raylib.DrawText(text, x - Raylib.MeasureText(text, textSize) / 2, y, textSize, color);
    }
}