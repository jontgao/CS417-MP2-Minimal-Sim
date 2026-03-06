using UnityEngine;

public class Clicker : MonoBehaviour
{
    public enum ResourceType { A, B }
    public ResourceType type;

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
    }
}
