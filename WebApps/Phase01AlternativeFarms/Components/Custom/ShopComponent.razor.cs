namespace Phase01AlternativeFarms.Components.Custom;
public partial class ShopComponent(IToast toast, IMessageBox message)
{
    private async Task OnActiveKeyChanged(string? key)
    {
        if (string.IsNullOrWhiteSpace(key))
        {
            return;
        }

        if (Enum.TryParse<EnumCatalogCategory>(key, out var category))
        {
            await StoreManager.ChoseNewCategoryAsync(category);
            Reload();
        }
    }
    private BasicList<StoreItemRowModel> _list = [];
    private void Reload()
    {
        _list = StoreManager.GetStoreItems();
    }


    private readonly (EnumCatalogCategory Category, string Text)[] _tabs =
    [
        (EnumCatalogCategory.Tree,     "Trees"),
        (EnumCatalogCategory.Animal,   "Animals"),
        (EnumCatalogCategory.Workshop, "Workshops"),
        (EnumCatalogCategory.Worker,   "Workers"),
        (EnumCatalogCategory.Worksite, "Worksites"),
        (EnumCatalogCategory.InstantUnlimited, "Instant Unlimited"),
        (EnumCatalogCategory.TimedBoosts, "Timed Boosts"),
        (EnumCatalogCategory.Misc, "Misc Items")
    ];

    private StoreItemRowModel? _currentItem;
    private bool _showConfirmation;
    private async Task ConfirmedAsync()
    {
        if (_currentItem is null)
        {
            return;
        }
        _showConfirmation = false;
        await StoreManager.AcquireAsync(_currentItem);
        Reload();
        toast.ShowSuccessToast($"Successfully acquired {_currentItem.TargetName.GetWords}");
        _currentItem = null;
    }

    private async Task ShowAugmentationDetailsAsync(StoreItemRowModel row)
    {
        if (string.IsNullOrWhiteSpace(row.OutputAugmentationKey))
        {
            return;
        }

        string description = Farm!.OutputAugmentationManager.GetDescription(row.OutputAugmentationKey);
        await message.ShowMessageAsync(description);
    }
    private void CancelPurchase()
    {
        _showConfirmation = false;
    }
    private void OnRowClicked(StoreItemRowModel row)
    {
        // You said you need the outer div for click; keep it here.
        // Example: if locked/maxed you could ignore, else attempt purchase.
        if (row.IsLocked || row.IsMaxedOut)
        {
            return;
        }
        if (StoreManager.CanAcquire(row) == false)
        {
            toast.ShowUserErrorToast("Failed to acquire."); //not always coins.
            return;
        }
        _currentItem = row;
        _showConfirmation = true;
    }

    private static string CardStateClass(StoreItemRowModel row)
    {
        if (row.IsLocked)
        {
            return "is-locked";
        }

        if (row.IsMaxedOut)
        {
            return "is-maxed";
        }

        return "is-ready";
    }

    private static string CostImageUrl(string currencyKeyOrItemKey)
    {
        // Adjust to your actual currency/item icon rules.
        // Example assumes icons live at root with .png
        return $"/{currencyKeyOrItemKey}.png";
    }

    

    protected override void OnInitialized()
    {
        Reload();
        base.OnInitialized();
    }
}