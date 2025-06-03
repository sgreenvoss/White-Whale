using System.Linq;
using UnityEngine;
using System.Collections.Generic;
using System;


namespace Skills
{
    public class SkillTree : MonoBehaviour
    {
        public static SkillTree Instance;

        private HashSet<string> unlockedUpgrades = new HashSet<string>();

        
        private string currentSelectedUpgrade;

        private Dictionary<string, string> selectedUpgrades = new();

        public bool IsCurrentlySelected(string category, string id)
        {
            return selectedUpgrades.TryGetValue(category, out var selected) && selected == id;
        }

        public void SelectUpgrade(string category, string id)
        {
            selectedUpgrades[category] = id;
        }

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
        public bool Unlockable(string id) => nodes[id].Prereqs.All(p => p.IsUnlocked) && nodes[id].cost <= PlayerSkills.Instance.coinCount;

        private void InitializeTree()
        {
            // this is the instantiator
            // there are a lot of things we could do to clean this up...
            nodes = new Dictionary<string, SkillNode>();
            SkillNode default_node = new SkillNode(0, "", new List<SkillNode>(), _applyEffect: new NotImplemented());
            SkillNode d_node = new SkillNode(300, "Speed1", new List<SkillNode>(), false, 3, new DashMult());
            nodes.Add("Speed1", d_node);
            SkillNode s_node = new SkillNode(400, "Speed2", new List<SkillNode> { d_node }, false, 1, new SpeedMult());
            nodes.Add("Speed2", s_node);
            SkillNode h_node = new SkillNode(500, "Speed3", new List<SkillNode> { s_node }, false, 1, new OxygenUp());
            nodes.Add("Speed3", h_node);

            SkillNode gun1 = new SkillNode(300, "Weapon1", new List<SkillNode>(), _applyEffect: new IncreaseGun());
            nodes.Add("Weapon1", gun1);
            SkillNode gun2 = new SkillNode(1000, "Weapon2", new List<SkillNode> { gun1 }, _applyEffect: new IncreaseGun());
            nodes.Add("Weapon2", gun2);
            SkillNode gun3 = new SkillNode(5000, "Weapon3", new List<SkillNode> { gun1, gun2 }, _applyEffect: new IncreaseGun());
            nodes.Add("Weapon3", gun3);

            SkillNode general1 = new SkillNode(300, "Oxygen1", new List<SkillNode>(), _max: 1, _applyEffect: new Flashlight());
            nodes.Add("Oxygen1", general1);
            SkillNode ox2 = new SkillNode(600, "Oxygen2", new List<SkillNode> { general1 }, _max: 1, _applyEffect: new BulletSize());
            nodes.Add("Oxygen2", ox2);
            SkillNode hands = new SkillNode(200, "Oxygen3", new List<SkillNode>(), _max: 7, _applyEffect: new NewHand());
            nodes.Add("Oxygen3", hands);

        }
        public void Unlock(string id)
        {
            if (!nodes.ContainsKey(id)) return;

            var node = nodes[id];
            if (Unlockable(id) && node.maxCt > node.appCt)
            {
                node.IsUnlocked = true;
                node.appCt++;
                node.ApplyEffect.Apply();
                PlayerSkills.Instance.ChangeCoins(node.cost);

                string category = new string(id.TakeWhile(char.IsLetter).ToArray());
                SelectUpgrade(category, id);
            }
            
        }
        
        public bool IsUnlocked(string id)
        {
            if (!nodes.ContainsKey(id)) return false;
            return nodes[id].IsUnlocked;
        }
    }

    public class SkillNode
    {
        public int cost;
        public string ID;
        public List<SkillNode> Prereqs = new();
        public bool IsUnlocked;
        public ISkillEffect ApplyEffect;
        // the number of times a player has unlocked this
        public int appCt = 0;
        // the maximum number of times a player can unlock this
        public int maxCt;
        public SkillNode(int _cost, string _id, List<SkillNode> _pre, bool _unlocked = false, int _max = 1, ISkillEffect _applyEffect = null)
        {
            cost = _cost;
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

    public class OxygenUp : ISkillEffect
    {
        public void Apply()
        {
            PlayerSkills.Instance.roundDurationBonus += 20f;
            Debug.Log("Added 20s bonus. Total bonus: " + PlayerSkills.Instance.roundDurationBonus);
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
                skills.SwapIndex(nextIndex);


                var bulletUI = GameObject.FindObjectOfType<BulletBarUI>();
                if (bulletUI != null)
                {
                    int playerMaxAmmo = skills.GetCurrentGunMaxAmmo();
                    bulletUI.SetTotalAmmo(playerMaxAmmo);
                }
                else
                {
                    Debug.LogWarning("BulletBarUI not found in scene");
                }

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
            PlayerSkills.Instance.roundDurationBonus += 20f;
            Debug.Log("Added 20s bonus. Total bonus: " + PlayerSkills.Instance.roundDurationBonus);
        }
    }

    public class NewHand : ISkillEffect
    {
        public void Apply()
        {
            PlayerSkills.Instance.hands++;
        }
    }

    public class Flashlight : ISkillEffect
    {
        public void Apply()
        {
            PlayerSkills.Instance.goggles = true;
        }
    }

    public class BulletSize : ISkillEffect
    {
        public void Apply()
        {
            PlayerSkills.Instance.bulletScale = 5f;
        }
    }
    
    
}
