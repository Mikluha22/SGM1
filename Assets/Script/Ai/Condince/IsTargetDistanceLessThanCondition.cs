using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "IsTargetDistanceLessThanCondition", story: "Is [controller] target distance less than [value]", category: "Conditions", id: "aad13ef850e6cb82c06b9fd067990f7c")]
public partial class IsTargetDistanceLessThanCondition : Condition
{
    [SerializeReference] public BlackboardVariable<EnemyController> Controller;
    [SerializeReference] public BlackboardVariable<float> Value;

    public override bool IsTrue()
    {
        return Controller.Value.DistanceTo(Controller.Value.Target) < Value;
    }

    public override void OnStart()
    {
    }

    public override void OnEnd()
    {
    }
}
