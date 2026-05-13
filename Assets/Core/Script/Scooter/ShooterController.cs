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

    private Wp _wp = null;
    private float _nextShotTimerSecond;
    private GameObject _target = null;
    private Collider[] _colliders = new Collider[16];

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
        if (_wp != null)
            Destroy(_wp.gameObject);
        _wp =Instantiate(wpPrefab, hand);
        _wp.transform.localPosition= Vector3.zero;
        _wp.transform.localRotation= Quaternion.identity;

    }

    private GameObject GetTarget()
    { 
        GameObject target = null;
        if (_wp == null)
            return null;

        var position = _wp.transform.position;
        var radius = _wp.ShootRadios;

        // If this object is an Enemy, search for Players and other Enemies
        // so enemies can shoot the player and each other. Otherwise (Player)
        // search for Enemies.
        int mask;
        if (gameObject.layer == LayerUnitl.EnemyLayer)
        {
            mask = LayerMask.GetMask(LayerUnitl.PlayerLayerName, LayerUnitl.EnemyLayerName);

            GameObject playerFound = null;
            GameObject otherFound = null;

            var size = Physics.OverlapSphereNonAlloc(position, radius, _colliders, mask);
            for (int i = 0; i < size; i++)
            {
                var go = _colliders[i].gameObject;
                if (go == gameObject) // ignore self
                    continue;

                if (go.layer == LayerUnitl.PlayerLayer)
                {
                    playerFound = go;
                    break; // priority to player
                }

                // remember other (enemy) target if player not present
                if (otherFound == null)
                    otherFound = go;
            }

            target = playerFound ?? otherFound;
        }
        else
        {
            mask = LayerMask.GetMask(LayerUnitl.EnemyLayerName);
            var size = Physics.OverlapSphereNonAlloc(position, radius, _colliders, mask);
            for (int i = 0; i < size; i++)
            {
                var go = _colliders[i].gameObject;
                if (go != gameObject) // ignore self
                {
                    target = go;
                    break;
                }
            }
        }

        return target;
    }


    private void OnDrawGizmos()
    {
#if UNITY_EDITOR
        if (_wp != null)
        {
            var lastColor = Gizmos.color;

            var position = _wp.transform.position;
            var radius = _wp.ShootRadios;
            UnityEditor.Handles.color = Color.blue;
            UnityEditor.Handles.DrawWireDisc(position, Vector3.up, radius);

            Gizmos.color = lastColor;
        }
#endif
    }
}
