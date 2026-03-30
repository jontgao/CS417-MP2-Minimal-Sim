// using UnityEngine;

// public class ResourceCylinder : MonoBehaviour
// {
//     public enum ResourceType { A, B }   // local enum

//     [Header("Settings")]
//     public ResourceType resourceType = ResourceType.A;
//     public float heightMultiplier = 0.1f;  // how tall the cylinder gets per unit of resource
//     public float easeSpeed = 5f;           // higher = snappier easing
//     public float minHeight = 0.1f;         // minimum height to avoid disappearing

//     private Vector3 initialScale;

//     void Awake()
//     {
//         initialScale = transform.localScale;
//     }

//     void Update()
//     {
//         if (RampingResources.Instance == null) return;

//         // Get resource magnitude
//         float targetHeight = minHeight;

//         if (resourceType == ResourceType.A)
//             targetHeight += RampingResources.Instance.resourceA * heightMultiplier;
//         else
//             targetHeight += RampingResources.Instance.resourceB * heightMultiplier;

//         // Ease cylinder scale Y to target height
//         Vector3 currentScale = transform.localScale;
//         float newY = Mathf.Lerp(currentScale.y, targetHeight, easeSpeed * Time.deltaTime);
//         transform.localScale = new Vector3(currentScale.x, newY, currentScale.z);

//         // Keep base at the same position (pivot at center)
//         Vector3 pos = transform.localPosition;
//         pos.y = transform.localScale.y / 2f;
//         transform.localPosition = pos;
//     }
// }

using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ResourceCylinder : MonoBehaviour
{
    public enum ResourceType { A, B }

    [Header("Settings")]
    public ResourceType resourceType = ResourceType.A;
    public float heightMultiplier = 0.1f;  // how tall the cylinder grows per unit
    public float easeSpeed = 5f;           // speed of easing
    public float minHeight = 0.1f;         // minimum cylinder height

    [Header("Milestone Feedback")]
    public int milestone = 10;              // trigger every N units
    public AudioClip milestoneSfx;          // sound to play
    public ParticleSystem milestoneParticles; // optional particle effect
    public float hapticAmplitude = 0.5f;    // 0-1
    public float hapticDuration = 0.1f;     // in seconds

    private Vector3 initialScale;
    private int lastMilestoneHit = 0;

    void Awake()
    {
        initialScale = transform.localScale;
    }

    void Update()
    {
        if (RampingResources.Instance == null) return;

        // Get current resource value
        float currentResource = (resourceType == ResourceType.A) ?
                                RampingResources.Instance.resourceA :
                                RampingResources.Instance.resourceB;

        // ===== Smooth cylinder height =====
        float targetHeight = minHeight + currentResource * heightMultiplier;
        Vector3 currentScale = transform.localScale;
        float newY = Mathf.Lerp(currentScale.y, targetHeight, easeSpeed * Time.deltaTime);
        transform.localScale = new Vector3(currentScale.x, newY, currentScale.z);

        // Keep base at floor (pivot at center)
        Vector3 pos = transform.localPosition;
        pos.y = transform.localScale.y / 2f;
        transform.localPosition = pos;

        // ===== Milestone check =====
        int milestoneCount = Mathf.FloorToInt(currentResource / milestone);
        if (milestoneCount > lastMilestoneHit)
        {
            lastMilestoneHit = milestoneCount;

            // Sound
            if (milestoneSfx != null)
                AudioSource.PlayClipAtPoint(milestoneSfx, transform.position);

            // Particles
            if (milestoneParticles != null)
                milestoneParticles.Play();

            // Haptics
            TriggerHaptics();
        }
    }

    private void TriggerHaptics()
    {
        // Find all active XRBaseControllers in scene
        XRBaseController[] controllers = FindObjectsOfType<XRBaseController>();
        foreach (var controller in controllers)
        {
            controller.SendHapticImpulse(hapticAmplitude, hapticDuration);
        }
    }
}