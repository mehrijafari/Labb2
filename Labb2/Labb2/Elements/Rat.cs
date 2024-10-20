internal class Rat : Enemy
{

    public Rat(Position position, LevelData levelData) : base(position, 'r', ConsoleColor.Red, levelData)
    {
        this.HealthPoints = 10;
        this.Name = "Rat";
        this.AttackDice = new Dice(1, 6, 3);
        this.DefenceDice = new Dice(1, 6, 1);

    }

    
    public override void Update(Position position) 
    {
        Random random = new Random();
        int randomDirection = random.Next(1, 5);
        var newPosition = new Position(position);

        // 1 = höger
        // 2 = vänster
        // 3 = upp
        // 4 = ner
        switch (randomDirection)
        {
            case 1:
                newPosition.X += 1;
                break;
            case 2:
                newPosition.X -= 1;
                break;
            case 3:
                newPosition.Y -= 1;
                break;
            case 4:
                newPosition.Y += 1;
                break;
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

            var playerInList = levelData.Elements.First(e => e is Player) as Player;

            bool playerInRange = Position.InRange(Position, playerInList.Position, 5);



            Console.SetCursorPosition(position.X, position.Y);
            Console.Write(' ');
            Position = newPosition;


            if (playerInRange)
            {
                Draw();
            }
            else
            {
                Hide();
            }
        }
    }

  

}
