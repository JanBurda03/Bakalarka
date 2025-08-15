
public class FitnessComparer<T> : IComparer<T> where T : IComparable<T>
{
    private readonly bool _minimizing;

    public FitnessComparer(bool minimizing)
    {
        _minimizing = minimizing;
    }

    public int Compare(T x, T y)
    {
        int comparison = x.CompareTo(y);
        return _minimizing ? comparison : -comparison;
    }
}