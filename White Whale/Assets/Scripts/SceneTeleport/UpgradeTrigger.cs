using UnityEngine;
using UnityEngine.SceneManagement;


public class UpgradeTrigger : MonoBehaviour
{
    private void OnMouseDown()
    {
        SceneManager.LoadScene("UpgradeScene");
    }
    
}
