
namespace Phase03RandomChests.Services.TimedBoosts;
public class TimedBoostManager
{
    private TimedBoostProfileModel _profile = null!;
    private ITimedBoostProfile _profileStore = null!;
    public event Action? Tick;             // “countdown changed” (UI refresh)
    private void NotifyTick() => Tick?.Invoke();
    public async Task SetTimedBoostStyleContextAsync(TimedBoostServicesContext context)
    {
        _profile = await context.TimedBoostProfile.LoadAsync();
        _profileStore = context.TimedBoostProfile;
        CleanupExpired();
       
        await SaveAsync();
        // reset cache when loading
    }

    public async Task GrantCreditAsync(RandomChestResultModel item)
    {
        if (item.Duration.HasValue == false)
        {
            throw new CustomBasicException("Must have duration for granting credit");
        }
        var existing = _profile.Credits.SingleOrDefault(x => x.BoostKey == item.TargetName && x.Duration == item.Duration);
        if (existing is null)
        {
            _profile.Credits.Add(new TimedBoostCredit
            {
                BoostKey = item.TargetName,
                Duration = item.Duration.Value,
                Quantity = item.Quantity,
                ReduceBy = item.ReduceBy,
                OutputAugmentationKey = item.OutputAugmentationKey
            });
        }
        else
        {
            existing.Quantity += item.Quantity;
        }
        await SaveAsync();
    }


    public async Task GrantCreditAsync(CatalogOfferModel item)
    {   
        if (item.Duration.HasValue == false)
        {
            throw new CustomBasicException("Must have duration for granting credit");
        }
        var existing = _profile.Credits.SingleOrDefault(x => x.BoostKey == item.TargetName && x.Duration == item.Duration);

        if (existing is null)
        {
            _profile.Credits.Add(new TimedBoostCredit
            {
                BoostKey = item.TargetName,
                Duration = item.Duration.Value,
                Quantity = 1,
                ReduceBy = item.ReduceBy,
                OutputAugmentationKey = item.OutputAugmentationKey
            });
        }
        else
        {
            existing.Quantity += 1;
        }
        await SaveAsync();
    }
    public BasicList<ActiveTimedBoost> GetActiveBoosts => _profile.Active.ToBasicList();
    public BasicList<TimedBoostCredit> GetBoosts()
    {
        return _profile.Credits.Where(x => x.Quantity > 0).ToBasicList();
    }
    public TimeSpan GetReducedTime(string key)
    {
        var active = _profile.Active.SingleOrDefault(a => a.BoostKey == key);
        if (active is null)
        {
            return TimeSpan.Zero;
        }
        if (active.ReduceBy is null)
        {
            return TimeSpan.Zero;
        }
        return active.ReduceBy.Value;
    }

    public string? GetActiveOutputAugmentationKeyForItem(string itemName)
    {
        CleanupExpired();
        var active = _profile.Active
            .SingleOrDefault(a =>
                a.BoostKey == itemName &&
                string.IsNullOrWhiteSpace(a.OutputAugmentationKey) == false);
        return active?.OutputAugmentationKey;
    }

    public bool CanActivateBoost(TimedBoostCredit credit)
    {
        var active = _profile.Active.SingleOrDefault(a => a.BoostKey == credit.BoostKey);
        if (active is null)
        {
            return true; //none like this so okay (for now).  once i have 2 different types, will require rethinking.
        }
        if (active.ReduceBy == credit.ReduceBy)
        {
            return true; //for now, okay.  later more rules.
        }
        return false; 
    }

    // Activate later from credits
    // Rule: if already active, EXTEND the current end time.
    public async Task ActiveBoostAsync(TimedBoostCredit credit)
    {
        if (CanActivateBoost(credit) == false)
        {
            throw new CustomBasicException("Unable to activate boost.   should had called CanActivateBoost");
        }
        credit.Quantity--;
        if (credit.Quantity == 0)
        {
            _profile.Credits.RemoveSpecificItem(credit);
        }

        CleanupExpired();

        var now = DateTime.Now;
        var active = _profile.Active.SingleOrDefault(a => a.BoostKey == credit.BoostKey);

        if (active is null)
        {
            _profile.Active.Add(new ActiveTimedBoost
            {
                BoostKey = credit.BoostKey,
                StartedAt = now,
                EndsAt = now.Add(credit.Duration),
                ReduceBy = credit.ReduceBy,
                OutputAugmentationKey = credit.OutputAugmentationKey
            });
        }
        else
        {
            // extend
            var baseTime = active.EndsAt > now ? active.EndsAt : now;
            active.EndsAt = baseTime.Add(credit.Duration);
        }
        await SaveAsync(); //no problem to save here.
    }
    //use this information (so if its null, then normal screen).
    public TimeSpan? GetUnlimitedSpeedSeedTimeLeft()
    {
        var active = _profile.Active
            .SingleOrDefault(x => x.BoostKey == BoostKeys.UnlimitedSpeedSeed);

        if (active is null)
        {
            return null;
        }
        var remaining = active.EndsAt - DateTime.Now;
        if (remaining <= TimeSpan.Zero)
        {
            return null; // already expired
        }
        return remaining;
    }
    public bool HasNoSuppliesNeededForWorksites()
    {
        var active = _profile.Active
           .SingleOrDefault(x => x.BoostKey == BoostKeys.WorksiteNoSupplies);
        if (active is null)
        {
            return false;
        }
        var remaining = active.EndsAt - DateTime.Now;
        if (remaining <= TimeSpan.Zero)
        {
            return false; //already expired
        }
        return true;
    }
    private bool CleanupExpired()
    {
        var now = DateTime.Now;

        int before = _profile.Active.Count;
        _profile.Active.RemoveAllAndObtain(a => a.EndsAt <= now);

        return _profile.Active.Count != before;
    }

    private async Task SaveAsync()
    {
        // Whatever your profile wrapper uses. If your interface differs,
        // change this to context.TimedBoostProfile.SaveAsync(_profile).
        await _profileStore.SaveAsync(_profile);
    }
    
    public async Task UpdateTickAsync()
    {
        if (_profile.Active.Count == 0)
        {
            return; //you have none active.
        }

        


        bool changed = CleanupExpired();
        if (changed)
        {
            await SaveAsync();
            //NotifyChanged();
            // if everything expired, ping tick once so any countdown UI clears
            if (_profile.Active.Count == 0)
            {
                NotifyTick();
                return;
            }
        }
        if (_profile.Active.Count > 0)
        {
            // still active => refresh countdown
            NotifyTick();
        }

        
    }
}