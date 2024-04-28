using System.Numerics;
using Raylib_cs;

namespace Slutprojekt;

class Program
{
    static void Main(string[] args)
    {
        Raylib.InitWindow(Constants.SCREEN_SIZE.X, Constants.SCREEN_SIZE.Y, "Raylib");
        CombatHandler combatHandler = new(
            new(20, 4, 0, 1),
            new(1)
        );

        combatHandler.Start();

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