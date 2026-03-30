using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(UnityEngine.XR.Interaction.Toolkit.Interactables.XRSimpleInteractable))]
public class SimpleInteractableParticles : MonoBehaviour
{
    [Header("Particle Effect")]
    public ParticleSystem clickParticles;  // assign your particle system here

    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRSimpleInteractable interactable;

    void Awake()
    {
        interactable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRSimpleInteractable>();
    }

    void OnEnable()
    {
        interactable.selectEntered.AddListener(OnInteractableSelected);
    }

    void OnDisable()
    {
        interactable.selectEntered.RemoveListener(OnInteractableSelected);
    }

    private void OnInteractableSelected(SelectEnterEventArgs args)
    {
        // Play particle effect if assigned
        if (clickParticles != null)
        {
            // optional: random rotation for variation
            clickParticles.transform.localRotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
            clickParticles.Play();
        }

        // Trigger haptics on the controller that interacted
        var controllerInteractor = args.interactorObject as UnityEngine.XR.Interaction.Toolkit.Interactors.XRBaseInputInteractor;
        if (controllerInteractor != null && controllerInteractor.xrController != null)
        {
            controllerInteractor.xrController.SendHapticImpulse(0.5f, 0.1f);
        }
    }
}