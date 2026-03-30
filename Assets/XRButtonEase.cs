using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections;

[RequireComponent(typeof(UnityEngine.XR.Interaction.Toolkit.Interactables.XRSimpleInteractable))]
public class XRButtonEase : MonoBehaviour
{
    [Header("Ease Settings")]
    public Vector3 squashScale = new Vector3(0.8f, 0.8f, 0.8f); // scale when pressed
    public float easeSpeed = 10f;      // how fast it eases
    public float returnDuration = 0.2f; // total time to return to original scale

    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRSimpleInteractable interactable;
    private Vector3 originalScale;

    void Awake()
    {
        interactable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRSimpleInteractable>();
        originalScale = transform.localScale;
    }

    void OnEnable()
    {
        interactable.selectEntered.AddListener(OnButtonPressed);
    }

    void OnDisable()
    {
        interactable.selectEntered.RemoveListener(OnButtonPressed);
    }

    private void OnButtonPressed(SelectEnterEventArgs args)
    {
        StopAllCoroutines();
        StartCoroutine(SquashAndRelease());
    }

    private IEnumerator SquashAndRelease()
    {
        // Step 1: Ease to squash scale
        float elapsed = 0f;
        Vector3 startScale = transform.localScale;
        while ((transform.localScale - squashScale).sqrMagnitude > 0.001f)
        {
            transform.localScale += (squashScale - transform.localScale) * easeSpeed * Time.deltaTime;
            yield return null;
        }

        // Step 2: Ease back to original scale smoothly
        elapsed = 0f;
        while ((transform.localScale - originalScale).sqrMagnitude > 0.001f)
        {
            transform.localScale += (originalScale - transform.localScale) * easeSpeed * Time.deltaTime;
            yield return null;
        }

        transform.localScale = originalScale; // ensure exact final scale
    }
}