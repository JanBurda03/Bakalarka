public readonly record struct ProgramSetting
{
    public string SourceJson { get; init; }
    public string OutputJson { get; init; }
    public string[] SelectedPlacementHeuristics { get; init; }

    public bool AllowRotations { get; init; }
    public string? SelectedPackingOrderHeuristic { get; init; }
    public string AlgorithmName { get; init; }

    public int NumberOfIndividuals { get; init; }

    public int NumberOfGenerations { get; init; }
}
