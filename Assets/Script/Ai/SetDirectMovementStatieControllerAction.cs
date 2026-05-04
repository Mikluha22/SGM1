using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Set direct movement statie controller", story: "Set direct movement statie [controller]", category: "Action", id: "1b3a135da9de6d4fbc7bacf1c38ba4dd")]
public partial class SetDirectMovementStatieControllerAction : Action
{
    [SerializeReference] public BlackboardVariable<EnemyController> Controller;

    protected override Status OnStart()
    {
        Controller.Value.SetDirectMove();
        return Status.Running;
    }

 
}

