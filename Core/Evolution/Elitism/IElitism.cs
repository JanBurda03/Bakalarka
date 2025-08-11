public interface IElitism<T> where T : IComparable<T>
{
    public int NumberOfElites { get; init; }
    public bool Minimizing { get; init; }
    public IReadOnlyList<T> GetElites(IReadOnlyList<T> list);
}

