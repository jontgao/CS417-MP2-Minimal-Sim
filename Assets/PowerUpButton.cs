using UnityEngine;

public class PowerUpButton : MonoBehaviour
{
    public enum PowerType { A, B }

    public PowerType type;
    public float cost = 100f;
    public float multiplierFactor = 2f;  // x2 multiplier

    public void PurchasePowerUp()
    {
        var manager = RampingResources.Instance;
        if (manager == null) return;

        bool purchased = false;

        if (type == PowerType.A)
        {
            if (manager.SpendResourceA(cost))
            {
                manager.MultiplyA(multiplierFactor);
                purchased = true;
            }
        }
        else
        {
            if (manager.SpendResourceB(cost))
            {
                manager.MultiplyB(multiplierFactor);
                purchased = true;
            }
        }

        if (purchased)
            Debug.Log("Power-up purchased!");
        else
            Debug.Log("Not enough resources!");
    }
}
