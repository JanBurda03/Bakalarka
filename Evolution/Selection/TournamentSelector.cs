
public class TournamentSelector<T> : ISelector<T> where T : IComparable<T>
{
    private bool Minimizing { get; init; }
    private Random Random { get; init; }
    private int NumberToSelect { get; init; }
    private int TournamentSize { get; init; }

    public TournamentSelector(int numberToSelect, int tournamentSize, bool minimizing)
    {
        NumberToSelect = numberToSelect;
        TournamentSize = tournamentSize;
        Minimizing = minimizing;
        Random = new Random();
    }

    public IReadOnlyList<T> Select(IReadOnlyList<T> evaluatedPopulation)
    {
        T[] selected = new T[NumberToSelect];

        for (int i = 0; i < NumberToSelect; i++)
        {
            selected[i] = SelectOne(evaluatedPopulation);
        }
        return selected;
    }

    private T SelectOne(IReadOnlyList<T> evaluatedPopulation)
    {
        int bestIndex = Random.Next(evaluatedPopulation.Count);

        for (int j = 1; j < TournamentSize; j++)
        {
            int challengerIndex = Random.Next(evaluatedPopulation.Count);

            if (IsBetter(evaluatedPopulation[challengerIndex], evaluatedPopulation[bestIndex]))
            {
                bestIndex = challengerIndex;
            }
        }

        return evaluatedPopulation[bestIndex];
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
