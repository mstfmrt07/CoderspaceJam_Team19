using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CurrencyUI : MonoBehaviour
{
    public CurrencyData data;
    public Image currencyImage;
    public TextMeshProUGUI valueText;

    private void Awake()
    {
        UpdateUI();
        data.OnCurrencyUpdated += OnCurrencyUpdated;
    }

    private void OnCurrencyUpdated(int amount)
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        currencyImage.sprite = data.sprite;
        valueText.text = "x" + data.Amount.ToString();
    }
}
