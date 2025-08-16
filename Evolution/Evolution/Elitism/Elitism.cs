public class Elitism<T> : IElitism<T>
{
    private readonly IComparer<double> _fitnessComparer;

    public Elitism(bool minimizing)
    {
        _fitnessComparer = new FitnessComparer<double>(minimizing);
    }

    public Elitism(IComparer<double> fitnessComparer)
    {
        _fitnessComparer = fitnessComparer;
    }

    public (IReadOnlyList<T> individuals, IReadOnlyList<double> fitnesses) GetElites(IReadOnlyList<T> population, IReadOnlyList<double> fitness, int numberOfElites)
    {
        if (population.Count != fitness.Count)
        {
            throw new ArgumentException("Number of individuals and fitnesses must be the same!");
        }

        if (numberOfElites <= 0)
        {
            return (Array.Empty<T>(), Array.Empty<double>());
        }

        if (population.Count <= numberOfElites)
        {
            return (population, fitness);
        }

        
        // array for the best individuals and array for their fitness
        T[] elites = new T[numberOfElites];
        double[] elitesFitness = new double[numberOfElites];

        // initially filling the elite array
        for (int i  = 0; i < numberOfElites; i++)
        {
            elites[i] = population[i];
            elitesFitness[i] = fitness[i];
        }

        int worstIndex = -1;

        for (int i = numberOfElites; i < population.Count; i++)
        {
            // worstIndex -1 means that it is not known which of the elites has the worst fitness 
            // we need to know worstIndex, because the worst is the one that is going to be replaced next
            if (worstIndex == -1)
            {
                worstIndex = FindWorstIndex(elitesFitness);
            }

            // if the next individual has better fitness that the worst of elites, it is replaced
            if (_fitnessComparer.Compare(fitness[i], elitesFitness[worstIndex]) < 0)
            {
                elitesFitness[worstIndex] = fitness[i];
                elites[worstIndex] = population[i];

                worstIndex = -1;
            }
            
        }

        return (elites, elitesFitness);
    }

    private int FindWorstIndex(IReadOnlyList<double> elitesFitness)
    {
        // finding the worst index by comparing all elites
        int worstIndex = 0;
        for (int j = 1; j < elitesFitness.Count; j++)
        {
            if (_fitnessComparer.Compare(elitesFitness[worstIndex], elitesFitness[j]) < 0)
                worstIndex = j;
        }
        return worstIndex;
    }

}