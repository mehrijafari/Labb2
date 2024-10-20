abstract class LevelElement
{
    public LevelElement(Position position, char icon, ConsoleColor iconcolor)
    {
        this.Position = position;
        this.Icon = icon;
        this.IconColor = iconcolor;
    }
    
    public Position Position { get; set; }
    protected char Icon { get; set; }
    public ConsoleColor IconColor { get; set; }


    public void Draw()
    {
        Console.SetCursorPosition(Position.X, Position.Y);
        Console.ForegroundColor = IconColor;
        Console.Write(Icon); 
        Console.ResetColor();
    }

    public void Hide()
    {
        Console.SetCursorPosition(Position.X, Position.Y);
        Console.Write(' ');
    }

    public void ClearText()
    {
        int width = Console.WindowWidth;
        for (int i = 0; i < 3; i++)
        {
            Console.SetCursorPosition(0, i);
            Console.Write(new string(' ', width));
        }
    }
}