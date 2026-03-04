using UnityEngine;
using TMPro;

public class GeneratorButton : MonoBehaviour
{
    public enum GeneratorType { A, B }

    public GeneratorType type;
    public float cost = 20f;
    public float costGrowth = 1.5f;
    public float rateIncrease = 2f;

    private TextMeshProUGUI childText;
    
    void Start()
    {
        childText = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void PurchaseGenerator()
    {
        var manager = RampingResources.Instance;

        if (manager == null) return;

        bool purchased = false;

        if (type == GeneratorType.A)
        {
            if (manager.SpendResourceA(cost))
            {
                manager.AddGeneratorA(rateIncrease);
                purchased = true;
            }
        }
        else
        {
            if (manager.SpendResourceB(cost))
            {
                manager.AddGeneratorB(rateIncrease);
                purchased = true;
            }
        }




        if (purchased)
        {
            // update cost
            cost = (int)(cost * costGrowth);
            childText.text = $"Purchase Generator {type}\nCost {cost} {type}";
            Debug.Log("Generator Purchased!");
        }
        else
            Debug.Log("Not enough resources!");
    }
}
