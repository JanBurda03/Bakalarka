public interface ISelector<T> where T : IComparable<T>
{
    public IReadOnlyList<T> Select(IReadOnlyList<T> list);
}