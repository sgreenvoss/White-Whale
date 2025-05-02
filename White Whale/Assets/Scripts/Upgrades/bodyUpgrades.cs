using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

namespace Skills
{
    public class PlayerSkills
    {
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
    public class SkillTree
    {
        private PlayerSkills skills;
        private Dictionary<string, SkillNode> nodes;
        public bool Unlockable(string id) => nodes[id].Prereqs.All(p => p.IsUnlocked);
     
        public SkillTree(PlayerSkills _skills)
        {
            // this is the instantiator
            // there are a lot of things we could do to clean this up...
            skills = _skills;
            nodes = new Dictionary<string, SkillNode>();
            SkillNode d_node = new SkillNode("dashMult", new List<SkillNode>(), false, 3, new DashMult());
            nodes.Add("dashMult", d_node);
            SkillNode s_node = new SkillNode("speedMult", new List<SkillNode> { d_node }, false, 1, new SpeedMult());
            nodes.Add("speedMult", s_node);
            SkillNode h_node = new SkillNode("NewHand", new List<SkillNode>(), false, 7, new NewHand());
            nodes.Add("NewHand", h_node);
        }
        public void Unlock(string id)
        {
            var node = nodes[id];
            if (Unlockable(id) && node.maxCt > node.appCt)
            {
                node.IsUnlocked = true;
                node.appCt++;
                node.ApplyEffect.Apply(skills);
            }
        }
    }
    public interface ISkillEffect
    {
        void Apply(PlayerSkills skills);
    }

    public class SpeedMult : ISkillEffect {

        public void Apply(PlayerSkills skills) {
            skills.velocity *= 2f;
        }
    }

    public class DashMult : ISkillEffect
    {
        public void Apply(PlayerSkills skills)
        {
            skills.dashAmt *= 2f;
        }
    }

    public class GameAdd : ISkillEffect
    {
        public void Apply(PlayerSkills skills)
        {
            skills.gameTime += 20f;
        }
    }

    public class NewHand : ISkillEffect
    {
        public void Apply(PlayerSkills skills)
        {
            WeaponManager.Instance.GrantWeapon();
        }
    }

}
