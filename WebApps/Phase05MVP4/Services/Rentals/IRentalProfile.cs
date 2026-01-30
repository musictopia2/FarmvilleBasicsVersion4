namespace Phase05MVP4.Services.Rentals;
public interface IRentalProfile
{
    Task<BasicList<RentalInstanceModel>> LoadAsync();
    Task SaveAsync(BasicList<RentalInstanceModel> rentals);
}