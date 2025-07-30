public interface IEvolutionBlock<T> where T : IComparable<T>
{
    public IReadOnlyList<T> NextPartialGeneration(IReadOnlyList<T> CurrentEvaluatedPopulation);
}