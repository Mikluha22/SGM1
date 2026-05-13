using System;
using UnityEngine;

public abstract class PickUpItem : MonoBehaviour
{
    public event Action<PickUpItem> OnPickUp;

    public virtual void PickUp(BaseCharacter character)
    {
        OnPickUp?.Invoke(this);
        
    }
}
