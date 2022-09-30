using System;
using UnityEngine;

[CreateAssetMenu(fileName ="New Currency Data", menuName = "Currency/CurrencyData")]
public class CurrencyData: ScriptableObject
{
    public CurrencyType type;
    public Sprite sprite;
    public int Amount;

    public Action<int> OnCurrencyUpdated;

    public void AddAmount(int amountToAdd)
    {
        int newAmount = Amount + amountToAdd;

        if (newAmount >= 0)
        {
            Amount = newAmount;

            if (OnCurrencyUpdated != null)
                OnCurrencyUpdated.Invoke(newAmount);
        }
        else
        {
            Debug.Log("Couldn't update amount.");
        }
    }
}

public enum CurrencyType
{
    Money,
    Gold,
    Diamond,
}
