

public class DifferentialEvolution<T> : EvolutionaryBase<T>
{

    private readonly int _tournamentSize;

    private readonly IUniformMutator<T> _mutator;
    private readonly TournamentSelector<T> _selector;

    public DifferentialEvolution(IReadOnlyList<T> initialPopulation, IMultipleFitnessEvaluator<T> fitnessEvaluator, IUniformMutator<T> mutator, int tournamentSize, bool minimizing, IEvolutionData<T>? data = null, double? hardStop = null): base(initialPopulation, fitnessEvaluator, minimizing, data, hardStop)
    {
        _tournamentSize = tournamentSize;

        _mutator = mutator;

        _selector = new TournamentSelector<T>(_fitnessComparer);

    }



    protected override void NextGeneration()
    {
        CurrentGeneration++;

        // selecting individuals to be mutated
        IReadOnlyList<T> selected = _selector.SelectMultiple(CurrentGenerationPopulation, CurrentGenerationFitness, CurrentGenerationPopulation.Count, _tournamentSize).individuals;

        // mutation and their evaluation
        IReadOnlyList<T> mutated = Mutate(selected);
        IReadOnlyList<double> mutatedFitness = _fitnessEvaluator.EvaluateFitnesses(mutated);

        // new population consists of the selected and then evaluated individuals
        CurrentGenerationPopulation = mutated;
        CurrentGenerationFitness = mutatedFitness;


       
    }

    private IReadOnlyList<T> Mutate(IReadOnlyList<T> selected)
    {
        T[] mutated = new T[selected.Count];


        for (int i = 0;i < selected.Count; i++)
        {
            // uniformly mutating selected individual with random individual from the same generation (_selector.Select with tournament size 1 selects random individual)      

            mutated[i] = _mutator.Mutate(selected[i], _selector.Select(CurrentGenerationPopulation, CurrentGenerationFitness, 1).individual);
        }
        return mutated;
    }
}




