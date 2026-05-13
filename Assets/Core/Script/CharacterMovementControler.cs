using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class CharacterMovementControler : MonoBehaviour
{
    [SerializeField, Range(0f, 10f)] private float _speed = 1f;
    [SerializeField, Range(1f, 5f)] private float _sprintMultiplier = 2f;
    [SerializeField, Range(0f, 45f)] private float _rotationSpeed = 1f;

    public Vector3 MovementDirect { get; set; }
    public Vector3 LookDirect { get; set; }

    private CharacterController _characterController;
    private float _currentSpeedMultiplier = 1f;       // спринт
    private float _externalSpeedMultiplier = 1f;      // временный бонус
    private float _stateSpeedMultiplier = 1f;         // постоянный множитель состояния (побег)
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
            float delta = _speed * _currentSpeedMultiplier * _externalSpeedMultiplier * _stateSpeedMultiplier * Time.deltaTime;
            _characterController.Move(MovementDirect * delta);
        }
    }

    public void SetSprinting(bool isSprinting)
    {
        _currentSpeedMultiplier = isSprinting ? _sprintMultiplier : 1f;
    }

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

    /// <summary> Установить постоянный множитель скорости (например, при побеге). </summary>
    public void SetStateSpeedMultiplier(float multiplier)
    {
        _stateSpeedMultiplier = multiplier;
    }

    /// <summary> Сбросить постоянный множитель скорости. </summary>
    public void ClearStateSpeedMultiplier()
    {
        _stateSpeedMultiplier = 1f;
    }

    private void Rotate()
    {
        // без изменений
        if (LookDirect != Vector3.zero)
        {
            var targetRotation = Quaternion.LookRotation(LookDirect);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
        }
    }
}