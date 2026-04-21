using UnityEngine;
[RequireComponent(typeof(CharacterController))]
public class CharacterMovementControler : MonoBehaviour
{
    [SerializeField, Range(0f, 10f)]
    private float _speed = 1f;

    [SerializeField, Range(1f, 5f)]
    private float _sprintMultiplier = 2f;

    [SerializeField, Range(0f, 45f)]
    private float _rotationspeed = 1f;

    public Vector3 MovementDirect { get; set; }
    public Vector3 LookDirect { get; set; }

    private CharacterController _characterController;

    private float _timer = 0f;
    private float _currentSpeedMultiplier = 1f;
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
            var delta = MovementDirect * (_speed * _currentSpeedMultiplier * Time.deltaTime);
            _characterController.Move(delta);
        }
    }

    // Called by input/controller to enable or disable sprinting
    public void SetSprinting(bool isSprinting)
    {
        _currentSpeedMultiplier = isSprinting ? _sprintMultiplier : 1f;
    }

    private void Rotate()
    {
        if (LookDirect != Vector3.zero)
        {
            var targetRotation = Quaternion.LookRotation(LookDirect);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _rotationspeed * Time.deltaTime);
        }

    }
}