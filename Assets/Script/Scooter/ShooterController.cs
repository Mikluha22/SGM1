using UnityEngine;


public class ShooterController : MonoBehaviour
{
    public bool HasTarget => _target != null;
    public Vector3 TargetPosition
    {
        get
        {
            if (HasTarget)
                return _target.transform.position;

            return Vector3.zero;
        }
    }

    private Wp _wp;
    private float _nextShotTimerSecond;
    private GameObject _target = null;
    private Collider[] _colliders = new Collider[2];

    private void Update()
    {
        _target= GetTarget();

        _nextShotTimerSecond -= Time.deltaTime;
        if (_nextShotTimerSecond <= 0f)
        {
            if (HasTarget)
             _wp.Shoot(_target.transform.position);
            
           
            _nextShotTimerSecond = _wp.ShootFroquencySeconds;
        }
        
    }
    public void SetWp(Wp wpPrefab, Transform hand)
    { 
        _wp=Instantiate(wpPrefab, hand);
        _wp.transform.localPosition= Vector3.zero;
        _wp.transform.localRotation= Quaternion.identity;

    }

    private GameObject GetTarget()
    { 
        GameObject target = null;

        var position = _wp.transform.position;
        var radius = _wp.ShootRadios;
        string layerToFind = gameObject.layer == LayerUnitl.EnemyLayer ? LayerUnitl.PlayerLayerName : LayerUnitl.EnemyLayerName;
        var mask = LayerMask.GetMask(layerToFind);

        var size = Physics.OverlapSphereNonAlloc(position, radius, _colliders, mask);
        if (size > 0)
        {
            target = _colliders[0].gameObject;
        }

        return target;
    }

    private void OnDrawGizmos()
    {
        if (_wp != null)
        {
            var lastColor = Gizmos.color;

            
            var position = _wp.transform.position;
            var radius = _wp.ShootRadios;
            UnityEditor.Handles.color = Color.blue;
            UnityEditor.Handles.DrawWireDisc(position, Vector3.up, radius);

            Gizmos.color = lastColor;
        }
        
    }
}
