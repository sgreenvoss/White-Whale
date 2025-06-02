
using UnityEngine;
using System.Collections;
using UnityEngine.UIElements;

namespace DistantLands
{
    public class EnemyFish : ABSFish
    {
        public UIManager uiManager; // UIManager in inspector

        public static bool WhaleCaught = false;

        public bool caught = false;

        protected override void Start()
        {
            base.Start();

        }




        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                // Decrease score
                ABSFish.total_score = Mathf.Max(0, ABSFish.total_score - 10); // prevent score from going below 0

                uiManager.HandleFishScore(null);



                Debug.Log("Shark bite!");
                Rigidbody playerRb = collision.gameObject.GetComponent<Rigidbody>();
                if (playerRb != null)
                {
                    Debug.Log("player rb not found");
                    Vector3 knockbackDir = (collision.transform.position - transform.position).normalized;

                    float knockbackForce = 60f;
                    rb.AddForce(-knockbackDir * knockbackForce, ForceMode.Impulse); // Shark knockback
                    playerRb.AddForce(knockbackDir * knockbackForce, ForceMode.Impulse); // Player knockback
                }
            }
        }
        

        public override void Catch()
        {
  
            ABSFish.total_score += this.fish_score;
            
            caught = true;

            gameObject.SetActive(false); //deactivate inseatd of destroy
            Debug.Log("Fish caught");          
            
            if (this.tag == "Whale")
            {
                WhaleCaught = true;
                GameWon();
            }
        }

        void GameWon()
        {
            GameState.Instance.ChangeState(GState.EndRound);

            // convert score to total coins
            ABSFish.total_coins += ABSFish.score * 10;

            GameEvents.RoundEnded(); // Notify all subscribed observers

        }

    }
}
