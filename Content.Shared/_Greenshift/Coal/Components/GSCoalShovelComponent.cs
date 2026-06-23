namespace Content.Shared._Greenshift.Coal.Components;

[Access(typeof(GSCoalShovelingSystem))]
[RegisterComponent]
public sealed partial class GSCoalShovelComponent : Component
{
    /// <summary>
    /// Whether the shovel currently has coal on it.
    /// </summary>
    [DataField]
    public bool HasCoal { get; set; } = false;
}
