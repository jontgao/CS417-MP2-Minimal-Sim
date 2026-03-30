// using UnityEngine;


// public class PowerUpButton : MonoBehaviour
// {
//     public enum PowerType { A, B }

//     public PowerType type;
//     public float cost = 100f;
//     public float multiplierFactor = 2f;  // x2 multiplier
//     public GeneratorButton generator;
//     public AudioClip sfxClip;

//     public void PurchasePowerUp()
//     {
//         var manager = RampingResources.Instance;
//         if (manager == null) return;

//         bool purchased = false;

//         if (type == PowerType.A)
//         {
//             if (manager.SpendResourceA(cost))
//             {
//                 manager.MultiplyA(multiplierFactor);
//                 purchased = true;
//             }
//         }
//         else
//         {
//             if (manager.SpendResourceB(cost))
//             {
//                 manager.MultiplyB(multiplierFactor);
//                 purchased = true;
//             }
//         }

//         if (purchased)
//         {
//             if (generator) generator.UpdateParticleEmission();
//             TriggerHaptics();
//             PlaySfx();
//             Debug.Log("Power-up purchased!");
//         }
//         else
//             Debug.Log("Not enough resources!");
//     }

//     private void TriggerHaptics()
//     {
//         // This was being buggy and I don't have headset to debug, but this should be the right base code?

//         //UnityEngine.XR.Interaction.Toolkit.Interactors.XRBaseInteractor interactor = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable>().selectingInteractor;
//         //interactor.SendHapticImpulse(0.5f, 0.1f);
//     }

//     private void PlaySfx()
//     {
//         if (sfxClip)
//         {
//             AudioSource.PlayClipAtPoint(sfxClip, transform.position);
//         }
//     }
// }

using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PowerUpButton : MonoBehaviour
{
    public enum PowerType { A, B }

    [Header("Power-Up Settings")]
    public PowerType type;
    public float cost = 100f;
    public float multiplierFactor = 2f;  // x2 multiplier
    public GeneratorButton generator;

    [Header("Feedback")]
    public AudioClip sfxClip;
    public ParticleSystem clickParticles;  // assign your particle prefab here

    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRSimpleInteractable interactable;

    void Awake()
    {
        interactable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRSimpleInteractable>();
    }

    void OnEnable()
    {
        if (interactable != null)
            interactable.selectEntered.AddListener(OnButtonPressed);
    }

    void OnDisable()
    {
        if (interactable != null)
            interactable.selectEntered.RemoveListener(OnButtonPressed);
    }

    private void OnButtonPressed(SelectEnterEventArgs args)
    {
        PurchasePowerUp();

        // Trigger particle effect
        if (clickParticles != null)
        {
            clickParticles.Play();
        }

        // Trigger haptics on the interacting controller
        TriggerHaptics(args);
    }

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
            PlaySfx();
            Debug.Log("Power-up purchased!");
        }
        else
            Debug.Log("Not enough resources!");
    }

    private void TriggerHaptics(SelectEnterEventArgs args)
    {
        var controllerInteractor = args.interactorObject as UnityEngine.XR.Interaction.Toolkit.Interactors.XRBaseInputInteractor;
        if (controllerInteractor != null && controllerInteractor.xrController != null)
        {
            controllerInteractor.xrController.SendHapticImpulse(0.5f, 0.1f);
        }
    }

    private void PlaySfx()
    {
        if (sfxClip)
        {
            AudioSource.PlayClipAtPoint(sfxClip, transform.position);
        }
    }
}