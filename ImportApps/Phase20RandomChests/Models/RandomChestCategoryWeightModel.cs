namespace Phase20RandomChests.Models;
//this is my starting point.
public class RandomChestCategoryWeightModel
{
    //this is just for 2 areas at least.  one is for main category.  the other is if it chooses the proper one and needs to dial further.
    public string Key { get; set; } = ""; //this is where its coin, power gloves, etc.
    public int Weight { get; set; } //higher weight means lean more towards that. 
    public int LevelRequired { get; set; } = 1; //this is the level requirement
}