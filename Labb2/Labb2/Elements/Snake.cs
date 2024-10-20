internal class Snake : Enemy
{

    
    public Snake(Position position, LevelData levelData) : base (position, 's', ConsoleColor.Green, levelData)
    {
        this.HealthPoints = 25;
        this.Name = "Snake";
        this.AttackDice = new Dice(3, 4, 2);
        this.DefenceDice = new Dice(1, 8, 5);
    }
    public override void Update(Position playerPosition) 
    {
        if (IsPlayerNearby(playerPosition, 2))
        {
            MoveAwayFromPlayer(playerPosition);
        }
    }

    private bool IsPlayerNearby(Position playerPosition, int distance)
    {
        return Position.CalculateDistance(Position, playerPosition) <= distance;
    }

    private void MoveAwayFromPlayer(Position playerPosition)
    {
        var newPosition = new Position(Position.X, Position.Y);

        if (playerPosition.X > Position.X)
        {
            newPosition.X -= 1;
        }
        else if (playerPosition.X < Position.X)
        {
            newPosition.X += 1;
        }
        else if (playerPosition.Y < Position.Y)
        {
            newPosition.Y += 1;
        }
        else if (playerPosition.Y > Position.Y)
        {
            newPosition.Y -= 1;
        }

        if (levelData.CanMoveTo(newPosition))
        {
            LevelElement elementAtNewPosition = levelData.GetLevelElementAt(newPosition);
            if (elementAtNewPosition is Snake)
            {
                return;
            }
            else if (elementAtNewPosition is Rat)
            {
                return;
            }
            else if (elementAtNewPosition is Player player)
            {
                Attack(player);
                return;
            }

            Console.SetCursorPosition(Position.X, Position.Y);
            Console.Write(' ');
            Position = newPosition;
            Draw();
        }
       
    }

}

