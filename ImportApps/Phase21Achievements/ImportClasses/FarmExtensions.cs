namespace Phase21Achievements.ImportClasses;
public static class FarmExtensions
{
    extension(FarmKey farm)
    {
        public FarmKey AsMain => farm with { Slot = EnumFarmSlot.Main };
        public FarmKey AsCoin => farm with { Slot = EnumFarmSlot.Coin };
        public FarmKey AsCooperative => farm with { Slot = EnumFarmSlot.Cooperative };
        public bool IsMain => farm.Slot == EnumFarmSlot.Main;
        public bool IsCoin => farm.Slot == EnumFarmSlot.Coin;
        public bool IsCooperative => farm.Slot == EnumFarmSlot.Cooperative;
        public bool IsBaseline => farm.IsMain || farm.IsCooperative;
    }
    extension<T>(IFarmDocumentFactory<T>)
         where T : IFarmDocumentModel, IFarmDocumentFactory<T>
    {
        public static BasicList<T> PopulateEmptyForCoins()
        {
            BasicList<T> output = [];
            foreach (var farm in FarmHelperClass.GetAllCoinFarms())
            {
                output.Add(T.CreateEmpty(farm));
            }
            return output;

        }
    }

}