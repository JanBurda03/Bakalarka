
using System.Collections.Generic;

public interface ISelector<T>
{
    public IReadOnlyList<T> Select(IReadOnlyList<T> population, IReadOnlyList<IComparable> fitnesses);
}

public class TournamentSelector<T>:ISelector<T>
{
    private bool Minimizing { get; init; }
    private Random Random { get; init; }
    private int NumberToSelect { get; init; }
    private int TournamentSize { get; init;  }

    public IReadOnlyList<T> Select(IReadOnlyList<T> population, IReadOnlyList<IComparable> fitnesses)
    {
        T[] selected = new T[NumberToSelect];

        for (int i = 0; i < NumberToSelect; i++)
        {
            selected[i] = SelectOne(population, fitnesses);
        }
        return selected;
    }

    private T SelectOne(IReadOnlyList<T> population, IReadOnlyList<IComparable> fitnesses)
    {
        int bestIndex = Random.Next(population.Count);

        for (int j = 1; j < TournamentSize; j++)
        {
            int challengerIndex = Random.Next(population.Count);

            if (IsBetter(fitnesses[challengerIndex], fitnesses[bestIndex]))
            {
                bestIndex = challengerIndex;
            }
        }

        return population[bestIndex];
    }

    private bool IsBetter(IComparable candidate, IComparable currentBest)
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

public interface IChildmaker<T>
{
    public IReadOnlyList<T> MakeChildren(IReadOnlyList<T> selected);
}

public class Childmaker : IChildmaker<PackingVector>
{
    public IReadOnlyList<PackingVector> MakeChildren(IReadOnlyList<PackingVector> selected)
    {

    }
}

public class PupulationReproducer<T> : IPopulationReproducer <T>
{
    int Elitism { get; init; }

    public ISelector<T> Selector  {get; init; }
    public IChildmaker<T> Childmaker { get; init; }

    public IReadOnlyList<T> Reproduce(IReadOnlyList<T> population, IReadOnlyList<IComparable> fitnesses)
    {
        IReadOnlyList<T> elites = GetElites(population, fitnesses);
        IReadOnlyList<T> selected = Selector.Select(population, fitnesses);
        IReadOnlyList<T> children = Childmaker.MakeChildren(selected);
        return children.Concat(elites).ToArray();
    }

    public IReadOnlyList<T> GetElites(IReadOnlyList<T> population, IReadOnlyList<IComparable> fitnesses)
    {

    }
}