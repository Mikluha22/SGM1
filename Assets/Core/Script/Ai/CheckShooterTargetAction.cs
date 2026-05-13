using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "CheckShooterTarget", story: "Check shooter target for [controller]", category: "Action", id: "bc0c49d3f3d6c3069c82eb439f58c3a8")]
public partial class CheckShooterTargetAction : Action
{
    [SerializeReference] public BlackboardVariable<BaseCharacter> Controller;

    protected override Status OnStart()
    {
        if (Controller.Value.HasShooterTarget())
            return Status.Success;
        else
            return Status.Failure;
    }
}

