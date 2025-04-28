using UnityEngine;

public class scoreManager : MonoBehaviour
{
    public int score = 0;

    public void Increment(int value)
    {
        score += value;
    }
}
