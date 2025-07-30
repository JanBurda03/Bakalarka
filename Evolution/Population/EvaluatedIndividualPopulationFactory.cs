public class EvaluatedIndividualPopulationFactory<T,U>: IPopulationFactory<EvaluatedIndividual<T, U>> where U:IComparable<U>
{
    private IPopulationFactory<T> PopulationFactory { get; init; }
    private IFitnessEvaluator<T, U> FitnessEvaluator { get; init; }

    public EvaluatedIndividualPopulationFactory(IPopulationFactory<T> populationFactory, IFitnessEvaluator<T, U> fitnessEvaluator)
    {
        PopulationFactory = populationFactory;
        FitnessEvaluator = fitnessEvaluator;
    }

    public IReadOnlyList<EvaluatedIndividual<T, U>> CreatePopulation(int numberOfIndividuals)
    {
        IReadOnlyList<T> population = PopulationFactory.CreatePopulation(numberOfIndividuals);

        EvaluatedIndividual<T, U>[] evaluatedPopulation = new EvaluatedIndividual<T, U>[numberOfIndividuals];

        for (int i = 0; i < numberOfIndividuals; i++)
        {
            U fitness = FitnessEvaluator.EvaluateFitness(population[i]);
            evaluatedPopulation[i] = new EvaluatedIndividual<T, U> (population[i], fitness );
        }
        return Array.AsReadOnly(evaluatedPopulation);
    }
}