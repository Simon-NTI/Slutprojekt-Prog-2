using System.Reflection.Metadata;
using Raylib_cs;
using Slutprojekt;

abstract class Character
{
    protected int maxHealth, health, damage, defense;
    private float healthRecoveryBuffer, healthRecoveryPerSecond;
    protected string characterName;
    (int x, int y) textOffset;
    protected float attackSpeed, attackCooldown;

    private const int HEALTHBAR_LENGTH = 300;
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
        new FloatingText((textOffset.x + 200, textOffset.y), (textOffset.x + 200, textOffset.y - 150), 3, "-" + damage.ToString(), Color.Red);
    }

    public bool IsDead()
    {
        return health <= 0;
    }


    public void BeginRecovering(float recoveryPeriod)
    {
        healthRecoveryBuffer = 0;
        healthRecoveryPerSecond = (maxHealth - health) / recoveryPeriod * (float)Raylib.GetFrameTime();
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
        int yOffset = (int)((Constants.SCREEN_SIZE.Y - 400) / 3f); 
        textOffset.y = GetType().ToString().Equals("Player") ? yOffset : 2 * yOffset + 200;
    }

    public void DrawInformation()
    {
        //Draw the name of the character
        Raylib.DrawText(
            characterName,
            textOffset.x,
            textOffset.y,
            Constants.DEFAULT_FONT_SIZE,
            Color.White
        );

        // Draw healthbar
        {
            Raylib.DrawRectangle(
                textOffset.x - (HEALTHBAR_LENGTH - Raylib.MeasureText(characterName, Constants.DEFAULT_FONT_SIZE)) / 2,
                textOffset.y - 60,
                HEALTHBAR_LENGTH,
                50,
                Color.Red
            );

            Raylib.DrawRectangle(
                textOffset.x - (HEALTHBAR_LENGTH - Raylib.MeasureText(characterName, Constants.DEFAULT_FONT_SIZE)) / 2,
                textOffset.y - 60,
                (int)((float)health / maxHealth * HEALTHBAR_LENGTH),
                50,
                Color.Green
            );
            
            // Draw the amount of health the character has
            Raylib.DrawText(
                $"Health: {health}", 
                textOffset.x, 
                textOffset.y - 60, 
                Constants.DEFAULT_FONT_SIZE,
                Color.Black
            );
        }
    }

    abstract public void OnDeath(Character opposingCharacter);
}