namespace Phase01AlternativeFarms.DataAccess.Rentals;
public class RentalFactory : IRentalFactory
{
    RentalsServicesContext IRentalFactory.GetRentalServices(FarmKey farm)
    {
        return new()
        {
            RentalProfile = new RentalInstanceDatabase(farm)
        };
    }
}