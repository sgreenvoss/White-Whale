using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;



// standing for ABSTRACT fish. (or fish with abs)
// holds all shared functions to enable polymorphism
public abstract class ABSFish : MonoBehaviour
{
    [SerializeField]
    public int fish_score;

    public static int total_score;

    int coin_mult = 10;
    public static int total_coins;

    public int max_health;

    protected int current_health; 

    static public int score = 0;

    public abstract void Catch();

    protected Rigidbody rb;

    public int high_score;
    
    [SerializeField] private AudioClip[] fishCaught;



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
            AudioManager.instance.PlayRandomSoundClip(fishCaught, transform, 2f);
            int my_coin = fish_score * coin_mult;
            total_coins += my_coin;
            Debug.Log("coins: " + total_coins.ToString());
            
            Catch();
        }
    }

    public static void ResetFishStats()
    {
        total_score = 0;
        total_coins = 0;
        score = 0;
    }

}