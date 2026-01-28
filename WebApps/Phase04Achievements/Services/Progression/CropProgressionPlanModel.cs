namespace Phase04Achievements.Services.Progression;
public class CropProgressionPlanModel
{
    public BasicList<int> SlotLevelRequired { get; set; } = [];
    public BasicList<ItemUnlockRule> UnlockRules { get; set; } = [];
}