using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(VerticalLayoutGroup))]
public class ResponsiveSpacing : MonoBehaviour
{
    private VerticalLayoutGroup layout;
    private float referenceHeight = 1080f;

    public float baseSpacing = 50f;
    public float baseBottomPadding = 20f;

    private Vector2 lastResolution;

    void Start()
    {
        layout = GetComponent<VerticalLayoutGroup>();
        lastResolution = new Vector2(Screen.width, Screen.height);
        UpdateSpacing();
    }

    void Update()
    {
        if (Screen.width != lastResolution.x || Screen.height != lastResolution.y)
        {
            lastResolution = new Vector2(Screen.width, Screen.height);
            UpdateSpacing();
        }
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
