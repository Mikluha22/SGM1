using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Set pathfinding state for controller", story: "Set pathfinding state for [controller]", category: "Action", id: "be385eb44d4c809d7da0457d5dd86a99")]
public partial class SetPathfindingStateForControllerAction : Action
{
    [SerializeReference] public BlackboardVariable<EnemyController> Controller;

    protected override Status OnStart()
    {
        Controller.Value.SetPathFinding();
        return Status.Running;
    }
}

