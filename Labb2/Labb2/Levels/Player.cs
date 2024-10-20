
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

internal class Player : LevelElement
{
    private LevelData levelData;
    public int HealthPoints { get; set; }
    public Dice AttackDice { get; set; }
    public Dice DefenceDice { get; set; }

    public Player(Position position, LevelData levelData) : base(position, '@', ConsoleColor.Yellow)
    {
        this.HealthPoints = 100;
        this.levelData = levelData;
        this.AttackDice = new Dice(2, 6, 2);
        this.DefenceDice = new Dice(1, 6, 0);
    }


    public void Update()
    {
        ShowElements();
        Draw();
        Console.CursorVisible = false;
        var key = Console.ReadKey(true).Key;
        var newPosition = new Position(Position.X, Position.Y);
        
        switch (key)
        {
            case ConsoleKey.RightArrow:
                newPosition.X += 1;
                break;
            case ConsoleKey.LeftArrow:
                newPosition.X -= 1;
                break;
            case ConsoleKey.UpArrow:
                newPosition.Y -= 1;
                break;
            case ConsoleKey.DownArrow:
                newPosition.Y += 1;
                break;
            case ConsoleKey.Escape:
                Environment.Exit(0);
                return;

        }

        if (levelData.CanMoveTo(newPosition))
        {
            LevelElement elementAtNewPosition = levelData.GetLevelElementAt(newPosition);
            if (elementAtNewPosition is Enemy enemy)
            {
                ClearText();
                Attack(enemy);
                return;
            }

            Console.SetCursorPosition(Position.X, Position.Y);
            Console.Write(' ');
            Position = newPosition;
            Draw();

            ShowElements();

        }

        ClearText();
    }

    private void ShowElements()
    {
        foreach (LevelElement levelElement in levelData.Elements)
        {
            if (Position.InRange(Position, levelElement.Position, 5))
            {
                if (levelElement is Rat)
                {
                    levelElement.Draw();
                }
                levelElement.Draw();
            }
            else
            {
                if (levelElement is not Wall)
                {
                    levelElement.Hide();
                }
            }
        }
    }
    private void Attack(Enemy enemy)
    {
        if (enemy is Rat rat)
        {
            int playerAttackScore = AttackDice.Throw();
            int playerDefenceScore = DefenceDice.Throw();
            int ratAttackScore = rat.AttackDice.Throw();
            int ratDefenceScore = rat.DefenceDice.Throw();

            if (playerAttackScore - ratDefenceScore > 0 && rat.HealthPoints > 0)
            {
                int damage = playerAttackScore - ratDefenceScore;
                rat.HealthPoints -= damage;
                Console.SetCursorPosition(0, 1);
                Console.ForegroundColor = IconColor;
                Console.Write($"You (ATK: {AttackDice.ToString()} => {playerAttackScore}) attacked the rat (DEF: {rat.DefenceDice.ToString()} => {ratDefenceScore}). Rat's health: {rat.HealthPoints}");
                Console.ResetColor();
            }
            if (rat.HealthPoints <= 0)
            {
                //tar bort råttan ur listan och suddar ut den från banan
                Console.SetCursorPosition(rat.Position.X, rat.Position.Y);
                Console.Write(' ');
                levelData.Elements.Remove(rat);
                Console.SetCursorPosition(0, 1);
                Console.ForegroundColor = IconColor;
                Console.Write($"You (ATK: {AttackDice.ToString()} => {playerAttackScore}) attacked the rat (DEF: {rat.DefenceDice.ToString()} => {ratDefenceScore}), killing the rat.");
                Console.ResetColor();
                return; //råttan gör ej en motattack om den dör


            }
            if (ratAttackScore - playerDefenceScore > 0)
            {
                int damage = ratAttackScore - playerDefenceScore;
                HealthPoints -= damage;
                Console.SetCursorPosition(0, 2);
                Console.ForegroundColor = rat.IconColor;
                Console.Write($"The rat (ATK: {rat.AttackDice.ToString()} => {ratAttackScore}) attacked you (DEF: {DefenceDice.ToString()} => {playerDefenceScore})");
                Console.ResetColor();
            }
            else if (ratAttackScore - playerDefenceScore < 0)
            {
                Console.SetCursorPosition(0, 2);
                Console.ForegroundColor = rat.IconColor;
                Console.Write("The rat tried to attack but couldn't do any damage.");
                Console.ResetColor();
            }

        }
        else if (enemy is Snake snake) 
        {
            int playerAttackScore = AttackDice.Throw();
            int playerDefenceScore = DefenceDice.Throw();
            int snakeAttackScore = snake.AttackDice.Throw();
            int snakeDefenceScore = snake.DefenceDice.Throw();

            if (playerAttackScore - snakeDefenceScore > 0 && snake.HealthPoints > 0)
            {
                int damage = playerAttackScore - snakeDefenceScore;
                snake.HealthPoints -= damage;
                Console.SetCursorPosition(0, 1);
                Console.ForegroundColor = IconColor;
                Console.Write($"You (ATK: {AttackDice.ToString()} => {playerAttackScore}) attacked the snake (DEF: {snake.DefenceDice.ToString()} => {snakeDefenceScore}) Snake's health: {snake.HealthPoints}");
                Console.ResetColor();
            }
            if (snake.HealthPoints <= 0)
            {
                Console.SetCursorPosition(snake.Position.X, snake.Position.Y);
                Console.Write(' ');
                levelData.Elements.Remove(snake);
                Console.SetCursorPosition(0, 1);
                Console.ForegroundColor = IconColor;
                Console.Write($"You (ATK: {AttackDice.ToString()} => {playerAttackScore}) attacked the snake (DEF: {snake.DefenceDice.ToString()} => {snakeDefenceScore}), killing the snake.");
                Console.ResetColor();
                return; //ormen gör inte en motattack om den dör
            }
            if (snakeAttackScore - playerDefenceScore > 0)
            {
                int damage = snakeAttackScore - playerDefenceScore;
                HealthPoints -= damage;
                Console.SetCursorPosition(0, 2);
                Console.ForegroundColor = snake.IconColor;
                Console.Write($"The snake (ATK: {snake.AttackDice.ToString()} => {snakeAttackScore}) attacked you (DEF: {DefenceDice.ToString()} => {playerDefenceScore})");
                Console.ResetColor();
            }

        }

    }



}



