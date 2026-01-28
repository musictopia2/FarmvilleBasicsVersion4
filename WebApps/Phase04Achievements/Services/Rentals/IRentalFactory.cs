namespace Phase04Achievements.Services.Rentals;
public interface IRentalFactory
{
    RentalsServicesContext GetRentalServices(FarmKey farm);
}