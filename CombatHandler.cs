/// <summary>
/// A combat handler handles combat between two characters
/// </summary>

using Raylib_cs;

class CombatHandler
{
    //TODO the player should be given instructions on how to play
    //TODO enemies should drop items based on their loot table
    private int currentStage = 1;
    private int unlockedStage = 1;
    private Player player;
    private Enemy enemy;
    const float recoveryPeriod = 2;
    public float recoveryRemaining = 0;

    /// <summary>
    /// Constructor
    /// </summary>
    public CombatHandler(Player player, Enemy enemy)
    {
        this.player = player;
        this.enemy = enemy;
    }

    /// <summary>
    /// Logic to perform every update
    /// </summary>
    public void NextStep()
    {
        FloatingText.UpdateAllInstances();
        DrawInformation();
        player.Inventory.DrawInformation();

        HandleMoveStage();

        if(recoveryRemaining > 0)
        {
            WhileRecovering();
        }
        else
        {
            WhileCombat();
        }
    }

    /// <summary>
    /// Logic to perform during combat
    /// </summary>
    private void WhileCombat()
    {
        enemy.PerformActions(player);
        enemy.DrawInformation();
        if(player.IsDead())
        {
            player.OnDeath(enemy);
            BeginRecovery();
            return;
        }

        player.PerformActions(enemy);
        player.DrawInformation();
        enemy.IsDead();
        if(enemy.IsDead())
        {
            if(currentStage == unlockedStage)
            {
                unlockedStage++;
            }

            enemy.OnDeath(player);
            BeginRecovery();
            return;
        }
    }

    /// <summary>
    /// Initiate a recovery period, heals all characters to full health over the course of a set amount of time
    /// At the end of the recovery period, a new enemy will be instantiated
    /// </summary>
    private void BeginRecovery()
    {
        recoveryRemaining = recoveryPeriod;
        player.BeginRecovering(recoveryPeriod);
        enemy.BeginRecovering(recoveryPeriod);
    }

    /// <summary>
    /// Logic to perform while recovering
    /// </summary>
    private void WhileRecovering()
    {
        IUtils.DrawCenteredText(
            "Recovering...",
            Constants.SCREEN_SIZE.X / 2 - 200, 
            100,
            Constants.DEFAULT_FONT_SIZE,
            Color.Green
        );

        player.Recover();
        player.DrawInformation();

        enemy.Recover();
        enemy.DrawInformation();

        recoveryRemaining -= Raylib.GetFrameTime();
        if (recoveryRemaining <= 0)
        {
            enemy = new Enemy(currentStage);
        }
        return;
    }

    /// <summary>
    /// Display certain values that are important to the
    /// </summary>
    private void DrawInformation()
    {
        //Display current stage number
        Raylib.DrawText("Current Stage: " + currentStage, 50, 20, Constants.DEFAULT_FONT_SIZE - 10, Color.White);

        //Display unlocked stage number
        Raylib.DrawText("Highest Unlocked Stage: " + unlockedStage, 50, 60, Constants.DEFAULT_FONT_SIZE - 10, Color.Gold);
    }


    /// <summary>
    /// Check if the player made any inputs to move to a different stage
    /// </summary>
    private void HandleMoveStage()
    {
        if(Raylib.IsKeyPressed(KeyboardKey.Left) && currentStage > 1)
        {
            BeginRecovery();
            currentStage--;
        }
        else if(Raylib.IsKeyPressed(KeyboardKey.Right) && currentStage < unlockedStage)
        {
            BeginRecovery();
            currentStage++;
        }
    }
}