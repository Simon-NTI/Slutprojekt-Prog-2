using Raylib_cs;
using Slutprojekt;

abstract class Character
{
    public int health, damage, defense;
    public float attackCooldown, currentAttackCooldown, attackSpeed;

    protected Character(int health, int damage, int defense, float attackSpeed)
    {
        this.health = health;
        this.damage = damage;
        this.defense = defense;
        this.attackSpeed = attackSpeed;
        attackCooldown = 1f / attackSpeed;
        currentAttackCooldown = 0;
    }

    public void PerformActions(Character oppCharacter)
    {
        Console.WriteLine();
        Console.WriteLine($"Deltatime: {Raylib.GetFrameTime()}");


        Type characterType = GetType();

        int textYOffset = characterType.ToString().Equals("Player") ? 400 : 500;

        Raylib.DrawText($"{GetType()} Health: {health}", Program.screenSize.x / 2 - Raylib.MeasureText($"{GetType()} Health: {health}", 30) / 2, textYOffset, 30, Color.White);

        currentAttackCooldown += Raylib.GetFrameTime();
        if(currentAttackCooldown >= attackCooldown)
        {
            currentAttackCooldown -= attackCooldown;
            oppCharacter.RecieveDamage(damage);
        }
    }

    public void RecieveDamage(int damage)
    {
        health -= damage;
    }
}