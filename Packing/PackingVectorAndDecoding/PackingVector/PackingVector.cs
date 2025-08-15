
public readonly struct PackingVector
{
    private PackingVectorCell[] Vector { get; init; }

    public int Count => Vector.Length;

    public PackingVectorCell this[int index] => Vector[index];

    public PackingVector(IReadOnlyList<double> vector)
    {
        Vector = new PackingVectorCell[vector.Count];
        for (int i = 0; i < vector.Count; i++)
        {
            Vector[i] = (PackingVectorCell)vector[i];
        }
    }

    public PackingVector Slice(int start, int length)
    {
        if (start < 0 || length < 0 || start + length > Count)
        {
            throw new ArgumentOutOfRangeException();
        }
        
        double[] slice = new double[length];
        for (int i = 0; i < length; i++)
        {
            slice[i] = Vector[start + i];
        }
        return new PackingVector(slice);
    }


    public static PackingVector CreateRandom(int length)
    {
        Random random = Random.Shared;
        var vector = new double[length];
        for (int i = 0; i < length; i++)
        {
            vector[i] = random.NextDouble();
        }
        return new PackingVector(vector); 
    }

    public static PackingVector CreateEmpty()
    {
        return new PackingVector(Array.Empty<double>());
    }

    public static PackingVector CreateZeros(int length)
    {
        return new PackingVector(new double[length]);
    }

    public static implicit operator double[](PackingVector vector)
    {
        double[] doubles = new double[vector.Count];
        for (int i = 0; i < vector.Count; i++)
        {
            doubles[i] = vector[i];
        }
        return doubles;
    }

    public static implicit operator List<double>(PackingVector vector)
    {
        var doubles = new List<double>(vector.Count);
        for (int i = 0; i < vector.Count; i++)
        {
            doubles.Add(vector[i]);
        }
        return doubles;
    }


    public static explicit operator PackingVector(double[] doubles)
    {
        return new PackingVector(doubles);
    }

    public static explicit operator PackingVector(List<double> doubles)
    {
        return new PackingVector(doubles);
    }
}