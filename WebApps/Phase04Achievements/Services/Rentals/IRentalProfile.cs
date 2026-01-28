namespace Phase04Achievements.Services.Rentals;
public interface IRentalProfile
{
    Task<BasicList<RentalInstanceModel>> LoadAsync();
    Task SaveAsync(BasicList<RentalInstanceModel> rentals);
}