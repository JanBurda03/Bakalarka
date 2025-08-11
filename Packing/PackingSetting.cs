public readonly record struct PackingSetting
{    
    public string[] SelectedPlacementHeuristics { get; init; }
    public bool AllowRotations { get; init; }
    public string? SelectedPackingOrderHeuristic { get; init; }
}