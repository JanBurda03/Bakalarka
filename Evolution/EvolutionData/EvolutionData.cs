public class EvolutionData<T>: IEvolutionData<T>
{
    // class used for gaining additional data from the evolution, for example for creating graphs
    public IReadOnlyList<(T, double, int)> Data => _data.AsReadOnly();
    private readonly List<(T, double, int)> _data = new List<(T, double, int)>();
    public void Update(IReadOnlyList<T> currentPopulation, IReadOnlyList<double> currentFitness, (T, double) currentBest, (T, double) currentGenerationBest, int currentGeneration)
    {
        _data.Add((currentGenerationBest.Item1, currentGenerationBest.Item2, currentGeneration));
        Console.WriteLine($"The best individual in generation {currentGeneration} has fitness {currentGenerationBest.Item2}");
    }
}

public class NoEvolutionData<T>: IEvolutionData<T>
{
    public void Update(IReadOnlyList<T> currentPopulation, IReadOnlyList<double> currentFitness, (T, double) currentBest, (T, double) currentGenerationBest, int currentGeneration)
    {

        Console.WriteLine(currentGenerationBest.Item2);
    }
}
