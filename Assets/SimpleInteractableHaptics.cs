using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SimpleInteractableHaptics : MonoBehaviour
{
    public float amplitude = 0.5f;
    public float duration = 0.1f;

    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRSimpleInteractable interactable;

    void Awake()
    {
        interactable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRSimpleInteractable>();
    }

    void OnEnable()
    {
        interactable.selectEntered.AddListener(OnSelect);
    }

    void OnDisable()
    {
        interactable.selectEntered.RemoveListener(OnSelect);
    }

    void OnSelect(SelectEnterEventArgs args)
    {
        // Get the controller that triggered the interaction
        var controllerInteractor = args.interactorObject as UnityEngine.XR.Interaction.Toolkit.Interactors.XRBaseInputInteractor;

        if (controllerInteractor != null && controllerInteractor.xrController != null)
        {
            controllerInteractor.xrController.SendHapticImpulse(amplitude, duration);
        }
        else
        {
            Debug.Log("Haptics: No valid controller found");
        }
    }
}