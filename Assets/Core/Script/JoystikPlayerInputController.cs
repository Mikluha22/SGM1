using UnityEngine;

public class JoystikPlayerInputController : MonoBehaviour
{
    private Camera _camera;
    private Joystick _joystick;
    private CharacterMovementControler _characterMovementControler;


    public Vector3 MovementDirect {  get; private set; }

    private void Awake()
    {
        _joystick= FindObjectOfType<Joystick>();
        _camera = Camera.main;
        _characterMovementControler = GetComponent<CharacterMovementControler>();
    }
    void Update()
    {
        var horizontal = _joystick.Horizontal;
        var vertical = _joystick.Vertical;

        var direct = new Vector3(horizontal,0f, vertical);
        direct = _camera.transform.rotation * direct;
        direct.y = 0f;

        MovementDirect = direct.normalized;

        // Sprint when space is held down
        bool isSprinting = Input.GetKey(KeyCode.Space);
        if (_characterMovementControler != null)
            _characterMovementControler.SetSprinting(isSprinting);
    }
}
