using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(VerticalLayoutGroup))]
public class ResponsiveSpacing : MonoBehaviour
{
    private VerticalLayoutGroup layout;
    private float referenceHeight = 1080f; // Use your design reference height

    public float baseSpacing = 100f;

    void Start()
    {
        layout = GetComponent<VerticalLayoutGroup>();
        UpdateSpacing();
    }

    void Update()
    {
        UpdateSpacing(); // You could optimize by only calling on resolution change
    }

    void UpdateSpacing()
    {
        float currentHeight = Screen.height;
        float scaleFactor = currentHeight / referenceHeight;
        layout.spacing = baseSpacing * scaleFactor * 10;
    }
}
