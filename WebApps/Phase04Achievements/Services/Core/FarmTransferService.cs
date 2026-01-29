namespace Phase04Achievements.Services.Core;
public class FarmTransferService(GameRegistry gameRegistry)
{

    public bool CanTransferInventory(FarmKey farm, ItemAmount item)
    {
        if (farm.IsCoin)
        {
            return false;
        }
        FarmKey other;
        if (farm.IsMain)
        {
            other = farm.AsCooperative;
        }
        else if (farm.IsCooperative)
        {
            other = farm.AsMain;
        }
        else
        {
            return false;
        }
        var container = gameRegistry.GetFarm(other);
        return container.InventoryManager.CanAdd(item);
    }

    public void TransferInventory(FarmKey farm, ItemAmount item, InventoryManager original)
    {
        if (CanTransferInventory(farm, item) == false)
        {
            throw new CustomBasicException("Unable to transfer inventory.  Should had called CanTransferInventory");
        }
        FarmKey other;
        if (farm.IsMain)
        {
            other = farm.AsCooperative;
        }
        else if (farm.IsCooperative)
        {
            other = farm.AsMain;
        }
        else
        {
            throw new CustomBasicException("Wrong farm");
        }
        var container = gameRegistry.GetFarm(other);
        container.InventoryManager.Add(item);
        original.Consume(item);
    }
    public async Task AddCoinFromScenarioCompletionAsync(FarmKey coinFarm, int amount, IToast toast)
    {
        if (!coinFarm.IsCoin)
        {
            throw new InvalidOperationException("Coin can only be transferred from coin farms.");
        }
        FarmKey other = coinFarm.AsMain;
        //this will not actually remove inventory because never put into inventory (if i do put into inventory, then would remove then).
        if (amount <= 0)
        {
            return;
        }
        var temps = gameRegistry.GetFarm(other);
        int firsts = await temps.AchievementManager.ScenarioCompletedAsync();
        int earned = temps.AchievementManager.CoinsEarnedFromAchievement(amount + firsts);
        bool neededToast = false;
        if (earned > 0)
        {
            toast.ShowSuccessToast($"You earned at least one achievement for earnings coins.   You earned at least {earned} coins.");
            neededToast = true;
        }
        if (firsts > 0)
        {
            toast.ShowSuccessToast($"You earned {firsts} from completing scenarios");
            neededToast = true;
        }
        if (neededToast)
        {
            await Task.Delay(2000); //so you can see the toasts for 2 seconds.
        }
        temps.InventoryManager.AddCoin(amount);
    }
}