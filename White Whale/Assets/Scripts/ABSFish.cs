using System.Security.Cryptography;
using UnityEngine;

// standing for ABSTRACT fish. (or fish with abs)
// holds all shared functions to enable polymorphism
public abstract class ABSFish : MonoBehaviour
{
    static public int score = 0;

    public abstract void Catch();

    protected Rigidbody rb;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
}