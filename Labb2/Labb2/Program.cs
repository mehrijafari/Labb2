
LevelData level = new LevelData();
level.Load("Level1.txt");

Player player = new Player(level.PlayerStarterPosition, level);

level.Elements.Add(player);

Console.WriteLine("Enter your name:");
string name = Console.ReadLine();
Console.Clear();

GameLoop gameloop = new GameLoop(level, player, name);
gameloop.Run();

Console.Clear();
Console.WriteLine("Game over");


