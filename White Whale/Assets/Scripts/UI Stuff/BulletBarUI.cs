using UnityEngine;
using UnityEngine.UI;



public class BulletBarUI : MonoBehaviour
{
    [SerializeField] private Image[] bullets; 

    [SerializeField] private Color activeColor = Color.red;
    [SerializeField] private Color inactiveColor = Color.gray;

    private int totalAmmo = 6;
    public void SetTotalAmmo(int newMaxAmmo)
    {
        totalAmmo = newMaxAmmo;
        Debug.Log($"[BulletBarUI] Total ammo set to: {totalAmmo}");
    }


    public void UpdateBulletDisplay(int currentAmmo)
    {
        Debug.Log($"[TEST] Updating bullet visuals to reflect: {currentAmmo}");


        //Debug.Log($"[TEST] Bullet[0] color: {bullets[0]?.color}");




        int iconCount = bullets.Length;
        float ammoPerIcon =(float)totalAmmo / iconCount;

        int activeIcons = Mathf.CeilToInt((float)currentAmmo / ammoPerIcon);





        for (int i = 0; i < iconCount; i++)
        {

            bullets[i].color = (i < activeIcons) ? activeColor : inactiveColor;

            /*
            int reverseIndex = iconCount - 1 - i;

            float threshold = ammoPerIcon * i;
            bullets[reverseIndex].color = currentAmmo > threshold ? activeColor : inactiveColor;

            // Failed linear interpolation attempt


            float chunkStart = i * bulletsPerIcon;
            float chunkEnd = (i + 1) * bulletsPerIcon;


            float fill = Mathf.Clamp01((currentAmmo - chunkStart) / bulletsPerIcon);


            Color iconColor;
            if (fill >= 1f)
            {
                iconColor = activeColor;
            }
            else if (fill > 0f)
            {
                iconColor = Color.Lerp(inactiveColor, activeColor, 0.5f);
            }
            else
            {
                iconColor = inactiveColor;
            }



            bullets[i].color = iconColor;


            */
        }


    }
    
}
