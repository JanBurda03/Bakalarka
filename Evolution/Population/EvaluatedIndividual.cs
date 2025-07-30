
public readonly record struct EvaluatedIndividual<T, U>: IComparable<EvaluatedIndividual<T, U>> where U : IComparable<U>
{
    public T Item { get; init; }
    public U Fitness { get; init; }

    public EvaluatedIndividual(T item, U fitness)
    {
        Item = item;
        Fitness = fitness;
    }

    public int CompareTo(EvaluatedIndividual<T, U> other)
    {
        return Fitness.CompareTo(other.Fitness);
    }
}

