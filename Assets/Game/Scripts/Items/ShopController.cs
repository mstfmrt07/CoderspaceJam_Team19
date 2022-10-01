using UnityEngine;

public class ShopController : MSingleton<ShopController>
{
    public Purchasable insuranceItem;
    public Purchasable slotItem;

    public void UseInsurance()
    {
        if (insuranceItem.inventoryItem.Amount <= 0)
        {
            Debug.Log("No item left!");
            return;
        }

        insuranceItem.inventoryItem.AddAmount(-1);
        GameManager.Instance.RecoverGame();
    }

    public void UseSlot()
    {
        insuranceItem.maxCount = slotItem.inventoryItem.Amount;
    }

    public bool BuyItem(Purchasable item)
    {
        if (item.inventoryItem.Amount >= item.maxCount)
        {
            Debug.Log("Reached max item count.");
            return false;
        }

        bool purchaseResult = item.Purchase();
        if (purchaseResult)
            item.inventoryItem.AddAmount(1);

        return purchaseResult;
    }
}
