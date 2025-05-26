using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DistantLands
{
    public class WaypointSystem : MonoBehaviour

    {

        public Transform player;
        public bool attackPlayer = false;

        public Transform objectToMove;
        public Rigidbody rb;
        public float moveSpeed;
        public float turnSpeed;

        public Transform target;
        private int progress;
        private List<Transform> waypoints;


        // Start is called before the first frame update
        void Start()
        {

            waypoints = GetComponentsInChildren<Transform>().ToList();
            waypoints.Remove(transform);
            target = waypoints[progress];

        }

        // Update is called once per frame
        void Update()
        {

            if (attackPlayer)
            {
                target = player;
                moveSpeed = 5f;
            }
        

            objectToMove.position += objectToMove.forward * moveSpeed * Time.deltaTime;
            objectToMove.rotation = Quaternion.RotateTowards(objectToMove.rotation, Quaternion.LookRotation(target.position - objectToMove.position, Vector3.up), turnSpeed * Time.deltaTime);

            if (!attackPlayer && Vector3.Distance(objectToMove.position, target.position) < moveSpeed)
                NextPoint();

        }


        public void NextPoint()
        {

            progress++;

            if (progress >= waypoints.Count)
                progress = 0;

            target = waypoints[progress];


        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                Debug.Log("Shark bite!");
                Rigidbody playerRb = collision.gameObject.GetComponent<Rigidbody>();
                if (playerRb != null)
                {
                    Vector3 knockbackDir = (collision.transform.position - transform.position).normalized;

                    float knockbackForce = 5f;
                    rb.AddForce(-knockbackDir * knockbackForce, ForceMode.Impulse); // Shark knockback
                    playerRb.AddForce(knockbackDir * knockbackForce, ForceMode.Impulse); // Player knockback
                }
            }
        }




        private void OnDrawGizmos()
        {


            for (int i = 0; i < transform.childCount; i++)
            {

                if (!transform.GetChild(i))
                    continue;

                Transform j = transform.GetChild(i);

                Gizmos.color = Color.green;
                Gizmos.DrawWireSphere(j.position, 0.5f);

                if (i < transform.childCount - 1)
                    Gizmos.DrawLine(j.position, transform.GetChild(i + 1).position);
                else
                    Gizmos.DrawLine(j.position, transform.GetChild(0).position);

            }
        }
    }
}