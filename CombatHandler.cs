using System.Diagnostics;

class CombatHandler
{
    float deltaTime, timeUntilNextUpdate, targetUpdateRate;
    bool stop = false;
    int targetFPS = 10;

    Player player;
    List<Enemy> enemies;

    public CombatHandler(int targetUpdateRate, Player player, List<Enemy> enemies)
    {
        targetFPS = targetUpdateRate;
        this.player = player;
        this.enemies = enemies;
    }

    public void Start()
    {
        targetUpdateRate = 1f / targetFPS * 1000;
        timeUntilNextUpdate = targetUpdateRate;
        Update();
    }
    private void Update()
    {
        Stopwatch stopwatch = new();
        while(true)
        {
            stopwatch.Start();

            // Primary logic
            if(!stop)
            {
                NextStep();
            }

            stopwatch.Stop();
            deltaTime = stopwatch.ElapsedMilliseconds;
            timeUntilNextUpdate = targetUpdateRate - stopwatch.ElapsedMilliseconds;
            stopwatch.Reset();
            Console.WriteLine("Until Next: " + timeUntilNextUpdate);
            Console.WriteLine("Deltatime: " + deltaTime);

            if(timeUntilNextUpdate >= 0)
            {
                Thread.Sleep((int)timeUntilNextUpdate);
            }
        }
    }
    private void NextStep()
    {
        Console.WriteLine();

    }
}