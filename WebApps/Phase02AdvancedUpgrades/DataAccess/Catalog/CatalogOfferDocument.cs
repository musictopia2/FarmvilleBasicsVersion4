namespace Phase02AdvancedUpgrades.DataAccess.Catalog;
public class CatalogOfferDocument : IFarmDocumentModel
{
    public required FarmKey Farm { get; init; }
    public required BasicList<CatalogOfferModel> Offers { get; init; } = [];
}