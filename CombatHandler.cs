using System.Diagnostics;
using Raylib_cs;
using Slutprojekt;

class CombatHandler
{
    Player player;
    Enemy enemy;
    float recoveryPeriod = 2;
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
            WhileRecovering();
            return;
        }

        enemy.PerformActions(player);
        enemy.DrawInformation();
        if(player.IsDead())
        {
            player.OnDeath(enemy);
            recoveryRemaining = recoveryPeriod;
        }

        player.PerformActions(enemy);
        player.DrawInformation();
        enemy.IsDead();
    }

    private void WhileRecovering()
    {
        Utils.DrawCenteredText(
            "Player is recovering...",
            Program.screenSize.x / 2, 100,
            Program.DEFAULT_FONT_SIZE + 20,
            Color.Green
        );

        player.Recover();
        player.DrawInformation();

        enemy.Recover();
        enemy.DrawInformation();

        recoveryRemaining -= Raylib.GetFrameTime();
        if (recoveryRemaining <= 0)
        {
            enemy = new Enemy(1);
        }
        return;
    }
}