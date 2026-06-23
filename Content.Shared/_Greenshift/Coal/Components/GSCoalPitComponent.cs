using Robust.Shared.Audio;

namespace Content.Shared._Greenshift.Coal.Components;

[Access(typeof(GSCoalShovelingSystem))]
[RegisterComponent, AutoGenerateComponentState]
public sealed partial class GSCoalPitComponent : Component
{
    /// <summary>
    /// Auto-networked field to track shovel digging.
    /// This makes sure a looping audio Stream isn't opened
    /// on the client-side. (DoAfterId/EntityUid isn't serializable.)
    /// </summary>
    [DataField, ViewVariables(VVAccess.ReadOnly), AutoNetworkedField]
    public bool ActiveShovelDigging;

    [DataField, ViewVariables(VVAccess.ReadOnly)]
    public EntityUid? Stream;

    /// <summary>
    /// Sound to make when digging the coal pit
    /// </summary>
    [DataField, ViewVariables(VVAccess.ReadOnly)]
    public SoundPathSpecifier DigSound = new SoundPathSpecifier("/Audio/Items/shovel_dig.ogg")
    {
        Params = AudioParams.Default.WithLoop(true)
    };
}
