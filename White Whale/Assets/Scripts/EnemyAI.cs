using UnityEngine;

public abstract class EnemyAI : MonoBehaviour
{
    protected EnemyState currentState;

    public bool IsAggro { get; protected set; } = false;
    

    protected virtual void Start()
    {
        // No state at the start because waypoint system is it's own thing
    }

    protected virtual void Update()
    {
        currentState?.Update();
    }

    public void ChangeState(EnemyState newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState.Enter(); 
        IsAggro = newState.ToString().ToLower().Contains("attack");
    
        if (newState.ToString().ToLower().Contains("attack"))
        {
            IsAggro = true;
        }
        else
        {
            IsAggro = false;
        }
    }

    public abstract void Damage(float amount);
}
