using Raylib_cs;
using Slutprojekt;

abstract class Character
{
    public int maxHealth, health, damage, defense;

    // Cached values
    protected int textYOffset;
    protected string characterType;

    public float attackSpeed, attackCooldown;
    public void PerformActions(Character opposingCharacter)
    {
        //Console.WriteLine();
        //Console.WriteLine($"Deltatime: {Raylib.GetFrameTime()}");

        if(health <= 0)
        {
            OnDeath(opposingCharacter);
            return;
        }

        attackCooldown += Raylib.GetFrameTime();
        if (attackCooldown >= attackSpeed)
        {
            attackCooldown -= attackSpeed;
            opposingCharacter.RecieveDamage(damage);
        }
    }
    public void RecieveDamage(int damage)
    {
        health -= damage;
    }

    public bool IsDead()
    {
        return health <= 0 ? true : false;
    }

    public void Recover()
    {
        //TODO instead of clamping the value to a minimum of 1, calculate the amount of health the character
        //needs to recover during the recovery period, and then slowly apply that change over time
        //to make it smoother

        health += Math.Clamp((int)(maxHealth * Raylib.GetFrameTime() / 2f), 1, 9999);
        if(health > maxHealth)
        {
            health = maxHealth;
        }
    }

    protected void RunOnConstruction()
    {
        characterType = GetType().ToString();
        textYOffset = characterType.Equals("Player") ? 300 : 500;
    }

    public void DrawInformation()
    {
        Utils.DrawCenteredText(
            characterType, 
            (int)((float)Program.screenSize.x * (1/3)), Program.screenSize.y / 2, 
            Program.DEFAULT_FONT_SIZE, 
            Color.White
        );

            Utils.DrawCenteredText(
            $"Health: {health}", 
            (int)((float)Program.screenSize.x * (2/3)), textYOffset + Program.screenSize.y / 2, 
            Program.DEFAULT_FONT_SIZE,
            Color.White
        );
    }

    abstract public void OnDeath(Character opposingCharacter);
}