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
        public static PlayerSkills Instance;
        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Debug.Log("called dontdestroyonload");
        }

        public float velocity = 10f;
        public float dashAmt = 40f;
        public bool longDash = false;
        public float gameTime = 30f;
        public int hands = 1;
    }

}
