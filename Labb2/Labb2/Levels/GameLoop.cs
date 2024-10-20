internal class GameLoop
{
    private LevelData levelData;
    private Player player;
    private string name;
    public GameLoop(LevelData levelData, Player player, string name)
    {
        this.levelData = levelData;
        this.player = player;
        this.name = name;
    }

    public void Run()
    {
        int moves = 0;

        while (player.HealthPoints > 0)
        {
            Console.SetCursorPosition(0, 0);
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write($"Name: {name}       HP: {player.HealthPoints}/100        Turn: {moves}");
            Console.ResetColor();
            player.Update(); 
            UpdateEnemies();
            moves++;
        }
    }

    private void UpdateEnemies()
    {
        List<Enemy> enemies = levelData.Elements.OfType<Enemy>().ToList();

        foreach (Enemy enemy in enemies)
        {
            if (enemy is Rat)
            {
                enemy.Update(enemy.Position);
            }
            else if (enemy is Snake)
            {
                enemy.Update(player.Position);
            }
            
        }
    }
}

