using UnityEngine;
using UnityEngine.AI;

public class EnemyController : BaseCharacter
{
    [SerializeField] private float _viewRadius;
    [Header("Flee Settings")]
    [SerializeField, Range(0f, 1f)] private float _fleeHealthPercent = 0.3f;
    [SerializeField, Range(0f, 1f)] private float _fleeChance = 0.7f;
    [SerializeField] private float _fleeSpeedMultiplier = 1.5f;

    public GameObject Target { get; private set; }
    private Vector3 _movementDirect;
    private PlayerController _player;
    private NavMesher _navMesher;

    private readonly Collider[] _colliders = new Collider[10];
    private bool _isFleeing;

    protected new void Awake()
    {
        _navMesher = new NavMesher();
        base.Awake();
        _player = FindAnyObjectByType<PlayerController>();
    }

    protected override Vector3 GetMovementDirect() => _movementDirect;

    public void SetIdle()
    {
        _movementDirect = Vector3.zero;
    }

    public void SetDirectMove()
    {
        if (Target == null) return;
        var direct = Target.transform.position - transform.position;
        direct.y = 0;
        _movementDirect = direct.normalized;
    }

    public void SetPathFinding()
    {
        if (Target == null) return;
        if (_navMesher.IsPathCalculated)
        {
            if (_navMesher.NeedRecalculatePath(Target.transform.position, 1f))
                _navMesher.CalculatePath(transform.position, Target.transform.position);
        }
        else
        {
            _navMesher.CalculatePath(transform.position, Target.transform.position);
        }

        var pathPoint = _navMesher.GetCurrentPoint(transform.position);
        var direct = pathPoint - transform.position;
        direct.y = 0;
        _movementDirect = direct.normalized;
    }

    /// <summary> Движение прочь от цели (побег). </summary>
    public void SetFleeDirection()
    {
        if (Target == null) return;
        var fleeDir = transform.position - Target.transform.position;
        fleeDir.y = 0;
        _movementDirect = fleeDir.normalized;
    }

    /// <summary> Начать побег: включить ускорение. </summary>
    public void BeginFlee()
    {
        if (!_isFleeing)
        {
            _isFleeing = true;
            _characterMovementControler.SetStateSpeedMultiplier(_fleeSpeedMultiplier);
        }
    }

    /// <summary> Закончить побег: убрать ускорение. </summary>
    public void EndFlee()
    {
        if (_isFleeing)
        {
            _isFleeing = false;
            _characterMovementControler.ClearStateSpeedMultiplier();
        }
    }

    /// <summary> Проверка условия для перехода в побег. </summary>
    public bool ShouldFlee()
    {
        float healthPercent = _heath / _maxHealth;
        if (healthPercent >= _fleeHealthPercent)
            return false;
        return Random.value < _fleeChance;
    }

    public float GetHealthPercent()
    {
        return _heath / _maxHealth;
    }

    public void FindClosest()
    {
        Target = null;
        // Если оружие всё ещё базовое (пистолет) – приоритет у подбираемого оружия
        if (!_hasNonDefaultWeapon)
        {
            Target = FindClosestPickup();
            if (Target != null) return;   // нашёлся пикап – он и есть цель
        }

        // Иначе (или если пикапов нет) ищем среди персонажей
        Target = FindClosestCharacter();
    }

    private GameObject FindClosestPickup()
    {
        GameObject best = null;
        float minDist = float.MaxValue;
        int count = Physics.OverlapSphereNonAlloc(transform.position, _viewRadius, _colliders, LayerUnitl.PickUpMask);
        for (int i = 0; i < count; i++)
        {
            var go = _colliders[i].gameObject;
            float dist = (go.transform.position - transform.position).sqrMagnitude;
            if (dist < minDist)
            {
                minDist = dist;
                best = go;
            }
        }
        return best;
    }

    private GameObject FindClosestCharacter()
    {
        GameObject best = null;
        float minDist = float.MaxValue;
        int count = Physics.OverlapSphereNonAlloc(transform.position, _viewRadius, _colliders, LayerUnitl.ShooterTargetMask);
        for (int i = 0; i < count; i++)
        {
            var go = _colliders[i].gameObject;
            if (go == gameObject) continue;
            float dist = (go.transform.position - transform.position).sqrMagnitude;
            if (dist < minDist)
            {
                minDist = dist;
                best = go;
            }
        }
        return best;
    }

    public float DistanceTo(GameObject target)
    {
        if (target == null) return float.MaxValue;
        return (target.transform.position - transform.position).magnitude;
    }

    protected new void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        if (_navMesher != null && _navMesher.IsPathCalculated)
            _navMesher.DrawGizmos();
    }
}
 