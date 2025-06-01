using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

namespace Skills
{
    public class PlayerSkills : MonoBehaviour
    {
        [SerializeField] public List<GunData> guns;
        public static int _index = 0;
        public GunData currentGunData;
        public int coinCount = 0;
        public List<Vector3> gunPositions = new List<Vector3>();

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

            for (int i = 0; i < 8; i++)
            {
                // initialize the gun positions :)
                gunPositions.Add(new Vector3(0.51f - (0.13f * (i + 1)), 0f, 0f));
            }

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


        public float velocity = 10f;
        public float baseVelocity = 10f;
        public float dashAmt = 40f;
        //public float gameTime = 30f;
        public float roundDurationBonus = 0f;
        public int hands = 1;
        public bool goggles = false;
        public float bulletScale = 1f;
    }

}
