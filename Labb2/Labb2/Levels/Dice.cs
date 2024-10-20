internal class Dice
{
    public int NumberOfDice { get; set; }
    public int SidesPerDice { get; set; }
    public int Modifier { get; set; }

    public Dice(int numberOfDice, int sidesPerDice, int modifier)
    {
        this.NumberOfDice = numberOfDice;
        this.SidesPerDice = sidesPerDice;
        this.Modifier = modifier;
    }

    public int Throw()
    {
        Random random = new Random();

        int result = 0;


        for (int i = 0; i < NumberOfDice; i++)
        {
            result += random.Next(1, SidesPerDice + 1);
        }

        result += Modifier;
        
        return result;
    }


    public override string ToString()
    {
        return $"{NumberOfDice}d{SidesPerDice}+{Modifier}";
    }

}

