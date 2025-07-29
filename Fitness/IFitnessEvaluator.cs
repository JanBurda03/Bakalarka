
public interface IFitnessEvaluator<T, U> where U : IComparable<U>
{
    public U EvaluateFitness(T t);
}