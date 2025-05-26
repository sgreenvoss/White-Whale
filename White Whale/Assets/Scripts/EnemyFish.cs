
using UnityEngine;
using System.Collections;
using UnityEngine.UIElements;

namespace DistantLands
{
    public class EnemyFish : ABSFish
    {
  
        // private float speed;
        // public float averageSpeed;
        public bool caught = false;
        

        //making independent variables
        // private Vector3 wanderDirection;
        // public float tankRadius = 10f;
        // public Vector3 tankCenter = Vector3.zero;
        // public float directionChangeInterval = 3f;

        // private float directionChangeTimer;



        // Use this for initialization
        protected override void Start()
        {
            base.Start();

            // speed = Random.Range(0.5f, 1.5f) * averageSpeed;
            // ChooseNewDirection();
            // directionChangeTimer = directionChangeInterval;

            // tankCenter = transform.position;
        }

        // Update is called once per frame
        void Update()
        {
            // directionChangeTimer -= Time.deltaTime;

            // if (directionChangeTimer <= 0)
            // {
            //     ChooseNewDirection();
            //     directionChangeTimer = directionChangeInterval;
            // }

            // if (Vector3.Distance(transform.position, tankCenter) > tankRadius)
            // {
            //     wanderDirection = (tankCenter - transform.position).normalized;
            // }

            // transform.rotation = Quaternion.Slerp(transform.rotation,
            //     Quaternion.LookRotation(wanderDirection),
            //     TurnSpeed() * Time.deltaTime);
            //     rb.MovePosition(rb.position + transform.forward * speed * Time.deltaTime);
                //smoothly rotates fish

        }

        

        // void ChooseNewDirection()
        // {
        //     float yaw = Random.Range(0f, 360f);
        //     //left and right
        //     float pitch = Random.Range(-20f, 20f);
        //     //up and down
        //     Quaternion randomRotation = Quaternion.Euler(pitch, yaw, 0);
        //     wanderDirection = randomRotation * Vector3.forward;
        // }

        

        // float TurnSpeed()
        // {
        //     return Random.Range(0.2f, .4f) * speed;
        // }


        // void OnDrawGizmos()
        // {
        //     // Set the color of the Gizmos (green in this case)
        //     Gizmos.color = Color.red;

        //     // Draw a wire sphere to represent the tank radius
        //     Gizmos.DrawWireSphere(tankCenter, tankRadius);
        // }

        // private void OnCollisionEnter(Collision collision)
        // {
        //     ChooseNewDirection();
        //     directionChangeTimer = directionChangeInterval;
        //     // quick fix for ground
        //     if (collision.gameObject.CompareTag("Ground"))
        //     {
        //         transform.Translate(0f, 1f, 0f);
        //     }
        // }

        public override void Catch()
        {
            ABSFish.total_score += this.fish_score;
            // FishManager fishManager = FindObjectOfType<FishManager>();
            // fishManager?.CatchFish(this);

            
            caught = true;

            gameObject.SetActive(false); //deactivate inseatd of destroy
            Debug.Log("Fish caught");
        }

        // public void ResetFish(Vector3 newPosition)
        // {
        //     //When a fish gets respawned reset its state
        //     transform.position = newPosition;
        //     tankCenter = newPosition;
        //     speed = Random.Range(0.5f, 1.5f) * averageSpeed;
        //     ChooseNewDirection();
        //     directionChangeTimer = directionChangeInterval;
        //     caught = false;
        //     gameObject.SetActive(true);
        // }
    }
}
