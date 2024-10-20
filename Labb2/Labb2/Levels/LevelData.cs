internal class LevelData
{

    private List<LevelElement> _elements;

	public List<LevelElement> Elements
	{
		get { return _elements; }
	}

	public Position PlayerStarterPosition { get; private set; }

	public void Load(string fileName)
	{
		int x = 0;
		int y = 3;
		
		_elements = new List<LevelElement>();

		using (StreamReader reader = new StreamReader("Level1.txt"))
		{
			string line;

			while ((line = reader.ReadLine()) != null)
			{

				x = 0;   

				foreach (char c in line)
				{
					switch (c)
					{
						case '#': _elements.Add(new Wall(new Position(x, y)));
							break;
						case 'r': _elements.Add(new Rat(new Position(x, y), this));
							break;
						case 's': _elements.Add(new Snake(new Position(x, y), this));
							break;
						case '@':
							PlayerStarterPosition = new Position(x, y);
							break;

					}
					x++;
				}
				y++;
			}
		}
    }

	public LevelElement GetLevelElementAt(Position position) 
	{
		
		foreach (LevelElement element in Elements)
		{
			if (element.Position.Equals(position))
			{
                return element;
            }
			
		}

		return null;

	}

	public bool CanMoveTo(Position newPosition) 
	{
		LevelElement elementAtNewPosition = GetLevelElementAt(newPosition);

		if (elementAtNewPosition == null)
		{
			return true;
		}

		if (elementAtNewPosition is Wall)
		{
			return false;
		}

		if (elementAtNewPosition is Enemy)
		{
			return true;
		}

		if (elementAtNewPosition is Player)
		{
			return true;
		}

		return false;
	}
}

