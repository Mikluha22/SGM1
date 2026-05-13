using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "FindClosest", story: "Find closest target for [controller]", category: "Action", id: "0a9297265094e1a25a907b33219c1a01")]
public partial class FindClosestAction : Action
{
    [SerializeReference] public BlackboardVariable<EnemyController> Controller;

    protected override Status OnStart()
    {
        Controller.Value.FindClosest();
        return Status.Running;
    }
}

