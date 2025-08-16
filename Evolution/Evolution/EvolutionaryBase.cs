
public abstract class EvolutionaryBase<T> : IEvolutionary<T>
{

    public int CurrentGeneration { get; protected set; }
    public IReadOnlyList<T> CurrentGenerationPopulation { get; protected set; }
    public IReadOnlyList<double> CurrentGenerationFitness { get; protected set; }
    public (T individual, double fitness) CurrentGenerationBest { get; protected set; }
    public (T individual, double fitness) GlobalBest { get; protected set; }

    protected readonly double? _hardStop;

    protected readonly IEvolutionData<T> _data;
    protected readonly IComparer<double> _fitnessComparer;
    protected readonly IMultipleFitnessEvaluator<T> _fitnessEvaluator;


    public EvolutionaryBase(IReadOnlyList<T> initialPopulation, IMultipleFitnessEvaluator<T> fitnessEvaluator, bool minimizing, IEvolutionData<T>? data = null, double? hardStop = null)
    {
        _fitnessEvaluator = fitnessEvaluator;
        CurrentGenerationPopulation = initialPopulation;

        CurrentGeneration = 0;
        _fitnessComparer = new FitnessComparer<double>(minimizing);

        // class for statistical purposes (for example getting data for purpose of graphical representation of the best individual in every generation)
        _data = data ?? new ConsoleOnlyEvolutionData<T>();
        _hardStop = hardStop;


        // evaluation of generation 0
        CurrentGenerationFitness = fitnessEvaluator.EvaluateFitnesses(initialPopulation);

        GlobalBest = FindGenerationBestIndividual();


    }

    public void Evolve(int numberOfGenerations)
    {

        for (int i = 0; i < numberOfGenerations; i++)
        {

            NextGeneration();

            CurrentGenerationBest = FindGenerationBestIndividual();

            if (_fitnessComparer.Compare(CurrentGenerationBest.fitness, GlobalBest.fitness) < 0)
            {
                GlobalBest = CurrentGenerationBest;
            }

            _data.Update(CurrentGenerationPopulation, CurrentGenerationFitness, GlobalBest, CurrentGenerationBest, CurrentGeneration);

            if (HardStopNow())
            {
                return;
            }
        }
    }

    protected bool HardStopNow()
    {
        if (_hardStop == null)
        {
            return false;
        }

        // checking whether the limit for fitness has been reached

        return (_fitnessComparer.Compare(GlobalBest.fitness, (double)_hardStop) < 0);

    }

    protected (T individual, double fitness) FindGenerationBestIndividual()
    {
        var fitness = CurrentGenerationFitness[0];
        var individual = CurrentGenerationPopulation[0];

        for (int i = 1; i < CurrentGenerationPopulation.Count; i++)
        {
            if (_fitnessComparer.Compare(CurrentGenerationFitness[i], fitness) < 0)
            {
                fitness = CurrentGenerationFitness[i];
                individual = CurrentGenerationPopulation[i];
            }
        }
        return (individual, fitness);
    }

    protected abstract void NextGeneration();


}



