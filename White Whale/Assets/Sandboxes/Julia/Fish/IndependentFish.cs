using UnityEngine;
using System.Collections;

namespace DistantLands
{
    public class IndependentFish : MonoBehaviour
    {

        private float speed;
        public float averageSpeed;
        

        //making independent variables
        private Vector3 wanderDirection;
        public float tankRadius = 10f;
        public Vector3 tankCenter = Vector3.zero;
        public float directionChangeInterval = 3f;

        private float directionChangeTimer;



        // Use this for initialization
        void Start()
        {
            speed = Random.Range(0.5f, 1.5f) * averageSpeed;
            ChooseNewDirection();
            directionChangeTimer = directionChangeInterval;

            tankCenter = transform.position;
        }

        // Update is called once per frame
        void Update()
        {
            directionChangeTimer -= Time.deltaTime;

            if (directionChangeTimer <= 0)
            {
                ChooseNewDirection();
                directionChangeTimer = directionChangeInterval;
            }

            if (Vector3.Distance(transform.position, tankCenter) > tankRadius)
            {
                wanderDirection = (tankCenter - transform.position).normalized;
            }

            transform.rotation = Quaternion.Slerp(transform.rotation,
                Quaternion.LookRotation(wanderDirection),
                TurnSpeed() * Time.deltaTime);
                transform.Translate(0, 0, Time.deltaTime * speed);
                //smoothly rotates fish
        }

        

        void ChooseNewDirection()
        {
            float yaw = Random.Range(0f, 360f);
            //left and right
            float pitch = Random.Range(-20f, 20f);
            //up and down
            Quaternion randomRotation = Quaternion.Euler(pitch, yaw, 0);
            wanderDirection = randomRotation * Vector3.forward;
        }

        

        float TurnSpeed()
        {
            return Random.Range(0.2f, .4f) * speed;
        }


        void OnDrawGizmos()
        {
            // Set the color of the Gizmos (green in this case)
            Gizmos.color = Color.green;

            // Draw a wire sphere to represent the tank radius
            Gizmos.DrawWireSphere(tankCenter, tankRadius);
        }
    }
}