using UnityEngine;

public class PickUpSpeedBoost : PickUpItem
{
    [SerializeField, Tooltip("Множитель скорости на время действия бонуса")]
    private float _speedMultiplier = 2f;

    [SerializeField, Tooltip("Длительность ускорения в секундах")]
    private float _duration = 5f;

    public override void PickUp(BaseCharacter character)
    {
        base.PickUp(character);
        character.ApplySpeedBoost(_speedMultiplier, _duration);
    }
}