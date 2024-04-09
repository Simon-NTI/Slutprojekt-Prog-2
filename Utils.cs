using System.Numerics;
using Raylib_cs;
using Slutprojekt;

public class Utils
{
    /// <summary>
    /// Draws text centered vertically on the given x value
    /// </summary>
    /// <param name="text"></param>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="textSize"></param>
    /// <param name="color"></param> <summary>
    /// 
    /// </summary>
    /// <param name="text"></param>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="textSize"></param>
    /// <param name="color"></param>
    public static void DrawCenteredText(string text, int x, int y, int textSize, Color color)
    {
        Raylib.DrawText(text, x - Raylib.MeasureText(text, textSize) / 2, y, textSize, color);
    }
}