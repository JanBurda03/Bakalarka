public class PackingVectorArrayParallelFintessesEvaluator<T>
    : IMultipleFitnessesEvaluator<PackingVector, double>
    where T : IFitnessEvaluator<PackingVector, double>,
                IParallelSafeCloneable<T>
{
    private T[] PackingVectorFitnessEvaluators { get; init; }

    public PackingVectorArrayParallelFintessesEvaluator(T packingVectorFitnessEvaluator, int numberOfThreads)
    {
        if (numberOfThreads < 1)
        {
            throw new ArgumentException();
        }

        PackingVectorFitnessEvaluators = new T[numberOfThreads];

        PackingVectorFitnessEvaluators[0] = packingVectorFitnessEvaluator;

        for (int i = 1; i < numberOfThreads; i++)
        {
            PackingVectorFitnessEvaluators[i] = packingVectorFitnessEvaluator.ParallelSafeClone();
        }


    }

    public double[] EvaluateFitnesses(IReadOnlyList<PackingVector> packingVectors)
    {
        int count = packingVectors.Count;
        double[] results = new double[count];
        int numberOfSolvers = PackingVectorFitnessEvaluators.Length;

        Task[] tasks = new Task[numberOfSolvers];

        for (int t = 0; t < numberOfSolvers; t++)
        {
            int solverIndex = t;
            tasks[solverIndex] = Task.Run(() =>
            {
                T evaluator = PackingVectorFitnessEvaluators[solverIndex];

                for (int i = solverIndex; i < count; i += numberOfSolvers)
                {
                    results[i] = evaluator.EvaluateFitness(packingVectors[i]);
                }
            });
        }

        Task.WaitAll(tasks);
        return results;
    }
}