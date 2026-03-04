using UnityEngine;
using TMPro;

public class Trophy : MonoBehaviour
{
    public TextMeshPro childText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Initialize(string text)
    {
        if (childText)
        {
            childText.text = text;
        } else
        {
            Debug.LogError("Can't find child text");
        }
    }
}
