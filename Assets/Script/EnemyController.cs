using UnityEngine;

public class EnemyController : BaseCharacter
{
    protected override Vector3 GetMovementDirect()
    {
        // Enemy stays in place for now
        return Vector3.zero;
    }
}
