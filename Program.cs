using System.Numerics;
using System.Runtime.CompilerServices;
using Raylib_cs;

namespace Slutprojekt;

class Program
{
    public const int DEFAULT_FONT_SIZE = 45;
    public static readonly (int x, int y) SCREEN_SIZE = new(1600, 1000);
    static void Main(string[] args)
    {
        CombatHandler combatHandler = new(
            new(20, 4, 0, 1),
            new(1)
            );

        combatHandler.Start();

        Raylib.InitWindow(SCREEN_SIZE.x, SCREEN_SIZE.y, "Raylib");
        Raylib.SetTargetFPS(60);
        
        while (!Raylib.WindowShouldClose())
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.Black);

            combatHandler.NextStep();

            Raylib.EndDrawing();
        }
    }
}