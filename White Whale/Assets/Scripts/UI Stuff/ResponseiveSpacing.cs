using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(VerticalLayoutGroup))]
public class ResponsiveSpacing : MonoBehaviour
{
    private VerticalLayoutGroup layout;
    private float referenceHeight = 1080f; // Use your design reference height

    public float baseSpacing = 50f;
    public float baseBottomPadding = 20f;

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
        layout.padding.bottom = Mathf.RoundToInt(baseBottomPadding * scaleFactor) * 10;

        // Force Layout refresh
        LayoutRebuilder.MarkLayoutForRebuild(layout.GetComponent<RectTransform>());
    }
}
