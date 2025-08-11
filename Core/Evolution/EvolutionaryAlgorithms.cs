public static class EvolutionaryAlgorithms
{
    private static readonly Dictionary<string, Func<IReadOnlyList<PackingVector>, IMultipleFitnessEvaluator<PackingVector>, IEvolutionary<PackingVector>>>
        EvolutionaryAlgorithmDictionary = new()
    {
        { "Differential Evolution", (population, fitnessEvaluator) => new PackingVectorDifferentialEvolution(population, fitnessEvaluator) }
    };

    public static string[] EvolutionaryAlgorithmsArray => EvolutionaryAlgorithmDictionary.Keys.ToArray();

    public static IEvolutionary<PackingVector> GetEvolutionaryAlgorithm(string name, IReadOnlyList<PackingVector> initialPopulation, IMultipleFitnessEvaluator<PackingVector> fitnessEvaluator)
    {
        if (EvolutionaryAlgorithmDictionary.TryGetValue(name, out var factory))
            return factory(initialPopulation, fitnessEvaluator);

        throw new ArgumentException($"Unknown evolutionary algorithm: {name}");
    }
}
