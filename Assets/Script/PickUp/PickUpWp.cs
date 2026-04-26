using UnityEngine;

public class PickUpWp : PickUpItem
{
    [SerializeField]
    private Wp _wpPrefab;

    public override void PickUp(BaseCharacter character)
    {
        base.PickUp(character);
        character.SetWp(_wpPrefab);
    }
}
