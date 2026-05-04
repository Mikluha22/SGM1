using UnityEngine;

public class CharacterSpawnZone : MonoBehaviour
{
    [SerializeField] private float _radius = 2f;
    [SerializeField] private Color _gizmoColor = new Color(0, 1, 0, 0.3f);

    public Vector3 Position => transform.position;
    public float Radius => _radius;

    private void OnDrawGizmos()
    {
        // Центр зоны
        Gizmos.color = _gizmoColor;
        Gizmos.DrawSphere(transform.position, 0.2f);

        // Граница зоны
        UnityEditor.Handles.color = _gizmoColor;
        UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.up, _radius);
    }
}
