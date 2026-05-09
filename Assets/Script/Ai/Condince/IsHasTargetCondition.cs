using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "IsHasTarget", story: "Is [controlle] has target", category: "Conditions", id: "274b985b758928a8509a066eaffb4c6c")]
public partial class IsHasTargetCondition : Condition
{
    [SerializeReference] public BlackboardVariable<EnemyController> Controlle;

    public override bool IsTrue()
    {
        return Controlle.Value.Target != null;
    }

  
}
