using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class CharacterMovementControler : MonoBehaviour
{
    [SerializeField, Range(0f, 10f)]
    private float _speed = 1f;

    [SerializeField, Range(1f, 5f)]
    private float _sprintMultiplier = 2f;

    [SerializeField, Range(0f, 45f)]
    private float _rotationSpeed = 1f;

    public Vector3 MovementDirect { get; set; }
    public Vector3 LookDirect { get; set; }

    private CharacterController _characterController;
    private float _currentSpeedMultiplier = 1f;       // множитель спринта (1 или _sprintMultiplier)
    private float _externalSpeedMultiplier = 1f;      // множитель от бонусов/способностей
    private Coroutine _resetSpeedCoroutine;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Translate();
        Rotate();
    }

    private void Translate()
    {
        if (MovementDirect != Vector3.zero)
        {
            float delta = _speed * _currentSpeedMultiplier * _externalSpeedMultiplier * Time.deltaTime;
            _characterController.Move(MovementDirect * delta);
        }
    }

    public void SetSprinting(bool isSprinting)
    {
        _currentSpeedMultiplier = isSprinting ? _sprintMultiplier : 1f;
    }

    /// <summary>
    /// Применяет временный множитель скорости.
    /// Если эффект уже активен, он сбрасывается и заменяется новым.
    /// </summary>
    public void ApplyTemporarySpeedBoost(float multiplier, float duration)
    {
        if (_resetSpeedCoroutine != null)
            StopCoroutine(_resetSpeedCoroutine);

        _externalSpeedMultiplier = multiplier;
        _resetSpeedCoroutine = StartCoroutine(ResetSpeedBoost(duration));
    }

    private IEnumerator ResetSpeedBoost(float duration)
    {
        yield return new WaitForSeconds(duration);
        _externalSpeedMultiplier = 1f;
        _resetSpeedCoroutine = null;
    }

    private void Rotate()
    {
        if (LookDirect != Vector3.zero)
        {
            var targetRotation = Quaternion.LookRotation(LookDirect);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
        }
    }
}