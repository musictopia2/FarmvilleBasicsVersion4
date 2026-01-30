namespace Phase05MVP4.DataAccess.Store;
public class StoreUiStateDocument : IFarmDocumentModel
{
    required public FarmKey Farm { get; set; }
    public EnumCatalogCategory LastCategory { get; set; } = EnumCatalogCategory.Tree;
}