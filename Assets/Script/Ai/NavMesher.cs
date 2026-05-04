using UnityEngine.AI;
using UnityEngine;

public class NavMesher
{
    private const float DistanceEps = 1f;
    public bool IsPathCalculated {  get; private set; }

    private readonly NavMeshQueryFilter _filter = new() { areaMask = NavMesh.AllAreas };

    private NavMeshPath _path = new NavMeshPath();
    private NavMeshHit _targetHit;
    private int _currentPathPointIndex;

    public void CalculatePath(Vector3 startPosition, Vector3 endPosition)
    {
        NavMesh.SamplePosition(startPosition, out var agentHit, 10f, _filter);
        NavMesh.SamplePosition(endPosition, out _targetHit, 10f, _filter);

        IsPathCalculated = NavMesh.CalculatePath(agentHit.position, _targetHit.position, _filter, _path);
        _currentPathPointIndex = 1;
    }

    public bool NeedRecalculatePath(Vector3 currentTargetPosition, float maxDistanse)
    {
        var distanse = (_targetHit.position - currentTargetPosition).magnitude;
        return distanse >= maxDistanse;

    }

    public Vector3 GetCurrentPoint(Vector3 position)
    {
        var currentPoint = _path.corners[_currentPathPointIndex];
        var distance = (position - currentPoint).magnitude;

        if (distance < DistanceEps)
            _currentPathPointIndex++;
       
        if (_currentPathPointIndex >= _path.corners.Length)
            IsPathCalculated = false;
        else
            currentPoint = _path.corners[_currentPathPointIndex];

        return currentPoint;
    }

    public void DrawGizmos()
    {
        var lastColor = Gizmos.color;
        Gizmos.color = Color.red;
        for (int i = 1; i < _path.corners.Length; i++)
        {
            Gizmos.DrawCube(_path.corners[i], Vector3.one * 0.5f);
            Gizmos.DrawLine(_path.corners[i - 1], _path.corners[i]);
        }

        Gizmos.color = Color.green;
        Gizmos.DrawCube(_path.corners[_currentPathPointIndex], Vector3.one * 0.5f);

        Gizmos.color = lastColor;
    }
}
