using UnityEngine;

public class FacePlayer : MonoBehaviour
{
    public Transform playerCamera;

    void Update()
    {
        transform.LookAt(playerCamera);
        transform.Rotate(0,180,0);
    }
}
