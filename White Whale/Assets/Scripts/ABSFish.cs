using System.Security.Cryptography;
using UnityEngine;

// standing for ABSTRACT fish. (or fish with abs)
// holds all shared functions to enable polymorphism
public abstract class ABSFish : MonoBehaviour
{
    [SerializeField]
    public int fish_score;

    public static int total_score;

    public int max_health;

    protected int current_health; 

    static public int score = 0;

    public abstract void Catch();

    protected Rigidbody rb;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody>();

        current_health = max_health;
    }

    public virtual void Damage(int damage)
    {
        current_health -= damage;
        if (current_health <= 0)
        {
            Catch();
        }
    }
}