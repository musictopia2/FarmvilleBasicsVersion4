namespace Phase18AlternativeFarms.ImportClasses;
internal static class FarmHelperClass
{


    extension(FarmKey farm)
    {
        public FarmKey AsMain => farm with { Slot = EnumFarmSlot.Main };
        public FarmKey AsAlternative => farm with { Slot = EnumFarmSlot.Alternative };
        public bool IsMain => farm.Slot == EnumFarmSlot.Main;
        public bool IsAlternative => farm.Slot == EnumFarmSlot.Alternative;
    }


    private static BasicList<FarmKey> GetFarmsForSlot(EnumFarmSlot slot)
    {
        BasicList<FarmKey> output = [];
        BasicList<string> themes = [FarmThemeList.Country, FarmThemeList.Tropical];
        BasicList<string> players = [PlayerList.Player1, PlayerList.Player2];
        BasicList<string> profiles = [ProfileIdList.Test];

        foreach (var player in players)
            foreach (var theme in themes)
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

        return output;
    }

    public static BasicList<FarmKey> GetAllMainFarms() => GetFarmsForSlot(EnumFarmSlot.Main);
    public static BasicList<FarmKey> GetAllAlternativeFarms() => GetFarmsForSlot(EnumFarmSlot.Alternative);
    public static BasicList<FarmKey> GetAllCompleteFarms()
    {
        var output = GetAllMainFarms();
        output.AddRange(GetAllAlternativeFarms());
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