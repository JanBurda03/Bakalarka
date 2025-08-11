public class Elitism<T> : IElitism<T> where T : IComparable<T>
{
    public int NumberOfElites { get; init; }

    public bool Minimizing { get; init; }

    public Elitism(int numberOfElites, bool minimizing)
    {
        NumberOfElites = numberOfElites;
        Minimizing = minimizing;
    }

    public IReadOnlyList<T> GetElites(IReadOnlyList<T> evaluatedPopulation)
    {
        
        if (NumberOfElites == 0)
        {
            return Array.Empty<T>();
        }

        if (evaluatedPopulation.Count <= NumberOfElites)
        {
            return evaluatedPopulation;
        }

        int worstIndex = -1;

        T[] elites = new T[NumberOfElites];

        for (int i = 0; i < evaluatedPopulation.Count; i++)
        {
            if (i < elites.Length)
            {
                elites[i] = evaluatedPopulation[i];
            }
            else
            {
                if (worstIndex == -1)
                {
                    worstIndex = 0;
                    for (int j = 1; j < elites.Length; j++)
                    {
                        if (IsBetter(elites[worstIndex], elites[j]))
                            worstIndex = j;
                    }
                }

                if (IsBetter(evaluatedPopulation[i], elites[worstIndex]))
                {
                    elites[worstIndex] = evaluatedPopulation[i];
                    worstIndex = -1;
                }
            }
        }

        return Array.AsReadOnly(elites);
    }

    private bool IsBetter(T candidate, T currentBest)
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