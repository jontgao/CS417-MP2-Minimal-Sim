using UnityEngine;

public class Clicker : MonoBehaviour
{
    public enum ResourceType { A, B }
    public ResourceType type;
    public AudioClip sfxClip;

    public void AddResource()
    {
        var manager = RampingResources.Instance;
        if (manager == null) return;

        if (type == ResourceType.A)
        {
            manager.addA();
        }
        else
        {
            manager.addB();
        }

        if (sfxClip != null)
        {
            AudioSource.PlayClipAtPoint(sfxClip, transform.position);
        }
    }
}
