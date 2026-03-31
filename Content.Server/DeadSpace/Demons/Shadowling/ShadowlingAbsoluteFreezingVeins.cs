using Content.Shared.Actions;
using Content.Shared.DeadSpace.Demons.Shadowling;
using Content.Server.Temperature.Components;
using Content.Shared.Humanoid;
using Content.Shared.Popups;
using Content.Shared.Damage;

namespace Content.Server.DeadSpace.Demons.Shadowling;

public sealed class ShadowlingAbsoluteFreezingVeinsSystem : EntitySystem
{
    [Dependency] private readonly SharedActionsSystem _actions = default!;
    [Dependency] private readonly SharedPopupSystem _popup = default!;
    [Dependency] private readonly DamageableSystem _damageable = default!;

    public override void Initialize()
    {
        base.Initialize();
        SubscribeLocalEvent<ShadowlingAbsoluteFreezingVeinsComponent, ComponentInit>(OnComponentInit);
        SubscribeLocalEvent<ShadowlingAbsoluteFreezingVeinsComponent, ShadowlingAbsoluteFreezingVeinsEvent>(OnAbsoluteFreezingVeinsAction);
    }

    private void OnComponentInit(EntityUid uid, ShadowlingAbsoluteFreezingVeinsComponent component, ComponentInit args)
    {
        _actions.AddAction(uid, ref component.ActionAbsoluteFreezingVeinsEntity, component.ActionAbsoluteFreezingVeins);
    }

    private void OnAbsoluteFreezingVeinsAction(EntityUid uid, ShadowlingAbsoluteFreezingVeinsComponent component, ShadowlingAbsoluteFreezingVeinsEvent args)
    {
        if (args.Handled) return;

        var target = args.Target;

        if (!HasComp<HumanoidAppearanceComponent>(target))
            return;

        if (HasComp<ShadowlingComponent>(target) ||
            HasComp<ShadowlingRevealComponent>(target) ||
            HasComp<ShadowlingSlaveComponent>(target))
            return;
        if (TryComp<TemperatureComponent>(target, out var temp))
        {
            temp.CurrentTemperature = component.TemperatureSet;
        }

        DamageSpecifier damage = new();
        damage.DamageDict.Add("Cold", component.DamageCold);
        _damageable.TryChangeDamage(target, damage, true);
        _popup.PopupEntity("Ваша кровь замерзает!", target, target, PopupType.LargeCaution);

        args.Handled = true;
    }
}
