using UnityEngine;
using TMPro;

public class RampingResources : MonoBehaviour
{
    public static RampingResources Instance;

    public GameObject resourceBGroup;   // assign parent object
    public GameObject unlockBButton;    // assign unlock cube
    public GameObject trophy;
    public TutorialManager tutorialManager;
    private int trophiesUnlocked = 0;

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

    public AudioClip sfxClip;

    private float finalRateA = 0f;
    private float finalRateB = 0f;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Update()
    {
        finalRateA = baseRateA * multiplierA;
        finalRateB = baseRateB * multiplierB;

        resourceA += finalRateA * Time.deltaTime;
        resourceB += finalRateB * Time.deltaTime;

        UpdateUI();
        CheckTrophies();
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

    public void addA()
    {
        resourceA += 1;
    }

    public void addB()
    {
        resourceB += 1;
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
            // unlockBButton.SetActive(false);      // hide unlock button
            tutorialManager.ShowResourceBTutorial();

            if (sfxClip != null)
            {
                AudioSource.PlayClipAtPoint(sfxClip, resourceBGroup.transform.position);
            }
        }
    }

    public void CheckTrophies()
    {
        if (resourceA >= Mathf.Pow(10,trophiesUnlocked + 2))
        {
            GameObject newTrophy = Instantiate(trophy, new Vector3(0f, 1f + (1f * trophiesUnlocked), 4f), Quaternion.identity);
            newTrophy.GetComponent<Trophy>().Initialize($"{Mathf.FloorToInt(resourceA)} A");
            ++trophiesUnlocked;
        }
        if (trophiesUnlocked == 1) 
        {
            tutorialManager.ShowTrophyTutorial();
        }
    }

    public float GetFinalRate(GeneratorButton.GeneratorType type)
    {
        return type == GeneratorButton.GeneratorType.A ? finalRateA : finalRateB;
    }

}
