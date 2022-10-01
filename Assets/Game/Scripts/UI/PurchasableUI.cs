using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PurchasableUI : MonoBehaviour
{
    public Purchasable data;
    public Image itemImage;
    public TextMeshProUGUI valueText;

    private void Awake()
    {
        UpdateUI();
        data.OnItemUpgraded += OnItemUpgraded;
    }

    public void Purchase()
    {
        data.Purchase();
    }

    private void OnItemUpgraded()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        itemImage.sprite = data.requiredCurrency.sprite;
        valueText.text = data.CurrentCost.ToString();
    }
}
