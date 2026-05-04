using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "SetFleeMovementAction", story: "Set flee movement for [controller]", category: "Action", id: "86929ad44f730b26a5cd94cc82cd19d6")]
public partial class SetFleeMovementAction : Action
{
    [SerializeReference] public BlackboardVariable<EnemyController> Controller;

    protected override Status OnStart()
    {
        Controller.Value.SetFleeDirection();
        Controller.Value.BeginFlee();
        return Status.Running;
    }

    protected override void OnEnd()
    {
        // Завершаем побег, если этот action перестаёт быть активным
        Controller.Value.EndFlee();
    }
}

