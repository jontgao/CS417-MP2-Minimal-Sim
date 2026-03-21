using UnityEngine;


public class PowerUpButton : MonoBehaviour
{
    public enum PowerType { A, B }

    public PowerType type;
    public float cost = 100f;
    public float multiplierFactor = 2f;  // x2 multiplier
    public GeneratorButton generator;
    public AudioClip sfxClip;

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
        {
            if (generator) generator.UpdateParticleEmission();
            TriggerHaptics();
            PlaySfx();
            Debug.Log("Power-up purchased!");
        }
        else
            Debug.Log("Not enough resources!");
    }

    private void TriggerHaptics()
    {
        // This was being buggy and I don't have headset to debug, but this should be the right base code?

        //UnityEngine.XR.Interaction.Toolkit.Interactors.XRBaseInteractor interactor = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable>().selectingInteractor;
        //interactor.SendHapticImpulse(0.5f, 0.1f);
    }

    private void PlaySfx()
    {
        if (sfxClip)
        {
            AudioSource.PlayClipAtPoint(sfxClip, transform.position);
        }
    }
}
