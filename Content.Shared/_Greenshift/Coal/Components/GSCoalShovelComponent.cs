using Robust.Shared.GameStates;

namespace Content.Shared._Greenshift.Coal.Components;

[Access(typeof(GSCoalShovelingSystem))]
[RegisterComponent, NetworkedComponent, AutoGenerateComponentState]
public sealed partial class GSCoalShovelComponent : Component
{
    /// <summary>
    /// Whether the shovel currently has coal on it.
    /// </summary>
    [DataField, AutoNetworkedField]
    public bool HasCoal { get; set; } = false;
}
