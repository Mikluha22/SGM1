using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "IsHealthLowCondition", story: "Is [controller] health below [threshold] percent", category: "Conditions", id: "0f396757132d8cd6e8e8070980ebc6fd")]
public partial class IsHealthLowCondition : Condition
{
    [SerializeReference] public BlackboardVariable<EnemyController> Controller;
    [SerializeReference] public BlackboardVariable<float> Threshold; // 0.0 - 1.0, например 0.3

    public override bool IsTrue()
    {
        if (Controller.Value == null)
            return false;

        float healthPercent = Controller.Value.GetHealthPercent();
        return healthPercent < Threshold.Value;
    }
}
