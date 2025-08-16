public static class EvolutionaryAlgorithms
{
    
    private static readonly Dictionary<string, Func<IReadOnlyList<PackingVector>, IMultipleFitnessEvaluator<PackingVector>, IEvolutionData<PackingVector>?, double, IEvolutionary<PackingVector>>>
        EvolutionaryAlgorithmDictionary = new()
    {
        { "Differential Evolution", (population, fitnessEvaluator, data, stopValue) => new PackingVectorDifferentialEvolution(population, fitnessEvaluator, data, stopValue) }
    };

    public static string[] EvolutionaryAlgorithmsArray => EvolutionaryAlgorithmDictionary.Keys.ToArray();

    public static IEvolutionary<PackingVector> GetEvolutionaryAlgorithm(string name, IReadOnlyList<PackingVector> initialPopulation, IMultipleFitnessEvaluator<PackingVector> fitnessEvaluator, IEvolutionData<PackingVector>? data, double stopValue)
    {
        if (EvolutionaryAlgorithmDictionary.TryGetValue(name, out var factory))
            return factory(initialPopulation, fitnessEvaluator, data, stopValue);

        throw new ArgumentException($"Unknown evolutionary algorithm: {name}");
    }
}
