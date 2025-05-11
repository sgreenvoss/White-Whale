using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleporter : MonoBehaviour
{
    [SerializeField] private string targetSceneName; // Assign this in the Inspector

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Entered trigger: " + other.name);

        if (other.CompareTag("Player"))
        {
            //Debug.Log("Teleporting to: " + targetSceneName);
            SceneManager.LoadScene(targetSceneName);
        }
    }
}
