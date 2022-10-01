using System;
using UnityEngine;

[CreateAssetMenu(fileName ="New Currency Data", menuName = "Currency/CurrencyData")]
public class CurrencyData: ScriptableObject
{
    public Sprite sprite;
    public int Amount => amount;

    public Action<int> OnCurrencyUpdated;
    private int amount;

    public void AddAmount(int amountToAdd)
    {
        int newAmount = Amount + amountToAdd;

        if (newAmount >= 0)
        {
            amount = newAmount;

            if (OnCurrencyUpdated != null)
                OnCurrencyUpdated.Invoke(newAmount);
        }
        else
        {
            Debug.Log("Couldn't update amount.");
        }
    }
}
