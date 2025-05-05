using UnityEngine;

//make the spawn points visible for eaiser editing

public class SpawnPointGizmo : MonoBehaviour
{
    public Color gizmoColor = Color.cyan;
    public float radius = 0.3f;

    private void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;
        Gizmos.DrawSphere(transform.position, radius);
    }
}