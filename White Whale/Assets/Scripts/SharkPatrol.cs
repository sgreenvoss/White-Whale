
using UnityEngine;

public class SharkPatrol : EnemyState
{
    private SharkAI shark;

    public SharkPatrol(SharkAI shark) : base(shark)
    {
        this.shark = shark;
    }

    public override void Enter()
    {
        Debug.Log("Shark entered patrol state");
        if (shark.waypointSystem != null)
            shark.waypointSystem.enabled = true; // enable patrol movement
    }

    public override void Update()
    {
        // Patrol movement handled by waypointSystem script
    }

    public override void Exit()
    {
        Debug.Log("Shark exiting patrol state");
        if (shark.waypointSystem != null)
            shark.waypointSystem.enabled = false;
    }
}
