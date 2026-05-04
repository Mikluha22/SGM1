using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterMovementControler))]
[RequireComponent(typeof(ShooterController))]
public abstract class BaseCharacter : MonoBehaviour
{
    [SerializeField] private Wp _baseWpPrefab;
    [SerializeField] private Transform _hand;
    [SerializeField] private Animator _animator;
    [SerializeField] protected float _heath = 10f;
    [SerializeField] protected float _maxHealth = 10f;      // добавлено

    protected CharacterMovementControler _characterMovementControler; // изменён доступ
    private ShooterController _shooterController;

    protected bool _hasNonDefaultWeapon { get; private set; } // добавлено

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
        if (_heath <= 0f)
            Die();
    }

    private bool _isDead = false;

    private void Die()
    {
        if (_isDead) return;
        _isDead = true;

        // отключаем управление, коллизию и т.п.
        _characterMovementControler.MovementDirect = Vector3.zero;
        GetComponent<Collider>().enabled = false;
        // можно отключить стрельбу: GetComponent<ShooterController>().enabled = false;

        _animator.SetTrigger("Die");
        StartCoroutine(DestroyAfterDeathAnimation());
    }

    private IEnumerator DestroyAfterDeathAnimation()
    {
        yield return null; // ждём кадр, чтобы аниматор обработал переход
        float length = _animator.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(length);
        Destroy(gameObject);
    }

    public void SetWp(Wp wpPrefab)
    {
        _shooterController.SetWp(wpPrefab, _hand);
        // Если полученное оружие отличается от базового, помечаем как заменённое
        if (wpPrefab != _baseWpPrefab)
            _hasNonDefaultWeapon = true;
    }

    public float GetHealthPercent()
    {
        return _heath / _maxHealth;
    }

    // ... остальные методы без изменений
    public void SetMovementParams()
    {
        var direct = GetMovementDirect();
        _characterMovementControler.MovementDirect = direct;
        _characterMovementControler.LookDirect = direct;

        _animator.SetBool("IsMoving", direct != Vector3.zero);
        _animator.SetBool("IsShooting", _shooterController.HasTarget);
    }

    public bool HasShooterTarget() => _shooterController.HasTarget;

    public void OverrideLookDirect()
    {
        var lookdirect = _shooterController.TargetPosition - transform.position;
        lookdirect.y = 0f;
        lookdirect.Normalize();
        _characterMovementControler.LookDirect = lookdirect;
    }

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