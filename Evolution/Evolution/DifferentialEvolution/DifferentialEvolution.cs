
public class DifferentialEvolution<T> : IEvolutionary<T>
{
    
    public int CurrentGeneration { get; private set; }
    public IReadOnlyList<T> CurrentPopulation { get; private set; }
    public IReadOnlyList<double> CurrentFitness { get; private set; }
    public (T individual, double fitness) GlobalBest { get; private set; }

    private const int TournamentSize = 2;

    private readonly IEvolutionData<T> _data;
    private readonly IComparer<double> _fitnessComparer;
    private readonly IMultipleFitnessEvaluator<T> _fitnessEvaluator;
    private readonly IUniformMutator<T> _mutator;
    private readonly TournamentSelector<T> _selector;

    public DifferentialEvolution(IReadOnlyList<T> initialPopulation, IMultipleFitnessEvaluator<T> fitnessEvaluator, IUniformMutator<T> mutator, bool minimizing, IEvolutionData<T>? data = null)
    {
        _fitnessEvaluator = fitnessEvaluator;
        CurrentPopulation = initialPopulation;

        CurrentGeneration = 0;
        _mutator = mutator;
        _fitnessComparer = new FitnessComparer<double>(minimizing);
        _selector = new TournamentSelector<T>(_fitnessComparer);


        // class for statistical purposes (for example getting data for purpose of graphical representation of the best individual in every generation)
        _data = data ?? new NoEvolutionData<T>();


        // evaluation of generation 0
        CurrentFitness = fitnessEvaluator.EvaluateFitnesses(initialPopulation);

        GlobalBest = FindGenerationBest();


    }

    public void Evolve(int numberOfGenerations)
    {

        for (int i = 0; i < numberOfGenerations; i++) 
        { 

            NextGeneration();

        }
    }


    private (T individual, double fitness) FindGenerationBest()
    {
        var fitness = CurrentFitness[0];
        var individual = CurrentPopulation[0];

        for (int i = 1; i < CurrentPopulation.Count ; i++) 
        { 
            if (_fitnessComparer.Compare(CurrentFitness[i], fitness) < 0)
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

        // selecting individuals to be mutated
        IReadOnlyList<T> selected = _selector.SelectMultiple(CurrentPopulation, CurrentFitness, CurrentPopulation.Count, TournamentSize).individuals;

        // mutation and their evaluation
        IReadOnlyList<T> mutated = Mutate(selected);
        IReadOnlyList<double> mutatedFitness = _fitnessEvaluator.EvaluateFitnesses(mutated);

        // new population consists of the selected and then evaluated individuals
        CurrentPopulation = mutated;
        CurrentFitness = mutatedFitness;


        var generationBest = FindGenerationBest();
        if (_fitnessComparer.Compare(generationBest.fitness, GlobalBest.fitness) < 0)
        {
            GlobalBest = generationBest;
        }
        _data.Update(CurrentPopulation, CurrentFitness, GlobalBest, generationBest, CurrentGeneration);


    }

    private IReadOnlyList<T> Mutate(IReadOnlyList<T> selected)
    {
        T[] mutated = new T[selected.Count];


        for (int i = 0;i < selected.Count; i++)
        {
            // uniformly mutating selected individual with random individual from the same generation (_selector.Select with tournament size 1 selects random individual)      

            mutated[i] = _mutator.Mutate(selected[i], _selector.Select(CurrentPopulation, CurrentFitness, 1).individual);
        }
        return mutated;
    }
}

public class PackingVectorDifferentialEvolution(IReadOnlyList<PackingVector> initialPopulation, IMultipleFitnessEvaluator<PackingVector> fitnessEvaluator) : DifferentialEvolution<PackingVector>(initialPopulation, fitnessEvaluator, new PackingVectorUniformMutator(0.85), true) { }


