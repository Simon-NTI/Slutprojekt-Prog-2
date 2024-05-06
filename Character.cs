/// <summary>
/// A character is a type which contains logic which allos them to perform combat against other characters
/// </summary>

using Raylib_cs;
abstract class Character
{
    protected int maxHealth, health, damage, defense;
    private float healthRecoveryBuffer, healthRecoveryPerSecond;
    protected string characterName;
    (int x, int y) textOffset;
    protected float attackSpeed, attackCooldown;
    private const int HEALTHBAR_LENGTH = 300;
    
    /// <summary>
    /// A common constructor for all characters
    /// </summary>
    public Character()
    {
        RunOnConstruction();
    }    
    
    /// <summary>
    /// Logic to perform each step
    /// </summary>
    public void PerformActions(Character opposingCharacter)
    {
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

    /// <summary>
    /// Apply the given damage value to the character
    /// </summary>
    public void RecieveDamage(int damage)
    {
        health -= damage;

        Random generator = new();
        new FloatingText(
            (textOffset.x + 250, textOffset.y - 20), 
            (textOffset.x + 250 + (30 - generator.Next(60)), textOffset.y + 150  + (30 - generator.Next(60))), 
            3, 
            "-" + damage.ToString(), 
            Color.Red);
    }

    /// <summary>
    /// If character health is less than 0, return true
    /// </summary>
    public bool IsDead() => health <= 0;

    /// <summary>
    /// Calculates how much health the character is missing
    /// then calculate how much health the player should recieve every second to fully replenish their health
    /// before the end of the recovery period 
    /// </summary>
    public void BeginRecovering(float recoveryPeriod)
    {
        healthRecoveryBuffer = 0;
        healthRecoveryPerSecond = (maxHealth - health) / recoveryPeriod * (float)Raylib.GetFrameTime();
        health += 1;
        attackCooldown = 0;
    }

    /// <summary>
    /// Heal the character at set intervals
    /// </summary>
    public void Recover()
    {
        healthRecoveryBuffer += healthRecoveryPerSecond;

        if(healthRecoveryBuffer >= 1)
        {
            health += 1;
            healthRecoveryBuffer -= 1;
        }

        if(health >= maxHealth)
        {
            health = maxHealth;
        }
    }

    /// <summary>
    /// Logic to perform when a character is constructed
    /// </summary>
    protected void RunOnConstruction()
    {
        characterName = "- " + GetType().ToString() + " -";

        textOffset.x = 100;
        int yOffset = (int)((Constants.SCREEN_SIZE.Y - 250) / 3f); 
        textOffset.y = GetType().ToString().Equals("Player") ? yOffset : 2 * yOffset + 200;
    }

    /// <summary>
    /// Display character related information
    /// </summary>
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

        //Draw healthbar
        {
            //Background
            Raylib.DrawRectangle(
                textOffset.x - (HEALTHBAR_LENGTH - Raylib.MeasureText(characterName, Constants.DEFAULT_FONT_SIZE)) / 2,
                textOffset.y - 60,
                HEALTHBAR_LENGTH,
                50,
                Color.Red
            );

            //Foreground
            Raylib.DrawRectangle(
                textOffset.x - (HEALTHBAR_LENGTH - Raylib.MeasureText(characterName, Constants.DEFAULT_FONT_SIZE)) / 2,
                textOffset.y - 60,
                (int)((float)health / maxHealth * HEALTHBAR_LENGTH),
                50,
                Color.Green
            );
            
            //Text
            Raylib.DrawText(
                $"Health: {health}", 
                textOffset.x, 
                textOffset.y - 60, 
                Constants.DEFAULT_FONT_SIZE,
                Color.Black
            );
        }

        //Draw visual indicator for attack cooldown
        {
            //Background
            Raylib.DrawRectangle(
                textOffset.x - (HEALTHBAR_LENGTH - Raylib.MeasureText(characterName, Constants.DEFAULT_FONT_SIZE)) / 2,
                textOffset.y - 135,
                HEALTHBAR_LENGTH,
                50,
                Color.Red
            );

            //Foreground
            Raylib.DrawRectangle(
                textOffset.x - (HEALTHBAR_LENGTH - Raylib.MeasureText(characterName, Constants.DEFAULT_FONT_SIZE)) / 2,
                textOffset.y - 135,
                (int)(attackCooldown / attackSpeed * HEALTHBAR_LENGTH),
                50,
                Color.SkyBlue
            );
            
            //Text
            Raylib.DrawText(
                $"{Math.Round(attackSpeed - attackCooldown, 2)}", 
                textOffset.x + HEALTHBAR_LENGTH / 2 - 80, 
                textOffset.y - 135, 
                Constants.DEFAULT_FONT_SIZE,
                Color.Black
            );
        }
    }

    /// <summary>
    /// Logic to perform when the character dies
    /// </summary>
    abstract public void OnDeath(Character opposingCharacter);
}