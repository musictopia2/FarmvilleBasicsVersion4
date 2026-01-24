namespace Phase01AlternativeFarms.Components.Custom;
public partial class CoinIndicator(NavigationManager nav)
{
    private int CoinCount => InventoryManager.Get(CurrencyKeys.Coin);
    private string CoinCountDisplay => CoinCount.ToString("N0");

    private void AttemptEarnCoin()
    {
        if (this.IsCooperative)
        {
            return;  //only main can do this.
        }
        FarmKey farm = this.AsCoin;
        nav.NavigateTo(farm);
    }

}