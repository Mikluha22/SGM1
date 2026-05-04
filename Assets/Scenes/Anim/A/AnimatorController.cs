using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class animatorController : MonoBehaviour
{
    [SerializeField] 
    private Animator _animator;

    protected void Start()
    {
        
        _animator.SetFloat("Speed", 1.0f);
       
    }

    protected  void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _animator.SetTrigger("Jump");
        }
    }
}
