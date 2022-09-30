using UnityEngine;
using UnityEngine.UI;

public class CurrencyUI : MonoBehaviour
{
    public CurrencyData data;
    public Image currencyImage;
    public Text valueText;

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
        valueText.text = data.Amount.ToString("00");
    }
}
