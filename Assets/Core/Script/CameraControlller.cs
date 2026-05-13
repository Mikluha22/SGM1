using System;
using UnityEngine;

public class CameraControlller : MonoBehaviour
{
    [SerializeField] 
    private Vector3 _followCameraOffset = Vector3.zero;

    [SerializeField]
    private Vector3 _rotationOffset = Vector3.zero;


    [SerializeField]
    private Transform _target;

    private void Start()
    {
        if (_target == null)
        {
            var player = FindAnyObjectByType<PlayerController>();
            if (player != null)
                _target = player.transform;
        }
    }

    private void LateUpdate()
    {
        Vector3 targetRotation = _rotationOffset - _followCameraOffset;

        transform.position = _target.position + _followCameraOffset; 
        transform.rotation = Quaternion.LookRotation(targetRotation,Vector3.up);
    }


}
