
public readonly record struct EvaluatedIndividiual<T, U> where U : IComparable<U>
{
    public T Item { get; init; }
    public U Fitness { get; init; }

    public EvaluatedIndividiual(T item, U fitness)
    {
        Item = item;
        Fitness = fitness;
    }
}
