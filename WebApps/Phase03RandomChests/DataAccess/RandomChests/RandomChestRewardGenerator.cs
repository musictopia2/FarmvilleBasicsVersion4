namespace Phase03RandomChests.DataAccess.RandomChests;
public class RandomChestRewardGenerator(RandomChestPlanDatabase db, ProgressionManager progression) : IRandomChestGenerator
{
    private RandomChestPlanDocument _document = null!;
    private int _currentLevel;
    async Task<RandomChestResultModel> IRandomChestGenerator.GenerateRewardAsync()
    {
        _document ??= await db.GetPlanAsync();
        _currentLevel = progression.CurrentLevel;

        RandomChestCategoryWeightModel category = PickCategory();
        
        var eligibleItems = _document.ItemWeights
            .Where(x => x.Category == category.Key && x.LeveRequired <= _currentLevel)
            .ToBasicList();

        // Category-only reward (no item table rows)
        if (eligibleItems.Count == 0)
        {
            var quantityRule = FindQuantityRule(category.Key);
            int qty = quantityRule is null ? 1 : RollQuantity(quantityRule);
            return CreateResultFromCategory(category, qty);
        }

        // Category has item rows
        var chosenItem = PickWeighted(eligibleItems, x => x.ItemWeight);
        if (string.IsNullOrWhiteSpace(chosenItem.TargetName))
        {
            throw new CustomBasicException("Random chest item had blank TargetName");
        }

        var itemQuantityRule = FindQuantityRule(chosenItem.TargetName);
        int quantity = itemQuantityRule is null ? 1 : RollQuantity(itemQuantityRule);

        return CreateResultFromItem(chosenItem, quantity);
    }

    private RandomChestQuantityModel? FindQuantityRule(string targetName) =>
        _document.QuantityRules.SingleOrDefault(x => x.TargetName == targetName);

    private static int RollQuantity(RandomChestQuantityModel rule) =>
        rs1.Randoms.GetRandomNumber(rule.MaximumQuantity, rule.MinimumQuantity);

    private static RandomChestResultModel CreateResultFromCategory(RandomChestCategoryWeightModel category, int quantity) =>
        new()
        {
            Duration = category.Duration,
            TargetName = category.Key,
            Quantity = quantity
        };

    private static RandomChestResultModel CreateResultFromItem(RandomChestItemWeightModel item, int quantity) =>
        new()
        {
            TargetName = item.TargetName,
            Quantity = quantity,
            Duration = item.Duration,
            OutputAugmentationKey = item.OutputAugmentationKey,
            ReducedBy = item.ReducedBy
        };
    
    private RandomChestCategoryWeightModel PickCategory()
    {
        var list = _document.CategoryWeights.Where(x => x.LevelRequired <= _currentLevel).ToBasicList();
        return PickWeighted(list, x => x.Weight);
    }




    private static T PickWeighted<T>(
     BasicList<T> list,
     Func<T, int> weightSelector)
    {
        if (list.Count == 0)
        {
            throw new CustomBasicException("Cannot pick weighted item from empty list.");
        }

        int totalWeight = list.Sum(x => Math.Max(0, weightSelector(x)));
        if (totalWeight <= 0)
        {
            throw new CustomBasicException("Total weight must be greater than zero.");
        }
        int roll = rs1.Randoms.GetRandomNumber(totalWeight, 1);

        int running = 0;
        foreach (var item in list)
        {
            int weight = Math.Max(0, weightSelector(item));
            running += weight;
            if (roll <= running)
            {
                return item;
            }
        }

        // Should never happen, but keeps compiler happy
        return list.Last();
    }


}