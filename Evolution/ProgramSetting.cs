public readonly record struct ProgramSetting
{
    public ProgramSetting(string sourceJson, string outputJson, PackingSetting packingSetting, string algorithmName, int numberOfIndividuals, int numberOfGenerations)
    {
        SourceJson = sourceJson;
        OutputJson = outputJson;
        PackingSetting = packingSetting;
        AlgorithmName = algorithmName;
        NumberOfIndividuals = numberOfIndividuals;
        NumberOfGenerations = numberOfGenerations;
    }

    public string SourceJson { get; init; }
    public string OutputJson { get; init; }
    public PackingSetting PackingSetting { get; init; }

    public string AlgorithmName { get; init; }

    public int NumberOfIndividuals { get; init; }

    public int NumberOfGenerations { get; init; }
}
