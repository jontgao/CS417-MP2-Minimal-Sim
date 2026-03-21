using UnityEngine;
using TMPro;

public class GeneratorButton : MonoBehaviour
{
    public enum GeneratorType { A, B }

    public GeneratorType type;
    public float cost = 20f;
    public float costGrowth = 1.5f;
    public float rateIncrease = 2f;
    public AudioClip sfxClip;

    public ParticleSystem generatorParticles;
    private float emissionMultiplier = 0.5f;

    public GameObject generatorVisual;
    private float generatorVisualGrowRate = 0.2f;
    private Vector3 generatorVisualTargetScale;

    private TextMeshProUGUI childText;

    void Start()
    {
        childText = GetComponentInChildren<TextMeshProUGUI>();
        UpdateParticleEmission();
        if (generatorVisual) generatorVisualTargetScale = generatorVisual.transform.localScale;
    }

    void Update()
    {
        if (generatorVisual)
        {
            generatorVisual.transform.localScale = Vector3.Lerp(
                generatorVisual.transform.localScale,
                generatorVisualTargetScale,
                Time.deltaTime * 10f
            );
        }
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
            UpdateParticleEmission();
            UpdateGeneratorVisual();
            PlaySfx();
        }
        else
            Debug.Log("Not enough resources!");
    }

    public void UpdateParticleEmission()
    {
        if (generatorParticles)
        {
            var manager = RampingResources.Instance;
            var emission = generatorParticles.emission;
            emission.rateOverTime = manager.GetFinalRate(type) * emissionMultiplier;
        }
    }

    private void UpdateGeneratorVisual()
    {
        generatorVisualTargetScale.y += generatorVisualGrowRate;
    }

    private void PlaySfx()
    {
        if (sfxClip)
        {
            AudioSource.PlayClipAtPoint(sfxClip, transform.position);
        }
    }
}
