using System.Diagnostics;

class CombatHandler
{
    float deltaTime, timeUntilNextUpdate, targetUpdateRate;
    bool stop = false;
    int _targetUpdateRate = 10;
    int framesElapsed = 0;

    public void Start()
    {
        //mainThread = new Thread(Update);
        targetUpdateRate = 1f / _targetUpdateRate * 1000;
        timeUntilNextUpdate = targetUpdateRate;
        Update();
    }
    private void Update()
    {
        Stopwatch debugTimer = new();
        debugTimer.Start();
        Stopwatch stopwatch = new();
        while(true)
        {
            stopwatch.Start();

            // Primary logic
            if(!stop)
            {
                NextStep();
                framesElapsed += 1;
                Console.WriteLine("This is frame nr: " + framesElapsed);
            }

            stopwatch.Stop();
            deltaTime = stopwatch.ElapsedMilliseconds;
            timeUntilNextUpdate = targetUpdateRate - stopwatch.ElapsedMilliseconds;
            stopwatch.Reset();
            Console.WriteLine("Until Next: " + timeUntilNextUpdate);
            Console.WriteLine("Deltatime: " + deltaTime);

            if(timeUntilNextUpdate >= 0)
            {
                //await Task.Delay((int)timeUntilNextUpdate);
                Thread.Sleep((int)timeUntilNextUpdate);
            }
            if(framesElapsed > 200)
            {
                debugTimer.Stop();
                Console.WriteLine("Total time elapsed: " + debugTimer.ElapsedMilliseconds);
                Console.WriteLine("Total frames: " + framesElapsed);
                break;
            }
        }
    }

    private void NextStep()
    {


    }
}