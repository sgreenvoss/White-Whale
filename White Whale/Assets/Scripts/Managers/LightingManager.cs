using Skills;
using UnityEngine;

public class LightingManager : MonoBehaviour
{
    public bool gogglesOn = false;

    private Color GoggleFogColor = new Color(0.1176f, 0.3255f, 0.4353f); 
    private float GoggleFogDensity = 0.015f;

    private Color defaultFogColor;
    private float defaultFogDensity;

    void Start()
    {
        defaultFogColor = RenderSettings.fogColor;
        defaultFogDensity = RenderSettings.fogDensity;
        if (PlayerSkills.Instance.goggles)
        {
            PutGogglesOn();
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            PutGogglesOn();
        }
    }

    void PutGogglesOn()
    {
        gogglesOn = true;
        RenderSettings.fogColor = GoggleFogColor;
        RenderSettings.fogDensity = GoggleFogDensity;
        Debug.Log("Goggles on!");
    }

    void TakeGogglesOff()
    //This function is mostly for testing
    {
        gogglesOn = false;
        RenderSettings.fogColor = defaultFogColor;
        RenderSettings.fogDensity = defaultFogDensity;
        Debug.Log("Goggles off!");
    }


}