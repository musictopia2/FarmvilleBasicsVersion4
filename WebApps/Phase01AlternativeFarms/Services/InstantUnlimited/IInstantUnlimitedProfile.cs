namespace Phase01AlternativeFarms.Services.InstantUnlimited;
public interface IInstantUnlimitedProfile
{
    Task<BasicList<UnlockModel>> LoadAsync();
    Task SaveAsync(BasicList<UnlockModel> list);
}