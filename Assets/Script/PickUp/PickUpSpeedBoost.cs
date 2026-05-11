using UnityEngine;

public class PickUpSpeedBoost : PickUpItem
{
    [SerializeField, Tooltip("Множитель скорости на время действия бонуса")]
    private float _speedMultiplier = 2f;

    [SerializeField, Tooltip("Длительность ускорения в секундах")]
    private float _duration = 5f;

    [SerializeField] private ParticleSystem activeEffectPrefab;

    public override void PickUp(BaseCharacter character)
    {
        base.PickUp(character);
        if (activeEffectPrefab != null)
        {
            ParticleSystem effect = Instantiate(activeEffectPrefab, character.transform.position, Quaternion.identity, character.transform);
            Destroy(effect.gameObject, _duration);
        }
        character.ApplySpeedBoost(_speedMultiplier, _duration);
    }
}