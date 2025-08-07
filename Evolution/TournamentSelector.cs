public class TournamentSelector<T>
{
    private bool Minimizing { get; init; }
    private Random Random { get; init; }

    public TournamentSelector(bool minimizing)
    {
        Minimizing = minimizing;
        Random = new Random();
    }

    public (IReadOnlyList<T>, IReadOnlyList<double>) SelectMultiple(IReadOnlyList<T> population, IReadOnlyList<double> fitness, int number, int tournamentSize)
    {
        T[] selected = new T[number];
        double[] selectedFitness = new double[number];

        for (int i = 0; i < number; i++)
        {
            (selected[i], selectedFitness[i]) = Select(population, fitness, tournamentSize);
        }
        return (selected, selectedFitness);
    }

    public (T, double) Select(IReadOnlyList<T> population, IReadOnlyList<double> fitness, int tournamentSize)
    {
        int bestIndex = Random.Next(population.Count);

        for (int j = 1; j < tournamentSize; j++)
        {
            int challengerIndex = Random.Next(population.Count);

            if (IsBetter(fitness[challengerIndex], fitness[bestIndex]))
            {
                bestIndex = challengerIndex;
            }
        }

        return (population[bestIndex], fitness[bestIndex]);
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