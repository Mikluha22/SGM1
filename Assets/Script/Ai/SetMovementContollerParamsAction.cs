using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "SetMovementContollerParams", story: "Set movement params for [controller]", category: "Action", id: "71e225af43b821ab802888e7438bc4dc")]
public partial class SetMovementContollerParamsAction : Action
{
    [SerializeReference] public BlackboardVariable<BaseCharacter> Controller;

    protected override Status OnStart()
    {
        Controller.Value.SetMovementParams();
        return Status.Success;
    }

  
}

