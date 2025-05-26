
using UnityEngine;
using DistantLands; 

public class SharkAI : EnemyAI
{
    public WaypointSystem waypointSystem;
    public Transform playerTarget;
    public float speed = 5f;

    protected override void Start()
    {
        base.Start();
        // Start in patrol state
        ChangeState(new SharkPatrol(this));
    }
    protected override void Update()
    {
        base.Update();  

    }

    public override void Damage(float amount)
    {
        Debug.Log("Shark took damage: " + amount);
        
        // add health reduction stuff later
        if (!IsAggro)
        {
            ChangeState(new SharkAttack(this));
        }
    }
}
