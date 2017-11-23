public class Level
{

    private int MapSize,
        MaxTunelCount,
        MinTunelLength,
        AmountOfCrates,
        AmountsOfMonster,
        TargetAmountOfDiamonds,
        _energyOnLevel;
    
    private int[] _levelGeneratorParameters;

    public Level(int mapSize, int maxTunelCount, int minTunelLength, int amountOfCrates, int amountsOfMonster, int targetAmountOfDiamonds, int energyOnLevel)
    {
        MapSize = mapSize;
        MaxTunelCount = maxTunelCount;
        MinTunelLength = minTunelLength;
        AmountOfCrates = amountOfCrates;
        AmountsOfMonster = amountsOfMonster;
        TargetAmountOfDiamonds = targetAmountOfDiamonds;
        _energyOnLevel = energyOnLevel;
    }

    public int GetTargetAmountOfDiamonds()
    {
        return TargetAmountOfDiamonds;
    }
    

    public int EnergyOnLevel
    {
        get { return _energyOnLevel; }
    }

    public int[] GetLevelGeneratorParametersArray()
    {
        //Debug.Log("Level 1");
        _levelGeneratorParameters = new int[5];

        _levelGeneratorParameters[0] = MapSize;
        _levelGeneratorParameters[1] = MaxTunelCount;
        _levelGeneratorParameters[2] = MinTunelLength;
        _levelGeneratorParameters[3] = AmountOfCrates;
        _levelGeneratorParameters[4] = AmountsOfMonster;

        return _levelGeneratorParameters;
    }
}
