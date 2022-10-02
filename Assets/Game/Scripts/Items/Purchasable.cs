using UnityEngine;
using System;

[CreateAssetMenu(fileName = "Purchasable", menuName = "Purchasables/New Purchasable")]
public class Purchasable : ScriptableObject
{
    [SerializeField] private int currentLevel = 1;
    [Header("Item Specs")]
    public CurrencyData inventoryItem;
    public string itemName;
    public Sprite icon;
    [TextArea(4, 12)] public string description;
    public int maxCount;

    [Header("Cost")]
    public CurrencyData requiredCurrency;
    public UpgradeType upgradeType;
    public int baseCost = 0;
    public float costMultiplier = 1f;

    public Action OnItemUpgraded;

    public int CurrentLevel { get => currentLevel; set => currentLevel = value; }

    public int CurrentCost
    {
        get
        {
            switch (upgradeType)
            {
                case UpgradeType.Incremental:
                    return (int)(baseCost + (CurrentLevel - 1) * costMultiplier);
                case UpgradeType.Multiplier:
                    return (int)(baseCost * Mathf.Pow(costMultiplier, CurrentLevel - 1));
                default:
                    return -1;
            }
        }
    }

    public bool Purchase()
    {
        if (requiredCurrency.Amount >= CurrentCost)
        {
            CurrentLevel += 1;
            CurrencyController.Instance.AddToCurrency(requiredCurrency, -CurrentCost);
            OnItemUpgraded?.Invoke();
            return true;
        }
        return false;
    }
    
    public void Reset()
    {
        CurrentLevel = 1;
    }

    public override string ToString()
    {
        return ("Item: " + itemName + " / Level: " + CurrentLevel + " / Current: " + CurrentCost);
    }
}

public enum UpgradeType { Incremental, Multiplier}
