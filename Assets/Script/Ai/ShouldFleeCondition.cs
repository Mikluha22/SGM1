using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "ShouldFleeCondition", story: "Should [controller] flee from target", category: "Conditions", id: "0fc704c59635cb4841c8e4b422a3a683")]

public partial class ShouldFleeCondition : Condition
{
    [SerializeReference] public BlackboardVariable<EnemyController> Controller;

    public override bool IsTrue()
    {
        return Controller.Value.ShouldFlee();
    }
}
