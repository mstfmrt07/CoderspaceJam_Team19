using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PurchasableUI : MonoBehaviour
{
    public Purchasable item;
    public CanvasGroup canvasGroup;
    public Image itemImage;
    public Image requiredCurrencyImage;

    public TextMeshProUGUI titleText;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI valueText;

    private void Awake()
    {
        UpdateUI();
        item.requiredCurrency.OnCurrencyUpdated += (_) => UpdateUI();
        item.OnItemUpgraded += UpdateUI;
    }

    public void Purchase()
    {
        ShopController.Instance.BuyItem(item);
    }

    private void UpdateUI()
    {
        requiredCurrencyImage.sprite = item.requiredCurrency.sprite;
        itemImage.sprite = item.icon;

        valueText.text = item.CurrentCost.ToString();
        titleText.text = item.itemName.ToUpper();
        descriptionText.text = item.description.ToUpper();

        canvasGroup.interactable = (item.inventoryItem.Amount < item.maxCount) && (item.requiredCurrency.Amount >= item.CurrentCost);
    }
}