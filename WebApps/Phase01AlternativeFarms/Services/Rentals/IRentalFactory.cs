namespace Phase01AlternativeFarms.Services.Rentals;
public interface IRentalFactory
{
    RentalsServicesContext GetRentalServices(FarmKey farm);
}