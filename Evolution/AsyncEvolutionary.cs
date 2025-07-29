public interface IElitism<T,U> where U : IComparable<U>
{
    public IReadOnlyList<EvaluatedIndividiual<T, U>> GetElites(IReadOnlyList<EvaluatedIndividiual<T, U>> evaluatedPopulation);
}

public class AsyncEvolutionary<T, U> where U : IComparable<U>
{
    public int Generation { get; private set; }

    private IReadOnlyList<EvaluatedIndividiual<T, U>> CurrentEvaluatedPopulation { get; set; }
    private IReadOnlyList<IEvolutionBlock<T, U>> EvolutionBlocks { get; init; }

    private IElitism<T, U> Elitism { get; init; }

    public AsyncEvolutionary(IReadOnlyList<EvaluatedIndividiual<T, U>> initialEvaluatedPopulation, IReadOnlyList<IEvolutionBlock<T, U>> evolutionBlocks, IElitism<T, U> elitism)
    {
        CurrentEvaluatedPopulation = initialEvaluatedPopulation;
        EvolutionBlocks = evolutionBlocks;
        Generation = 0;
        Elitism = elitism;
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
        int numberOfSolvers = EvolutionBlocks.Count;
        IReadOnlyList<EvaluatedIndividiual<T, U>>[] results = new IReadOnlyList<EvaluatedIndividiual<T, U>>[numberOfSolvers];

        Task[] tasks = new Task[numberOfSolvers];

        for (int t = 0; t < numberOfSolvers; t++)
        {
            int solverIndex = t;
            IEvolutionBlock<T, U> evolutionBlock = EvolutionBlocks[solverIndex];
            tasks[solverIndex] = Task.Run(() =>
            {
                results[solverIndex] = evolutionBlock.NextPartialGeneration(CurrentEvaluatedPopulation);
            });
        }

        Task.WaitAll(tasks);


        List<EvaluatedIndividiual<T, U>> mergedList = new List<EvaluatedIndividiual<T, U>>();

        foreach (var part in results)
        {
            mergedList.AddRange(part);
        }

        mergedList.AddRange(Elitism.GetElites(CurrentEvaluatedPopulation));

        CurrentEvaluatedPopulation = mergedList;

    }
}
