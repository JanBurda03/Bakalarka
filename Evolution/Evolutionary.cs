

public class Evolutionary<T, U> where U : IComparable<U>
{
    public int Generation { get; private set; }

    private IReadOnlyList<EvaluatedIndividual<T, U>> CurrentEvaluatedPopulation { get; set; }
    private IReadOnlyList<IEvolutionBlock<EvaluatedIndividual<T, U>>> EvolutionBlocks { get; init; }

    private IElitism<EvaluatedIndividual<T, U>> Elitism { get; init; }

    public Evolutionary(IReadOnlyList<EvaluatedIndividual<T, U>> initialEvaluatedPopulation, IReadOnlyList<IEvolutionBlock<EvaluatedIndividual<T, U>>> evolutionBlocks, IElitism<EvaluatedIndividual<T, U>> elitism)
    {
        CurrentEvaluatedPopulation = initialEvaluatedPopulation;
        EvolutionBlocks = evolutionBlocks;
        Generation = 0;
        Elitism = elitism;

    }

    public Evolutionary(IReadOnlyList<EvaluatedIndividual<T, U>> initialEvaluatedPopulation, IEvolutionBlock<EvaluatedIndividual<T, U>> evolutionBlock, IElitism<EvaluatedIndividual<T, U>> elitism) : this(initialEvaluatedPopulation, new[] { evolutionBlock }, elitism) { }


    public void Evolve(int numberOfGeneretions)
    {
        for (int i = 0; i < numberOfGeneretions; i++)
        {
            NextGeneration();

            Console.WriteLine(Generation);
            Console.WriteLine(CurrentEvaluatedPopulation.Min(x => x.Fitness));
        }

    }



    public void NextGeneration()
    {
        Generation++;
        int numberOfSolvers = EvolutionBlocks.Count;
        IReadOnlyList<EvaluatedIndividual<T, U>>[] results = new IReadOnlyList<EvaluatedIndividual<T, U>>[numberOfSolvers];

        int lastIndex = numberOfSolvers - 1;

        Task[] tasks = new Task[lastIndex];

        for (int t = 0; t < lastIndex; t++)
        {
            int solverIndex = t;
            IEvolutionBlock<EvaluatedIndividual<T, U>> evolutionBlock = EvolutionBlocks[solverIndex];
            tasks[solverIndex] = Task.Run(() =>
            {
                results[solverIndex] = evolutionBlock.NextPartialGeneration(CurrentEvaluatedPopulation);
            });
        }

        results[lastIndex] = EvolutionBlocks[lastIndex].NextPartialGeneration(CurrentEvaluatedPopulation);


        Task.WaitAll(tasks);


        List<EvaluatedIndividual<T, U>> mergedList = new List<EvaluatedIndividual<T, U>>(results.Sum(r => r.Count) + Elitism.NumberOfElites);

        foreach (var part in results)
        {
            mergedList.AddRange(part);
        }

        mergedList.AddRange(Elitism.GetElites(CurrentEvaluatedPopulation));

        CurrentEvaluatedPopulation = mergedList;

    }
}


