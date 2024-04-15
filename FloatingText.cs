using System.Numerics;
using Raylib_cs;

class FloatingText
{
    (int x, int y) initialPosition, currentPosition, targetPosition, increasePerUpdate;
    string textContent;

    public FloatingText((int x, int y) initialPosition, (int x, int y) targetPosition, float transitionTime)
    {
        increasePerUpdate.x = (int)(targetPosition.x - initialPosition.x / transitionTime);
        increasePerUpdate.y = (int)(targetPosition.y - initialPosition.y / transitionTime);
        
        this.initialPosition = initialPosition;
        currentPosition = initialPosition;
        this.targetPosition = targetPosition;
    }

    public void Update()
    {
        currentPosition = 
        Raylib.DrawText("")
    }
}