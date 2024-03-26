using System.Numerics;
using System.Runtime.CompilerServices;
using Raylib_cs;

namespace Slutprojekt;

class Program
{
    public static (int x, int y) screenSize = new(800, 1000);
    static void Main(string[] args)
    {
        Enemy enemy = new(10, 2, 0, 1, 1);
        Player player = new(20, 4, 0, 1);
        CombatHandler combatHandler = new(player, enemy);
        combatHandler.Start();


        Raylib.InitWindow(screenSize.x, screenSize.y, "Raylib");
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