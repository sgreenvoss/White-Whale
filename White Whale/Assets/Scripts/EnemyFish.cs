
using UnityEngine;
using System.Collections;
using UnityEngine.UIElements;

namespace DistantLands
{
    public class EnemyFish : ABSFish
    {
        public UIManager uiManager; // UIManager in inspector

        public ParticleSystem playerHitParticle;

        public static bool WhaleCaught = false;

        public static bool youDied;

        public bool caught = false;

        public float playerHealth = 6f;

        [SerializeField] private AudioClip[] fishAttack;



        protected override void Start()
        {
            base.Start();
            youDied = false;
            WhaleCaught = false;

        }




        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                // // Play player hit particle effect
                // playerHitParticle = collision.gameObject.GetComponentInChildren<ParticleSystem>();
                // playerHitParticle.Play();
                // CameraShake.instance.start = true;
                

                // Decrease score
                ABSFish.total_score = Mathf.Max(0, ABSFish.total_score - 10); // prevent score from going below 0

                uiManager.HandleFishScore(null);
                AudioManager.instance.PlayRandomSoundClip(fishAttack, transform, 1f);



                Debug.Log("Shark bite!");
                Rigidbody playerRb = collision.gameObject.GetComponent<Rigidbody>();
                if (playerRb != null)
                {

                    Vector3 knockbackDir = (collision.transform.position - transform.position).normalized;


                    float knockbackForce = (this.tag == "Whale") ? 90f : 60f;
                    rb.AddForce(-knockbackDir * knockbackForce, ForceMode.Impulse); // enemy knockback
                    playerRb.AddForce(knockbackDir * knockbackForce, ForceMode.Impulse); // Player knockback

                    if (this.tag == "Whale")
                    {
                        StartCoroutine(PauseForSeconds(1f));
                        playerHealth -= 1;
                        AudioManager.PitchShift(1.1f);
                        Debug.Log("health down");
                    }

                    if (playerHealth == 0)
                    {
                        WaypointSystem.attackPlayer = false;
                        youDied = true;
                        // GameWon();

                    }
                }
            }
        }

        private IEnumerator PauseForSeconds(float duration)
        {
            WaypointSystem.isPaused = true;
            yield return new WaitForSeconds(duration);
            WaypointSystem.isPaused = false;
        }
        

        public override void Catch()
        {
  
            ABSFish.total_score += this.fish_score;

            uiManager.HandleFishScore(null);
            
            caught = true;

            gameObject.SetActive(false); //deactivate inseatd of destroy
            Debug.Log("Fish caught");          
            
            if (this.tag == "Whale")
            {
                WhaleCaught = true;
                // GameWon();
            }
        }

        // void GameWon()
        // {
        //     GameState.Instance.ChangeState(GState.EndRound);

        //     // convert score to total coins
        //     ABSFish.total_coins += ABSFish.score * 10;

        //     GameEvents.RoundEnded(); // Notify all subscribed observers

        // }


    }
}
