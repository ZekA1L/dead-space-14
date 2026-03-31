using Content.Shared.Actions;
using Robust.Shared.GameStates;
using Robust.Shared.Prototypes;

namespace Content.Shared.DeadSpace.Demons.Shadowling;

[RegisterComponent, NetworkedComponent]
public sealed partial class ShadowlingAbsoluteFreezingVeinsComponent : Component
{
    [DataField] public EntProtoId ActionAbsoluteFreezingVeins = "ActionShadowlingAbsoluteFreezingVeins";
    [DataField] public EntityUid? ActionAbsoluteFreezingVeinsEntity;
    [DataField] public float DamageCold = 60f;
    [DataField] public float TemperatureSet = 0.0f;
}

public sealed partial class ShadowlingAbsoluteFreezingVeinsEvent : EntityTargetActionEvent {}
