public readonly record struct ProgramSetting
{
    public string SourceJson { get; init; }
    public string OutputJson { get; init; }
    public PackingSetting PackingSetting { get; init; }

    public string AlgorithmName { get; init; }

    public int NumberOfIndividuals { get; init; }

    public int NumberOfGenerations { get; init; }
}
