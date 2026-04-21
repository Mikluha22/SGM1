using UnityEngine;

[RequireComponent(typeof(PlayerInputController))]
public class PlayerController : BaseCharacter
{
    private PlayerInputController _playerInputController;
   
    protected void Awake()
    {
        base.Awake();
        _playerInputController = GetComponent<PlayerInputController>();
        
    }

    protected override Vector3 GetMovementDirect()
    {
        return _playerInputController.MovementDirect;
    }


}
