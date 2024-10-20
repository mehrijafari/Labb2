

public struct Position
{
    public int X { get; set; }
    public int Y { get; set; }


    public Position(Position position) : this(position.X, position.Y) { }

    public Position(int x, int y)
    {
        this.X = x;
        this.Y = y;
    }


    public double CalculateDistance(Position p1, Position p2)
    {
        int dx = p2.X - p1.X;
        int dy = p2.Y - p1.Y;

        return Math.Sqrt(dx * dx + dy * dy);
    }

    public bool InRange(Position playerPosition,Position objectPosition, int distance)
    {
        double rangeDistance = CalculateDistance(playerPosition, objectPosition);
        return rangeDistance <= distance;
        
    }
}