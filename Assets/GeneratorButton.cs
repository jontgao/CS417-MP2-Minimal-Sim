using UnityEngine;

public class GeneratorButton : MonoBehaviour
{
    public enum GeneratorType { A, B }

    public GeneratorType type;
    public float cost = 20f;
    public float rateIncrease = 2f;

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
            Debug.Log("Generator Purchased!");
        else
            Debug.Log("Not enough resources!");
    }
}
