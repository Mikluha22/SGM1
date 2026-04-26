using UnityEngine;

[RequireComponent(typeof(CharacterMovementControler))]
[RequireComponent(typeof(ShooterController))]
public abstract class BaseCharacter : MonoBehaviour
{
    [SerializeField] private Wp _baseWpPrefab;
    [SerializeField] private Transform _hand;
    [SerializeField] private float _heath = 10f;

    private CharacterMovementControler _characterMovementControler;
    private ShooterController _shooterController;

    protected void Awake()
    {
        _characterMovementControler = GetComponent<CharacterMovementControler>();
        _shooterController = GetComponent<ShooterController>();
    }

    protected void Start()
    {
        SetWp(_baseWpPrefab);
    }

    protected void Update()
    {
        var direct = GetMovementDirect();
        var lookdirect = direct;
        if (_shooterController.HasTarget)
        {
            lookdirect = _shooterController.TargetPosition - transform.position;
            lookdirect.y = 0f;
            lookdirect.Normalize();
        }

        _characterMovementControler.MovementDirect = direct;
        _characterMovementControler.LookDirect = lookdirect;

        if (_heath <= 0f)
            Destroy(gameObject);
    }

    public void SetWp(Wp wpPrefab)
    {
        _shooterController.SetWp(wpPrefab, _hand);
    }

    /// <summary> Применить временное ускорение от подбираемого бонуса. </summary>
    public void ApplySpeedBoost(float multiplier, float duration)
    {
        _characterMovementControler.ApplyTemporarySpeedBoost(multiplier, duration);
    }

    protected abstract Vector3 GetMovementDirect();

    protected void OnTriggerEnter(Collider other)
    {
        if (LayerUnitl.IsBullet(other.gameObject))
        {
            var bullet = other.GetComponent<Bullet>();
            _heath -= bullet.Damage;
            Destroy(other.gameObject);
        }
        else if (LayerUnitl.IsPickUp(other.gameObject))
        {
            var pickUp = other.GetComponent<PickUpItem>();
            pickUp.PickUp(this);
            Destroy(pickUp.gameObject);
        }
    }

    protected void OnDrawGizmos()
    {
        var lastColor = Gizmos.color;
        Gizmos.color = Color.red;
        Gizmos.DrawCube(_hand.position, new Vector3(0.2f, 0.2f, 0.2f));
        Gizmos.color = lastColor;
    }
}