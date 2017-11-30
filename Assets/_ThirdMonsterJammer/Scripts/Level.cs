public class Level
{
    private readonly int _mapSize;
    private readonly int _maxTunelCount;
    private readonly int _minTunelLength;
    private readonly int _amountOfCrates;
    private readonly int _amountsOfMonsterA;
    private readonly int _amountsOfMonsterB;
    private readonly int _targetAmountOfDiamonds;
    private readonly int _energyOnLevel;

    private int[] _levelGeneratorParameters;

    public Level(int mapSize, int maxTunelCount, int minTunelLength, int amountOfCrates, int amountsOfMonsterA, int amountsOfMonsterB, int targetAmountOfDiamonds, int energyOnLevel)
    {
        _mapSize = mapSize;
        _maxTunelCount = maxTunelCount;
        _minTunelLength = minTunelLength;
        _amountOfCrates = amountOfCrates;
        _amountsOfMonsterA = amountsOfMonsterA;
        _amountsOfMonsterB = amountsOfMonsterB;
        _targetAmountOfDiamonds = targetAmountOfDiamonds;
        _energyOnLevel = energyOnLevel;
    }

    public int GetTargetAmountOfDiamonds()
    {
        return _targetAmountOfDiamonds;
    }

    public int EnergyOnLevel
    {
        get { return _energyOnLevel; }
    }

    public int[] GetLevelGeneratorParametersArray()
    {
        _levelGeneratorParameters = new int[6];

        _levelGeneratorParameters[0] = _mapSize;
        _levelGeneratorParameters[1] = _maxTunelCount;
        _levelGeneratorParameters[2] = _minTunelLength;
        _levelGeneratorParameters[3] = _amountOfCrates;
        _levelGeneratorParameters[4] = _amountsOfMonsterA;
        _levelGeneratorParameters[5] = _amountsOfMonsterB;

        return _levelGeneratorParameters;
    }
}
