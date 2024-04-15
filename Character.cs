using Raylib_cs;
using Slutprojekt;

abstract class Character
{
    public int maxHealth, health, damage, defense;
    private float healthRecoveryBuffer, healthRecoveryPerSecond;


    // Cached values
    protected string characterName;
    (int x, int y) textOffset;
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


    public void BeginRecovering(float recoveryPeriod)
    {
        healthRecoveryBuffer = 0;
        healthRecoveryPerSecond = (maxHealth - health) / 2f * (float)Raylib.GetFrameTime();
        health += 1;
    }
    public void Recover()
    {
        healthRecoveryBuffer += healthRecoveryPerSecond;

        if(healthRecoveryBuffer >= 1)
        {
            health += 1;
            healthRecoveryBuffer -= 1;
            if (health > maxHealth) health = maxHealth;
        }
    }

    protected void RunOnConstruction()
    {
        characterName = "- " + GetType().ToString() + " -";

        textOffset.x = 100;
        int yOffset = (int)((Program.SCREEN_SIZE.y - 400) / 3f); 
        textOffset.y = GetType().ToString().Equals("Player") ? yOffset : 2 * yOffset + 200;
    }

    public void DrawInformation()
    {
        Raylib.DrawText(
            characterName,
            textOffset.x,
            textOffset.y,
            Program.DEFAULT_FONT_SIZE,
            Color.White
        );

        Raylib.DrawText(
            $"Health: {health}", 
            textOffset.x, 
            textOffset.y + 60, 
            Program.DEFAULT_FONT_SIZE,
            Color.White
        );
    }

    abstract public void OnDeath(Character opposingCharacter);
}