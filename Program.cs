using System.Numerics;
using System.Runtime.CompilerServices;
using Raylib_cs;

namespace Slutprojekt;

class Program
{
    public const int DEFAULT_FONT_SIZE = 45;
    public static readonly (int x, int y) SCREEN_SIZE = new(800, 1000);
    static void Main(string[] args)
    {
        Enemy enemy = new(1);
        Player player = new(20, 4, 0, 1);
        CombatHandler combatHandler = new(player, enemy);
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