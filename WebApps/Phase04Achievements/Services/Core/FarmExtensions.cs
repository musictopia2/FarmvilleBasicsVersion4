using Phase04Achievements.Components.Custom; //not common enough.

namespace Phase04Achievements.Services.Core;

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
    extension(FarmComponentBase farm)
    {
        public FarmKey AsMain => farm.Key with { Slot = EnumFarmSlot.Main };
        public FarmKey AsCoin => farm.Key with { Slot = EnumFarmSlot.Coin };
        public FarmKey AsCooperative => farm.Key with { Slot = EnumFarmSlot.Cooperative };
        public bool IsMain => farm.Key.Slot == EnumFarmSlot.Main;
        public bool IsCoin => farm.Key.Slot == EnumFarmSlot.Coin;
        public bool IsCooperative => farm.Key.Slot == EnumFarmSlot.Cooperative;
        public bool IsBaseline => farm.Key.IsMain || farm.Key.IsCooperative;
    }
    extension(NavigationManager nav)
    {
        public void NavigateTo(FarmKey key)
        {
            nav.NavigateTo($"/farm/{key.Theme}/{key.PlayerName}/{key.ProfileId}/{key.Slot}", forceLoad: true);
        }
    }

}