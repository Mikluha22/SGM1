using UnityEngine;


[RequireComponent(typeof(PlayerInputController))]
[RequireComponent(typeof(JoystikPlayerInputController))]
public class PlayerController : BaseCharacter
{
    private PlayerInputController _pcInput;
    private JoystikPlayerInputController _mobileInput;

    protected void Awake()
    {
        base.Awake();
        _pcInput = GetComponent<PlayerInputController>();
        _mobileInput = GetComponent<JoystikPlayerInputController>();

#if UNITY_STANDALONE || UNITY_EDITOR
        _pcInput.enabled = true;
        _mobileInput.enabled = false;
#else
        _pcInput.enabled = false;
        _mobileInput.enabled = true;
#endif
    }

    protected override Vector3 GetMovementDirect()
    {
#if UNITY_STANDALONE || UNITY_EDITOR
        return _pcInput.MovementDirect;
#else
        return _mobileInput.MovementDirect;
#endif
    }
}