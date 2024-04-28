using System.Diagnostics;
using Raylib_cs;
using Slutprojekt;

class CombatHandler
{
    int stage = 1;
    Player player;
    Enemy enemy;
    const float recoveryPeriod = 2;
    public float recoveryRemaining = 0;

    public CombatHandler(Player player, Enemy enemy)
    {
        this.player = player;
        this.enemy = enemy;
    }

    public void Start()
    {
        player.Inventory.GiveInitialItems();
    }
    public void NextStep()
    {
        FloatingText.UpdateAllInstances();
        DrawInformation();
        player.Inventory.DrawInformation();

        if(recoveryRemaining > 0)
        {
            WhileRecovering();
        }
        else
        {
            WhileCombat();
        }
    }

    private void WhileCombat()
    {
        enemy.PerformActions(player);
        enemy.DrawInformation();
        if(player.IsDead())
        {
            player.OnDeath(enemy);
            recoveryRemaining = recoveryPeriod;
            player.BeginRecovering(recoveryPeriod);
            enemy.BeginRecovering(recoveryPeriod);
        }

        player.PerformActions(enemy);
        player.DrawInformation();
        enemy.IsDead();
        if(enemy.IsDead())
        {
            enemy.OnDeath(player);
            recoveryRemaining = recoveryPeriod;
            player.BeginRecovering(recoveryPeriod);
            enemy.BeginRecovering(recoveryPeriod);
        }
    }

    private void WhileRecovering()
    {
        Utils.DrawCenteredText(
            "Player is recovering...",
            Constants.SCREEN_SIZE.X / 2, 100,
            Constants.DEFAULT_FONT_SIZE + 20,
            Color.Green
        );

        player.Recover();
        player.DrawInformation();

        enemy.Recover();
        enemy.DrawInformation();

        recoveryRemaining -= Raylib.GetFrameTime();
        if (recoveryRemaining <= 0)
        {
            enemy = new Enemy(stage);
        }
        return;
    }

    private void DrawInformation()
    {
        Raylib.DrawText("Stage: " + stage, 50, 50, Constants.DEFAULT_FONT_SIZE - 10, Color.White);
    }
}