using System.Diagnostics;
using Raylib_cs;

class CombatHandler
{
    Player player;
    Enemy enemy;
    float recoveryPeriod = 3;
    public float recoveryRemaining = 0;

    public CombatHandler(Player player, Enemy enemy)
    {
        this.player = player;
        this.enemy = enemy;
    }

    public void Start()
    {

    }
    public void NextStep()
    {
        Console.Clear();

        if(recoveryRemaining > 0)
        {
            player.Recover();
            enemy.Recover();
            recoveryRemaining -= Raylib.GetFrameTime();
        }

        enemy.PerformActions(player);
        if(player.IsDead())
        {
            player.OnDeath(enemy);
            recoveryRemaining = recoveryPeriod;
        }

        player.PerformActions(enemy);
        enemy.IsDead();
    }
}