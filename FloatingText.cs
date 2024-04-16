using System.Numerics;
using System.Xml;
using Raylib_cs;
using Slutprojekt;

class FloatingText
{
    public static List<FloatingText> floatingTexts = new();
    (float x, float y) initialPosition, currentPosition, targetPosition, changePerSecond;
    float lifeTime, timeAlive = 0;
    string content;
    Color color;
    public FloatingText((int x, int y) initialPosition, (int x, int y) targetPosition, float transitionTime, string content, Color color)
    {
        this.color = color;
        lifeTime = transitionTime;

        changePerSecond.x = (int)((targetPosition.x - initialPosition.x) / transitionTime);
        changePerSecond.y = (int)((targetPosition.y - initialPosition.y) / transitionTime);
        
        this.initialPosition = initialPosition;
        currentPosition = initialPosition;
        this.targetPosition = targetPosition;
        this.content = content;

        Console.WriteLine(
            $"Initial position: {initialPosition}\n"
        + $"Target position {targetPosition}\n"
        + $"Change per second {changePerSecond}");

        floatingTexts.Add(this);
    }

    public static void UpdateAllInstances()
    {
        for (int i = floatingTexts.Count - 1; i >= 0 ; i--)
        {
            //Console.WriteLine("There are currently {0} instances of floating text", floatingTexts.Count);
            if(floatingTexts[i].UpdateItem()) floatingTexts.RemoveAt(i);
        }
    }

    private bool UpdateItem()
    {
        //TODO decrease text oppacity based on life remaining
        if (timeAlive >= lifeTime)
        {
            return true;
        }

        timeAlive += Raylib.GetFrameTime();

        currentPosition.x += changePerSecond.x * Raylib.GetFrameTime();
        currentPosition.y += changePerSecond.y *  Raylib.GetFrameTime();

        Raylib.DrawText(content, (int)currentPosition.x, (int)currentPosition.y, Program.DEFAULT_FONT_SIZE, color);
        return false;
    }
}