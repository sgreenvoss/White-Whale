using UnityEngine;
using UnityEngine.UI;



public class BulletBarUI : MonoBehaviour
{
    [SerializeField] private Image[] bullets; 

    [SerializeField] private Color activeColor = Color.red;
    [SerializeField] private Color inactiveColor = Color.gray;

    public void UpdateBulletDisplay(int currentAmmo)
    {
        Debug.Log($"[TEST] Updating bullet visuals to reflect: {currentAmmo}");

//        foreach (var img in bullets)
//        {
//            if (img != null)
//                img.color = new Color(1f, 0f, 1f, 1f);;    // Forcing color change
//        }

        Debug.Log($"[TEST] Bullet[0] color: {bullets[0]?.color}");

        for (int i = 0; i < bullets.Length; i++)
        {
            bullets[i].color = i < currentAmmo ? activeColor : inactiveColor;
        }
    }
    
}
