namespace Phase02AdvancedUpgrades.Services.Catalog;
public interface ICatalogFactory
{
    CatalogServicesContext GetCatalogServices(FarmKey farm);
}