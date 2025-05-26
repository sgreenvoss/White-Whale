
using UnityEngine;

public class SharkAttack : EnemyState
{
    private SharkAI shark;

    public SharkAttack(SharkAI shark) : base(shark)
    {
        this.shark = shark;
    }

    public override void Enter()
    {
        Debug.Log("Shark entered attack state");
        if (shark.waypointSystem != null)
            shark.waypointSystem.enabled = false; // stop patrol movement
    }

    public override void Update()
    {   

        Vector3 direction = shark.playerTarget.position - shark.transform.position;

        shark.transform.position = Vector3.MoveTowards(
            shark.transform.position,
            shark.playerTarget.position,
            shark.speed * Time.deltaTime
        );
}

    public override void Exit()
    {
        Debug.Log("Shark exiting attack state");
    }
}
