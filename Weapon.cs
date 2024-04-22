public class Weapon : Item
{
    readonly static string[] possibleAdjectives = new string[]
    {
        "Awesome", "Shiny", "Large", "Legendary"
    };
    readonly static string[] possibleSubjects = new string[]
    {
        "Stick", "Sword", "Spoon", "Fork", "Broom"
    };
    readonly static string[] possibleNouns = new string[]
    {
        "of Time", "of Peace", "of Freedom", "of Violence"
    };

    string name;
    int damage;
    float attackSpeed; // Attack cooldown

    public Weapon(string name, int damage, float attackSpeed)
    {
        this.name = GenerateName();
        this.damage = damage;
        this.attackSpeed = attackSpeed;
    }

    private static string GenerateName()
    {
        Random generator = new();

        return possibleAdjectives[generator.Next(possibleAdjectives.Length)]
        + " " + possibleSubjects[generator.Next(possibleSubjects.Length)]
        + " " + possibleNouns[generator.Next(possibleNouns.Length)];
    }
}