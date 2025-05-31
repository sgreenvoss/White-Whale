using UnityEngine;
using UnityEngine.SceneManagement;


public class TutorialTeleport : MonoBehaviour
{
    private void OnMouseDown()
    {
        SceneManager.LoadScene("TutorialScene");
    }
    
}
