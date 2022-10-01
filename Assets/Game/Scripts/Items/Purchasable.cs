using UnityEngine;
using System;

[CreateAssetMenu(fileName = "Purchasable", menuName = "Purchasables/New Purchasable")]
public class Purchasable : ScriptableObject
{
    private int currentLevel = 1;

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

    public void Purchase()
    {
        if (requiredCurrency.Amount >= CurrentCost)
        {
            CurrentLevel += 1;
            CurrencyController.Instance.AddToCurrency(requiredCurrency, -CurrentCost);
            OnItemUpgraded?.Invoke();
        }
    }
    
    public void Reset()
    {
        CurrentLevel = 1;
    }

    public override string ToString()
    {
        return ("Item: " + name + " / Level: " + CurrentLevel + " / Current: " + CurrentCost);
    }
}

public enum UpgradeType { Incremental, Multiplier}
