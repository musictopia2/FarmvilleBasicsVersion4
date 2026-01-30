namespace Phase05MVP4.Services.Rentals;
public interface IRentalFactory
{
    RentalsServicesContext GetRentalServices(FarmKey farm);
}