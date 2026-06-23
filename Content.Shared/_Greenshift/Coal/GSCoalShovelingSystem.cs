using Content.Shared._Greenshift.Coal.Components;
using Content.Shared.DoAfter;
using Content.Shared.Interaction;
using Content.Shared.Popups;
using Robust.Shared.Audio.Systems;

namespace Content.Shared._Greenshift.Coal;
public sealed class GSCoalShovelingSystem : EntitySystem
{

    [Dependency] private readonly SharedPopupSystem _popupSystem = default!;
    [Dependency] private readonly SharedAudioSystem _audioSystem = default!;
    [Dependency] private readonly SharedDoAfterSystem _doAfterSystem = default!;

    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<GSCoalPitComponent, InteractUsingEvent>(OnInteractUsing);
    }

    private void OnInteractUsing(EntityUid uid, GSCoalPitComponent component, InteractUsingEvent args)
    {
        if (args.Handled)
            return;

        if (TryComp<GSCoalShovelComponent>(args.Used, out var shovel))
        {
            // Shovel is full
            if (shovel.HasCoal)
            {
                _popupSystem.PopupClient(Loc.GetString("gs-coal-shovel-full"), uid, args.User);
                return;
            }

            // Start the doafter to dig some coal
            var doAfterEventArgs = new DoAfterArgs(EntityManager, args.User, 3, new GSCoalShovelingDoAfterEvent(), uid, target: args.Target, used: uid)
            {
                BreakOnMove = true,
                BreakOnDamage = true,
                NeedHand = true,
            };

            //audio handlers
            if (component.Stream == null)
                component.Stream = _audioSystem.PlayPredicted(component.DigSound, uid, args.User)?.Entity;

            if (!_doAfterSystem.TryStartDoAfter(doAfterEventArgs))
            {
                _audioSystem.Stop(component.Stream);
                return;
            }

            shovel.HasCoal = true;
        }
    }
}
