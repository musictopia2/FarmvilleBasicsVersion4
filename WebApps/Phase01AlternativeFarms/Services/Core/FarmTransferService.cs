namespace Phase01AlternativeFarms.Services.Core;
public class FarmTransferService(GameRegistry gameRegistry)
{
    public void AddCoinFromScenarioCompletion(FarmKey coinFarm, int amount)
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
        temps.InventoryManager.AddCoin(amount);
    }
}