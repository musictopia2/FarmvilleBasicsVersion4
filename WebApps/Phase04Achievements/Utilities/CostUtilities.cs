namespace Phase04Achievements.Utilities;

public static class CostUtilities
{
    public static Dictionary<string, int> GetCoinOnlyDictionary(int value)
    {
        Dictionary<string, int> output = [];
        output[CurrencyKeys.Coin] = value;
        return output;
    }
}