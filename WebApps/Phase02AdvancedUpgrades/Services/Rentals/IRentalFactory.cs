namespace Phase02AdvancedUpgrades.Services.Rentals;
public interface IRentalFactory
{
    RentalsServicesContext GetRentalServices(FarmKey farm);
}