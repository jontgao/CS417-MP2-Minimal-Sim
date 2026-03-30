using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections;

[RequireComponent(typeof(UnityEngine.XR.Interaction.Toolkit.Interactables.XRSimpleInteractable))]
public class JuicyXRButton : MonoBehaviour
{
    [Header("Feedback")]
    public ParticleSystem clickParticles;
    public AudioClip sfxClip;
    public float hapticAmplitude = 0.5f;
    public float hapticDuration = 0.1f;

    [Header("Ease Settings")]
    public float easeSpeed = 10f;  // higher = faster
    public float disappearDuration = 0.5f;

    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRSimpleInteractable interactable;
    private Vector3 initialScale;

    void Awake()
    {
        interactable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRSimpleInteractable>();
        initialScale = transform.localScale;
    }

    void OnEnable()
    {
        interactable.selectEntered.AddListener(OnClick);
    }

    void OnDisable()
    {
        interactable.selectEntered.RemoveListener(OnClick);
    }

    private void OnClick(SelectEnterEventArgs args)
    {
        // Trigger particle
        if (clickParticles != null)
        {
            clickParticles.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            clickParticles.Play();
        }

        // Play sound
        if (sfxClip != null)
        {
            AudioSource.PlayClipAtPoint(sfxClip, transform.position);
        }

        // Trigger haptics
        var controller = args.interactorObject as UnityEngine.XR.Interaction.Toolkit.Interactors.XRBaseInputInteractor;
        if (controller != null && controller.xrController != null)
        {
            controller.xrController.SendHapticImpulse(hapticAmplitude, hapticDuration);
        }

        // Start ease-out coroutine
        StartCoroutine(EaseOutAndDestroy());
    }

    private IEnumerator EaseOutAndDestroy()
    {
        float elapsed = 0f;
        Vector3 startScale = transform.localScale;
        Vector3 targetScale = Vector3.zero;

        while (elapsed < disappearDuration)
        {
            elapsed += Time.deltaTime;
            // Simple exponential ease for scale
            transform.localScale += (targetScale - transform.localScale) * easeSpeed * Time.deltaTime;
            yield return null;
        }

        // Ensure fully zeroed
        transform.localScale = Vector3.zero;

        // Optional: Destroy after ease
        Destroy(gameObject);
    }
}