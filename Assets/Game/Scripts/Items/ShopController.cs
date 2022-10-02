using UnityEngine;

public class ShopController : MSingleton<ShopController>
{
    public Purchasable insuranceItem;
    public Purchasable slotItem;

    private void Start()
    {
        UpdateInsuranceSlots();
    }

    public void UseInsurance()
    {
        if (insuranceItem.inventoryItem.Amount <= 0)
        {
            WarningMessage.Instance.Show("No insurance left!");
            return;
        }

        insuranceItem.inventoryItem.AddAmount(-1);
        GameManager.Instance.RecoverGame();
    }

    public void UpdateInsuranceSlots()
    {
        slotItem.inventoryItem.Amount = slotItem.CurrentLevel;
        insuranceItem.inventoryItem.maximumAmount = slotItem.inventoryItem.Amount;

        if (insuranceItem.inventoryItem.Amount > insuranceItem.inventoryItem.maximumAmount)
            insuranceItem.inventoryItem.Amount = insuranceItem.inventoryItem.maximumAmount;
    }

    public bool BuyItem(Purchasable item)
    {
        if (item.inventoryItem.Amount >= item.inventoryItem.maximumAmount)
        {
            WarningMessage.Instance.Show("Reached max item count.");
            return false;
        }

        bool purchaseResult = item.Purchase();
        if (purchaseResult)
        {
            item.inventoryItem.AddAmount(1);
            WarningMessage.Instance.Show("Purchase successful!");
            UpdateInsuranceSlots();
        }
        else
            WarningMessage.Instance.Show("You don't have enough coins.");

        return purchaseResult;
    }
}
