using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "ClearFleeMovementAction", story: "Clear flee movement for [controller]", category: "Action", id: "0d648b83faa50fa054f9b98f4ee47d24")]

public partial class ClearFleeMovementAction : Action
{
    [SerializeReference] public BlackboardVariable<EnemyController> Controller;

    protected override Status OnStart()
    {
        if (Controller.Value != null)
            Controller.Value.EndFlee();
        return Status.Success;
    }
}

