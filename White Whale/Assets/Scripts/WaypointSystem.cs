using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DistantLands
{
    public class WaypointSystem : MonoBehaviour

    {

        public Transform player;
        public static bool attackPlayer = false;

        public Transform objectToMove;
        public Rigidbody rb;
        public float moveSpeed;
        public float turnSpeed;
        public float attackSpeed;
        public float attackdist = 10f;

        public Transform target;
        private int progress;
        private List<Transform> waypoints;


        // Start is called before the first frame update
        void Start()
        {

            waypoints = GetComponentsInChildren<Transform>().ToList();
            waypoints.Remove(transform);
            target = waypoints[progress];
            attackPlayer = false;

        }

        // Update is called once per frame
        void Update()
        {
            if (objectToMove.tag == "Whale")
            {
                float distanceToPlayer = Vector3.Distance(objectToMove.position, player.position);

                if (distanceToPlayer < attackdist && attackPlayer == false)
                {   
                    attackPlayer = true;
                    Debug.Log("Whale is coming for you!!!!");
                }
            }

            if (attackPlayer)
            {
                target = player;
                moveSpeed = attackSpeed;
            }

            if (objectToMove.tag == "Whale")
            {
                objectToMove.position += -objectToMove.forward * moveSpeed * Time.deltaTime;

                Vector3 lookDir = -(target.position - objectToMove.position).normalized;
                objectToMove.rotation = Quaternion.RotateTowards(
                objectToMove.rotation,
                Quaternion.LookRotation(lookDir, Vector3.up),turnSpeed * Time.deltaTime);
            }

            else
            {
                objectToMove.position += objectToMove.forward * moveSpeed * Time.deltaTime;
                objectToMove.rotation = Quaternion.RotateTowards(objectToMove.rotation, Quaternion.LookRotation(target.position - objectToMove.position, Vector3.up), turnSpeed * Time.deltaTime);
            }
      

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

        private void OnDrawGizmosSelected()
        {
            if (objectToMove)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(objectToMove.position, attackdist); // 10f is your detection radius
            }
        }
    }
}