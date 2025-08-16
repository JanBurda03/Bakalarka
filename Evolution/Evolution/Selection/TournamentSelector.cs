public class TournamentSelector<T>
{
    private readonly IComparer<double> _fitnessComparer;
    private readonly Random _random;

    public TournamentSelector(bool minimizing)
    {
        _fitnessComparer = new FitnessComparer<double>(minimizing);
        _random = new Random();
    }

    public TournamentSelector(IComparer<double> fitnessComparer)
    {
        _fitnessComparer = fitnessComparer;
        _random = new Random();
    }

    public (IReadOnlyList<T> individuals, IReadOnlyList<double> fitnesses) SelectMultiple(IReadOnlyList<T> population, IReadOnlyList<double> fitness, int number, int tournamentSize)
    {
        T[] selected = new T[number];
        double[] selectedFitness = new double[number];

        for (int i = 0; i < number; i++)
        {
            (selected[i], selectedFitness[i]) = Select(population, fitness, tournamentSize);
        }
        return (selected, selectedFitness);
    }

    public (T individual, double fitness) Select(IReadOnlyList<T> population, IReadOnlyList<double> fitness, int tournamentSize)
    {
        // randomly selecting certain number of individuals given by the tournamentSize and returning the best one by fitness

        if (tournamentSize < 1)
        {
            throw new ArgumentOutOfRangeException("Tournament size must be at least 1!");
        }

        int bestIndex = _random.Next(population.Count);

        for (int j = 1; j < tournamentSize; j++)
        {
            int challengerIndex = _random.Next(population.Count);

            if (_fitnessComparer.Compare(fitness[challengerIndex], fitness[bestIndex]) < 0)
            {
                bestIndex = challengerIndex;
            }
        }

        return (population[bestIndex], fitness[bestIndex]);
    }

    
}

