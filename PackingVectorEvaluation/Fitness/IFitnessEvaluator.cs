
public interface IFitnessEvaluator<T>
{
    public double EvaluateFitness(T t);
}

public interface IMultipleFitnessEvaluator<T>: IFitnessEvaluator<T>
{
    public IReadOnlyList<double> EvaluateFitnesses(IReadOnlyList<T> t);
}