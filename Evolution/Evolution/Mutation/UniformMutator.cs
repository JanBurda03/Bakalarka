public class PackingVectorUniformMutator: IUniformMutator<PackingVector>
{
    private readonly double _prob; // probability that the cell comes from the first individual (the second has probability = 1 - _prob);
    private readonly Random _random;

    public PackingVectorUniformMutator(double prob)
    {
        if (prob < 0 || prob > 1)
            throw new ArgumentOutOfRangeException("Probability must be between 0 and 1!");

        _prob = prob;
        _random = new Random();
    }

    public PackingVector Mutate(PackingVector a, PackingVector b)
    {
        if (a.Count != b.Count)
            throw new ArgumentException("PackingVectors must have the same length!");

        if (a.Equals(b))
        {
            b = PackingVector.CreateRandom(a.Count);
        }


        double[] result = new double[a.Count];
        for (int i = 0; i < a.Count; i++)
        {
            // uniform mutation means that every cell in the new individual is chosen from one of the two parents with certain probability
            if (_random.NextDouble() < _prob)
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