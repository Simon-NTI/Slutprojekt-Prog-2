using Raylib_cs;
using Slutprojekt;

abstract class Character
{
    public int maxHealth, health, damage, defense;
    public float attackCooldown, currentAttackCooldown, attackSpeed;
    protected Character(int maxHealth, int damage, int defense, float attackSpeed)
    {
        this.maxHealth = maxHealth;
        health = maxHealth;
        this.damage = damage;
        this.defense = defense;
        this.attackSpeed = attackSpeed;
        attackCooldown = 1f / attackSpeed;
        currentAttackCooldown = 0;
    }

    public void PerformActions(Character opposingCharacter)
    {
        //Console.WriteLine();
        //Console.WriteLine($"Deltatime: {Raylib.GetFrameTime()}");

        if(health <= 0)
        {
            OnDeath(opposingCharacter);
            return;
        }

        //Type characterType = GetType();
        int textYOffset = GetType().ToString().Equals("Player") ? 400 : 500;

        string text = $"{GetType()} Health: {health}";
        Utils.DrawCenteredText(text, Program.screenSize.x / 2, textYOffset, 30, Color.White);

        currentAttackCooldown += Raylib.GetFrameTime();
        if (currentAttackCooldown >= attackCooldown)
        {
            currentAttackCooldown -= attackCooldown;
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
        //needs to recover during the recovery period, and then slowsly apply that change over time
        //to make it smoother

        health += Math.Clamp((int)(maxHealth / Raylib.GetFrameTime() / 2f), 1, 9999);
    }

    abstract public void OnDeath(Character opposingCharacter);
}