using UnityEngine;
using UnityEngine.SceneManagement;

public class UpgradeMenu : MonoBehaviour
{
    public void ExitToBase()
    {
        SceneManager.LoadScene("Underwater Base");
    }
    
}
