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
    public class SkillNode
    {
        public string ID;
        public List<SkillNode> Prereqs = new();
        public bool IsUnlocked;
        public ISkillEffect ApplyEffect;
        // the number of times a player has unlocked this
        public int appCt = 0;
        // the maximum number of times a player can unlock this
        public int maxCt;
        public SkillNode(string _id, List<SkillNode> _pre, bool _unlocked, int _max, ISkillEffect _applyEffect)
        {
            ID = _id;
            Prereqs = _pre;
            IsUnlocked = _unlocked;
            ApplyEffect = _applyEffect;
            maxCt = _max;
        }
    }
    public class SkillTree : MonoBehaviour
    {
        public static SkillTree Instance;
        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeTree();
        }

        private Dictionary<string, SkillNode> nodes;
        public bool Unlockable(string id) => nodes[id].Prereqs.All(p => p.IsUnlocked);
     
        private void InitializeTree()
        {
            // this is the instantiator
            // there are a lot of things we could do to clean this up...
            nodes = new Dictionary<string, SkillNode>();
            SkillNode d_node = new SkillNode("Speed1", new List<SkillNode>(), false, 3, new DashMult());
            nodes.Add("Speed1", d_node);
            SkillNode s_node = new SkillNode("Speed2", new List<SkillNode> { d_node }, false, 1, new SpeedMult());
            nodes.Add("Speed2", s_node);
            SkillNode h_node = new SkillNode("Speed3", new List<SkillNode>(), false, 7, new NewHand());
            nodes.Add("Speed3", h_node);
        }
        public void Unlock(string id)
        {
            var node = nodes[id];
            if (Unlockable(id) && node.maxCt > node.appCt)
            {
                node.IsUnlocked = true;
                node.appCt++;
                node.ApplyEffect.Apply();
            }
        }
    }
    public interface ISkillEffect
    {
        void Apply();
    }

    public class SpeedMult : ISkillEffect {

        public void Apply() {
            PlayerSkills.Instance.velocity *= 4f;
        }
    }

    public class DashMult : ISkillEffect
    {
        public void Apply()
        {
            PlayerSkills.Instance.dashAmt *= 2f;
        }
    }

    public class GameAdd : ISkillEffect
    {
        public void Apply()
        {
            PlayerSkills.Instance.gameTime += 20f;
        }
    }

    public class NewHand : ISkillEffect
    {
        public void Apply()
        {
            WeaponManager.Instance.GrantWeapon();
        }
    }

}
