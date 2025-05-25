using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

namespace Skills
{
    public class PlayerSkills : MonoBehaviour
    {
        [SerializeField] public List<GunData> guns;
        public static int _index = 0;
        public GunData currentGunData;
        
        public static PlayerSkills Instance;
        private void Awake()
        {
            currentGunData = guns[Mathf.Clamp(_index, 0, guns.Count - 1)];
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
            
        }

        public void SwapIndex(int index)
        {
            if (index >= 0 && index < guns.Count)
            {
                _index = index;
                currentGunData = guns[_index]; 
                Debug.Log("Swapped to gun: " + currentGunData.name);
            }
            else
            {
                Debug.LogWarning("Invalid gun index: " + index);
            }
        }

        public int GetCurrentGunMaxAmmo()
        {
            if (currentGunData != null)
                return currentGunData != null ? currentGunData.capacity : 0;

            Debug.LogWarning("currentGunData is null!");
            return 0;
        }


        public float lightIntensity = 0f;
        public float lightDistance = 5f;
        public float velocity = 10f;
        public float baseVelocity = 10f;
        public float dashAmt = 40f;
        public bool longDash = false;
        public float gameTime = 30f;
        public int hands = 1;
    }

}
