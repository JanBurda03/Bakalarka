public interface IUniformMutator<T>
{
    public T Mutate(T a, T b);
}

public class PackingVectorUniformMutator: IUniformMutator<PackingVector>
{
    private double Prob { get; init; }
    private Random random { get; init; }

    public PackingVectorUniformMutator(double prob)
    {
        Prob = prob;
        random = new Random();
    }

    public PackingVector Mutate(PackingVector a, PackingVector b)
    {
        if (a.Count != b.Count)
            throw new ArgumentException("PackingVectors must have the same length!");

        double[] result = new double[a.Count];
        for (int i = 0; i < a.Count; i++)
        {
            if (random.NextDouble() < Prob)
            {
                result[i] = a[i];
            }
            else
            {
                result[i] = b[i];
            }

        }
        return new PackingVector(result);
    }
}