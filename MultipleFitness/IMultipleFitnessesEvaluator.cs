
public interface IMultipleFitnessesEvaluator<T, U> where U : IComparable<U>
{
    public U[] EvaluateFitnesses(IReadOnlyList<T> packingVectors);
}