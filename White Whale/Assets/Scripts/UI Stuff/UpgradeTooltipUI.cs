using UnityEngine;
using TMPro;

public class UpgradeTooltipUI : MonoBehaviour
{
    public static UpgradeTooltipUI Instance;

    [SerializeField] private RectTransform tooltipPanel;
    [SerializeField] private TextMeshProUGUI tooltipText;

    void Awake()
    {
        Instance = this;
        Hide();
    }

    public void Show(string description, RectTransform buttonRect)
    {
        tooltipText.text = description;

        // Get the world corners of the button
        Vector3[] corners = new Vector3[4];
        buttonRect.GetWorldCorners(corners);

        // Bottom left corner of the button in world space
        Vector3 bottomLeft = corners[0];

        // Convert bottomLeft world poisition to screen point
        Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(null, bottomLeft);



        RectTransform canvasRect = (RectTransform)tooltipPanel.transform.parent;
        Vector2 anchoredPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvasRect, screenPoint, null, out anchoredPos);

        anchoredPos.y -= 30f;
        anchoredPos.x += 60f;

        // Set tooltip panel position relative to its parent canvas
        ((RectTransform)tooltipPanel.transform).anchoredPosition = anchoredPos;

        tooltipPanel.gameObject.SetActive(true);

    }


    public void Hide()
    {
        tooltipPanel.gameObject.SetActive(false);
    }
}
