
public class DifferentialEvolution<T> : IEvolutionary<T>
{
    
    public int CurrentGeneration { get; private set; }
    public IReadOnlyList<T> CurrentPopulation { get; private set; }
    public IReadOnlyList<double> CurrentFitness { get; private set; }

    private bool Minimizing { get; init; }
    private IMultipleFitnessEvaluator<T> FitnessEvaluator { get; init; }
    private IUniformMutator<T> Mutator { get; init; }
    private TournamentSelector<T> Selector { get; init; }

    public DifferentialEvolution(IReadOnlyList<T> initialPopulation, IMultipleFitnessEvaluator<T> fitnessEvaluator, IUniformMutator<T> mutator, bool minimizing)
    {
        FitnessEvaluator = fitnessEvaluator;
        CurrentPopulation = initialPopulation;
        CurrentFitness = fitnessEvaluator.EvaluateFitnesses(initialPopulation);
        Selector = new TournamentSelector<T>(true);
        CurrentGeneration = 0;
        Mutator = mutator;
        Minimizing = minimizing;
    }

    public void Evolve(int numberOfGenerations)
    {
        var lastBest = GetBest().Item2;
        for (int i = 0; i < numberOfGenerations; i++) 
        { 
            NextGeneration();

            var candidate = GetBest().Item2;
            if (candidate < lastBest)
            {
                lastBest = candidate;
                Console.WriteLine(CurrentGeneration);
                Console.WriteLine(candidate);
            }
            
        }
    }

    public (T, double) GetBest()
    {
        var fitness = CurrentFitness[0];
        var individual = CurrentPopulation[0];

        for (int i = 1; i < CurrentPopulation.Count ; i++) 
        { 
            if (CurrentFitness[i] < fitness)
            {
                fitness = CurrentFitness[i];
                individual = CurrentPopulation[i];
            }
        }
        return (individual, fitness);
    }

    private void NextGeneration()
    {
        CurrentGeneration++;

        (IReadOnlyList<T> selected, _) = Selector.SelectMultiple(CurrentPopulation, CurrentFitness, CurrentPopulation.Count, 16);

        IReadOnlyList<T> mutated = Mutate(selected);
        IReadOnlyList<double> mutatedFitness = FitnessEvaluator.EvaluateFitnesses(mutated);


        T[] newGeneration = new T[mutated.Count];
        double[] newGenerationFitness = new double[mutated.Count];

        for (int i = 0; i < newGenerationFitness.Length; i++)
        {
            if(IsBetter(mutatedFitness[i], CurrentFitness[i]))
            {
                newGeneration[i] = mutated[i];
                newGenerationFitness[i] = mutatedFitness[i];
            }
            else
            {
                newGeneration[i] = CurrentPopulation[i];
                newGenerationFitness[i] = CurrentFitness[i];
            }
        }
        CurrentPopulation = newGeneration;
        CurrentFitness = newGenerationFitness;


    }

    private IReadOnlyList<T> Mutate(IReadOnlyList<T> selected)
    {
        T[] mutated = new T[selected.Count];
        for (int i = 0;i < CurrentPopulation.Count;i++)
        {
            mutated[i] = Mutator.Mutate(selected[i], Selector.Select(CurrentPopulation, CurrentFitness, 1).Item1);
        }
        return mutated;
    }

    private bool IsBetter(double candidate, double currentBest)
    {
        int comparison = candidate.CompareTo(currentBest);

        if (Minimizing)
        {
            return comparison < 0;
        }
        else
        {
            return comparison > 0;
        }
    }
}

public class PackingVectorDifferentialEvolution(IReadOnlyList<PackingVector> initialPopulation, IMultipleFitnessEvaluator<PackingVector> fitnessEvaluator) : DifferentialEvolution<PackingVector>(initialPopulation, fitnessEvaluator, new PackingVectorUniformMutator(0.8), true) { }


