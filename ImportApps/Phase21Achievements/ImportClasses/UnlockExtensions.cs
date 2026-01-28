namespace Phase21Achievements.DataAccess;
public static class UnlockExtensions
{
    extension(BasicList<UnlockModel> list)
    {
        public void UnlockSeveralItems(BasicList<string> payLoad)
        {
            foreach (var item in payLoad)
            {
                list.Add(new()
                {
                    Name = item,
                    Unlocked = true
                });
            }
        }
    }
}