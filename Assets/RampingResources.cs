using UnityEngine;
using TMPro;

public class RampingResources : MonoBehaviour
{
    public static RampingResources Instance;

    public GameObject resourceBGroup;   // assign parent object
    public GameObject unlockBButton;    // assign unlock cube

    public int unlockBCost = 500;

    private bool bUnlocked = false;


    [Header("Resource Values")]
    public float resourceA = 0f;
    public float resourceB = 0f;

    [Header("Generator Base Rates")]
    public float baseRateA = 1f;
    public float baseRateB = 0f;

    [Header("Multipliers")]
    public float multiplierA = 1f;
    public float multiplierB = 1f;

    [Header("UI")]
    [SerializeField] private TMP_Text resourceText;

    public float resourceBBaseRate = 1f;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Update()
    {
        float finalRateA = baseRateA * multiplierA;
        float finalRateB = baseRateB * multiplierB;

        resourceA += finalRateA * Time.deltaTime;
        resourceB += finalRateB * Time.deltaTime;

        UpdateUI();
    }

    void UpdateUI()
    {
        if (resourceText != null)
        {
            resourceText.text =
                $"A: {Mathf.FloorToInt(resourceA)}\n" +
                (bUnlocked ? $"B: {Mathf.FloorToInt(resourceB)}" : "");
        }
    }

    // Spend
    public bool SpendResourceA(float amount)
    {
        if (resourceA >= amount)
        {
            resourceA -= amount;
            return true;
        }
        return false;
    }

    public bool SpendResourceB(float amount)
    {
        if (resourceB >= amount)
        {
            resourceB -= amount;
            return true;
        }
        return false;
    }

    // Generator additions
    public void AddGeneratorA(float amount)
    {
        baseRateA += amount;
    }

    public void AddGeneratorB(float amount)
    {
        baseRateB += amount;
    }

    // Power-up multipliers
    public void MultiplyA(float factor)
    {
        multiplierA *= factor;
    }

    public void MultiplyB(float factor)
    {
        multiplierB *= factor;
    }

    // public void TryUnlockB()
    // {
    //     if (bUnlocked) return;

    //     if (resourceA >= unlockBCost)
    //     {
    //         resourceA -= unlockBCost;
    //         bUnlocked = true;

    //         resourceBGroup.SetActive(true);   // show B UI
    //         unlockBButton.SetActive(false);   // hide unlock button
    //     }
    // }
    public void TryUnlockB()
    {
        if (bUnlocked) return;

        if (resourceA >= unlockBCost)
        {
            resourceA -= unlockBCost;
            bUnlocked = true;

            baseRateB = resourceBBaseRate;   // START B growth

            resourceBGroup.SetActive(true);      // show B UI
            unlockBButton.SetActive(false);      // hide unlock button
        }
    }


}
