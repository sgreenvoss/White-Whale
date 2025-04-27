using System.Security.Cryptography;
using UnityEngine;

// standing for ABSTRACT fish. (or fish with abs)
// holds all shared functions to enable polymorphism
public abstract class ABSFish : MonoBehaviour
{
    public abstract void Catch();
}