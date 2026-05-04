using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "OverrideLookDirect", story: "Override look direct for [controller]", category: "Action", id: "ade3a6a5b6ae7fc3aaa30b55c8fab574")]
public partial class OverrideLookDirectAction : Action
{
    [SerializeReference] public BlackboardVariable<BaseCharacter> Controller;

    protected override Status OnStart()
    {
        Controller.Value.OverrideLookDirect();
        return Status.Success;
    }

    protected override Status OnUpdate()
    {
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}

