using UnityEngine;

public class InsuranceController : MSingleton<InsuranceController>
{
    public int maxInsuranceCount;
    public CurrencyData insuranceData;
    public Purchasable insurancePrice;

    public void UseInsurance()
    {
        if (insuranceData.Amount <= 0)
        {
            Debug.Log("No insurance left!");
            return;
        }

        insuranceData.AddAmount(-1);
        GameManager.Instance.RecoverGame();
    }

    public void BuyInsurance()
    {
        if (insuranceData.Amount >= maxInsuranceCount)
        {
            Debug.Log("Reached max insurance count.");
            return;
        }

        insuranceData.AddAmount(1);
        insurancePrice.Purchase();
    }
}
