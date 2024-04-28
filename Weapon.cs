public class Weapon : Item
{    int damage;
    float attackSpeed; // Attack cooldown

    public Weapon(int damage, float attackSpeed)
    {
        this.damage = damage;
        this.attackSpeed = attackSpeed;
    }
}