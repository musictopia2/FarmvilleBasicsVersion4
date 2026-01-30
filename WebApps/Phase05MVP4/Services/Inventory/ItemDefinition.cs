namespace Phase05MVP4.Services.Inventory;
public readonly record struct ItemDefinition(string ItemName, EnumInventoryStorageCategory Storage, EnumInventoryItemCategory ItemCategory);