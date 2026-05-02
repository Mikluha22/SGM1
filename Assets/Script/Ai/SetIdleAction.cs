using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "SetIdle", story: "Set Idle statle for [controller]", category: "Action", id: "059d13a53483196082ad65aae0058de0")]
public partial class SetIdleAction : Action
{
    [SerializeReference] public BlackboardVariable<EnemyController> Controller;

    protected override Status OnStart()
    {
        Controller.Value.SetIdle();
        return Status.Running;
    }

}

