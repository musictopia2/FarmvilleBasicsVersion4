namespace Phase19AdvancedUpgrades.ImportClasses;
internal static class FarmHelperClass
{


    public static BasicList<TDocument> CreateDocumentsForCoins<TDocument>(Func<FarmKey, TDocument> factory)
        where TDocument : IFarmDocumentModel
        {
            BasicList<TDocument> output = [];
            foreach (var farm in GetAllCoinFarms())
            {
                output.Add(factory(farm));
            }

            return output;
        }

    private static BasicList<FarmKey> GetFarmsForSlot(EnumFarmSlot slot)
    {
        BasicList<FarmKey> output = [];
        BasicList<string> themes = [FarmThemeList.Country, FarmThemeList.Tropical];
        BasicList<string> players = [PlayerList.Player1, PlayerList.Player2];
        BasicList<string> profiles = [ProfileIdList.Test];

        foreach (var player in players)
        {
            foreach (var theme in themes)
            {
                foreach (var profile in profiles)
                {
                    output.Add(new FarmKey
                    {
                        PlayerName = player,
                        Theme = theme,
                        ProfileId = profile,
                        Slot = slot
                    });
                }
            }
        }

        return output;
    }

    public static BasicList<FarmKey> GetAllMainFarms() => GetFarmsForSlot(EnumFarmSlot.Main);
    public static BasicList<FarmKey> GetAllCoinFarms() => GetFarmsForSlot(EnumFarmSlot.Coin);
    public static BasicList<FarmKey> GetAllCooperativeFarms() => GetFarmsForSlot(EnumFarmSlot.Cooperative);
    // Baseline = farms that get the full “normal” world setup (Main + Cooperative)
    public static BasicList<FarmKey> GetAllBaselineFarms()
    {
        var output = GetAllMainFarms();
        output.AddRange(GetAllCooperativeFarms());
        return output;
    }
    // Complete = all farms that exist (Baseline + Coin)
    public static BasicList<FarmKey> GetAllCompleteFarms()
    {
        var output = GetAllBaselineFarms();
        output.AddRange(GetAllCoinFarms());
        return output;
    }

    public static Dictionary<string, int> GetCoinOnlyDictionary(int value)
    {
        Dictionary<string, int> output = [];
        output[CurrencyKeys.Coin] = value;
        return output;
    }
    public static Dictionary<string, int> GetFreeCosts => [];
    public static BasicList<string> GetOnlyItem(string item)
    {
        BasicList<string> output = [];
        output.Add(item);
        return output;
    }
}