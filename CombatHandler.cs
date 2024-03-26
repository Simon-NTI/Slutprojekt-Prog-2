using System.Diagnostics;

class CombatHandler
{
    Player player;
    Enemy enemy;

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
        enemy.PerformActions(player);
        player.PerformActions(enemy);
    }
}