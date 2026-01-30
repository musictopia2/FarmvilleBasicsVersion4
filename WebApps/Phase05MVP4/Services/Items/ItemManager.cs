namespace Phase05MVP4.Services.Items;
public class ItemManager
{
    private BasicList<ItemPlanModel> _items = [];
    public async Task SetItemStyleContextAsync(ItemServicesContext context, FarmKey farm)
    {
        _items = await context.ItemPlanProvider.GetPlanAsync(farm);
    }
    public BasicList<ItemPlanModel> GetEligibleItems(int level) => _items.Where(x => x.MinLevel <= level).ToBasicList();
    public EnumItemCategory GetItemCategory(string item) => _items.Single(x => x.ItemName == item).Category;
    public string GetSource(string item) => _items.Single(x => x.ItemName == item).Source;
    public BasicList<ItemPlanModel> GetAllItems => _items.ToBasicList();
}