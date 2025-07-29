

public class GeneticAlgorithm<T, U> where U : IComparable<U>
{
    public int Generation { get; private set; }

    private IReadOnlyList<T> CurrentPopulation { get; set; }

    private IReadOnlyList<U> CurrentPopulationFitnesses { get; set; }

    private IMultipleFitnessesEvaluator<T, U> FitnessesEvaluator { get; init; }

    private IPopulationReproducer<T, U> PopulationReproducer { get; init; }

    public GeneticAlgorithm(IMultipleFitnessesEvaluator<T, U> fitnessesEvaluator, T[] initialPopulation, IPopulationReproducer<T, U> populationReproducer)
    {
        FitnessesEvaluator = fitnessesEvaluator;
        PopulationReproducer = populationReproducer;
        Generation = 0;
        CurrentPopulation = initialPopulation;
        CurrentPopulationFitnesses = FitnessesEvaluator.EvaluateFitnesses(CurrentPopulation);
    }

    public void Reset(T[] initialPopulation)
    {
        Generation = 0;
        CurrentPopulation = initialPopulation;
        CurrentPopulationFitnesses = FitnessesEvaluator.EvaluateFitnesses(CurrentPopulation);
    }

    public void Evolve(int numberOfGeneretions)
    {
        for (int i = 0; i < numberOfGeneretions; i++) 
        {
            NextGeneration();
        }
        
    }

    public void NextGeneration()
    {
        Generation++;
        CurrentPopulation = PopulationReproducer.Reproduce(CurrentPopulation, CurrentPopulationFitnesses);
        CurrentPopulationFitnesses = FitnessesEvaluator.EvaluateFitnesses(CurrentPopulation);
    }
}
