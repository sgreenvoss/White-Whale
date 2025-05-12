using System.Linq;
using UnityEngine;
using System.Collections.Generic;
using System;


namespace Skills
{
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
            SkillNode default_node = new SkillNode("", new List<SkillNode>(), _applyEffect: new NotImplemented());
            SkillNode d_node = new SkillNode("Speed1", new List<SkillNode>(), false, 3, new DashMult());
            nodes.Add("Speed1", d_node);
            SkillNode s_node = new SkillNode("Speed2", new List<SkillNode> { d_node }, false, 1, new SpeedMult());
            nodes.Add("Speed2", s_node);
            SkillNode h_node = new SkillNode("Speed3", new List<SkillNode>(), false, 7, new NewHand());
            nodes.Add("Speed3", h_node);

            SkillNode gun1 = new SkillNode("Weapon1", new List<SkillNode>(), _applyEffect: new IncreaseGun());
            nodes.Add("Weapon1", gun1);
            SkillNode gun2 = new SkillNode("Weapon2", new List<SkillNode> { gun1 }, _applyEffect: new IncreaseGun());
            nodes.Add("Weapon2", gun2);
            nodes.Add("Weapon3", default_node);
            nodes.Add("Oxygen1", default_node);
            nodes.Add("Oxygen2", default_node);
            nodes.Add("Oxygen3", default_node);

        }
        public void Unlock(string id)
        {
            Debug.Log(id);
            var node = nodes[id];
            Debug.Log(node == null);
            if (Unlockable(id) && node.maxCt > node.appCt)
            {
                Debug.Log("unlockable here");
                node.IsUnlocked = true;
                node.appCt++;
                node.ApplyEffect.Apply();
            }
        }
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
        public SkillNode(string _id, List<SkillNode> _pre, bool _unlocked = false, int _max = 1, ISkillEffect _applyEffect = null)
        {
            ID = _id;
            Prereqs = _pre;
            IsUnlocked = _unlocked;
            ApplyEffect = _applyEffect;
            maxCt = _max;
        }
    }

    public interface ISkillEffect
    {
        void Apply();
    }

    public class NotImplemented : ISkillEffect
    {
        public void Apply()
        {
            Debug.Log("Not yet implemented, sorry!");
        }
    }

    public class SpeedMult : ISkillEffect
    {

        public void Apply()
        {
            PlayerSkills.Instance.velocity *= 4f;
            Debug.Log("speedmult");
        }
    }
    
    public class IncreaseGun : ISkillEffect
    {
        public void Apply()
        {
            Debug.Log("applying....");
            var skills = PlayerSkills.Instance;
            int nextIndex = PlayerSkills._index + 1;
            if (nextIndex < skills.guns.Count)
            {
                PlayerSkills.Instance.SwapIndex(nextIndex);
            }
            else
            {
                Debug.Log("No additional guns to swap to.");
            }
        }
    }

    public class DashMult : ISkillEffect
    {
        public void Apply()
        {
            PlayerSkills.Instance.dashAmt *= 2f;
            Debug.Log("dashmult");
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
       //     WeaponManager.Instance.GrantWeapon();
            Debug.Log("hands");
        }
    }
}
