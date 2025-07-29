public interface IEvolutionBlock<T, U> where U : IComparable<U>
{
    public IReadOnlyList<EvaluatedIndividiual<T, U>> NextPartialGeneration(IReadOnlyList<EvaluatedIndividiual<T, U>> CurrentEvaluatedPopulation);
}