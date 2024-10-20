
abstract class Enemy : LevelElement
{
    protected Enemy(Position position, char icon, ConsoleColor iconcolor, LevelData levelData) : base(position, icon, iconcolor)
    {
        this.levelData = levelData;
    }

    public string Name { get; set; }
    public int HealthPoints { get; set; }
    public Dice AttackDice { get; set; }
    public Dice DefenceDice { get; set; }
    public LevelData levelData { get; }


    public abstract void Update(Position position);
    
    public void Attack(Player player)
    {
        ClearText();
        int enemyAttackScore = AttackDice.Throw();
        int enemyDefenceScore = DefenceDice.Throw();
        int playerAttackScore = player.AttackDice.Throw();
        int playerDefenceScore = player.DefenceDice.Throw();

        if (enemyAttackScore - playerDefenceScore > 0 && this.HealthPoints > 0)
        {
            int damage = enemyAttackScore - playerDefenceScore;
            player.HealthPoints -= damage;
            Console.SetCursorPosition(0, 1);
            if (this is Snake)
            {
                Console.ForegroundColor = this.IconColor;
            }
            else if (this is Rat)
            {
                Console.ForegroundColor = this.IconColor;
            }
            Console.Write($"The {this} (ATK: {this.AttackDice.ToString()} => {enemyAttackScore}) attacked you (DEF: {player.DefenceDice.ToString()} => {playerDefenceScore})");
            Console.ResetColor();
        }
        else if (enemyAttackScore - playerDefenceScore < 0 && this.HealthPoints > 0)
        {
            Console.SetCursorPosition(0, 1);
            Console.ForegroundColor = IconColor;
            Console.Write($"{this} tried to attack but couldn't do any damage. ");
            Console.ResetColor(); 
        }
        if (playerAttackScore - enemyDefenceScore > 0 && this.HealthPoints > 0)
        {
            int damage = playerAttackScore - enemyDefenceScore;
            HealthPoints -= damage;
            Console.SetCursorPosition(0, 2);
            Console.ForegroundColor = player.IconColor;
            Console.Write($"You (ATK: {player.AttackDice.ToString()} => {playerAttackScore}) attacked the {this} (DEF: {this.DefenceDice.ToString()} => {enemyDefenceScore}) {this}'s health: {this.HealthPoints}");
            Console.ResetColor();
        }
        else if (playerAttackScore - enemyDefenceScore < 0 && this.HealthPoints > 0)
        {
            Console.SetCursorPosition(0, 2);
            Console.ForegroundColor = player.IconColor;
            Console.Write($"You tried to attack but couldn't do any damage.");
        }
        if (this.HealthPoints <= 0)
        {
            Console.SetCursorPosition(this.Position.X, this.Position.Y);
            Console.Write(' ');
            levelData.Elements.Remove(this);
            Console.SetCursorPosition(0, 1);
            Console.ForegroundColor = this.IconColor;
            Console.Write($"You (ATK: {player.AttackDice.ToString()} => {playerAttackScore}), killing the {this}.");

        }
    }
}

