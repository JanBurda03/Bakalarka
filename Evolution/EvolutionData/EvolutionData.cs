public class EvolutionData<T>: IEvolutionData<T>
{
    // class used for gaining additional data from the evolution, for example for creating graphs
    public IReadOnlyList<double> GenerationsBestFitnesses => _generationsBestFitnesses.AsReadOnly();
    public IReadOnlyList<double> GenerationsAverageFitnesses => _generationsAverageFitnesses.AsReadOnly();

    private readonly List<double> _generationsBestFitnesses = new List<double>();
    private readonly List<double> _generationsAverageFitnesses = new List<double>();
    public void Update(IReadOnlyList<T> currentPopulation, IReadOnlyList<double> currentFitness, (T, double) currentBest, (T, double) currentGenerationBest, int currentGeneration)
    {
        double average = currentFitness.Sum() / currentFitness.Count;

        _generationsBestFitnesses.Add(currentGenerationBest.Item2);
        _generationsAverageFitnesses.Add(average);

        Console.WriteLine($"Best Fitness of Generation {currentGeneration} is {currentGenerationBest.Item2} with Average of {average}");
    }
}

public class ConsoleOnlyEvolutionData<T>: IEvolutionData<T>
{
    public void Update(IReadOnlyList<T> currentPopulation, IReadOnlyList<double> currentFitness, (T, double) currentBest, (T, double) currentGenerationBest, int currentGeneration)
    {

        double average = currentFitness.Sum() / currentFitness.Count;

        Console.WriteLine($"Best Fitness of Generation {currentGeneration} is {currentGenerationBest.Item2} with Average of {average}");
    }
}
