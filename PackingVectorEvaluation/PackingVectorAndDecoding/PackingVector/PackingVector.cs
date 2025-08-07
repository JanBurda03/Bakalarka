using System.Collections;

public readonly struct PackingVector:IReadOnlyList<double>
{
    private PackingVectorCell[] Vector { get; init; }
    private int SectionLength => Vector.Length/3;

    public int Count => Vector.Length;

    public double this[int index] => Vector[index];

    public PackingVector(IReadOnlyList<double> vector)
    {
        if(vector.Count%3!=0)
        {
            throw new ArgumentException("The Packing Vector must have length divisible by 3!");
        }

        Vector = new PackingVectorCell[vector.Count];
        for (int i = 0; i < vector.Count; i++)
        {
            Vector[i] = (PackingVectorCell)vector[i];
        }
    }

    public ReadOnlySpan<PackingVectorCell> GetBoxSortingPart()
    {
        return new ReadOnlySpan<PackingVectorCell>(Vector, 0, SectionLength);
    }

    public ReadOnlySpan<PackingVectorCell> GetRotationPart()
    {
        return new ReadOnlySpan<PackingVectorCell>(Vector, SectionLength, SectionLength);
    }

    public ReadOnlySpan<PackingVectorCell> GetPlacementHeuristicPart()
    {
        return new ReadOnlySpan<PackingVectorCell>(Vector, 2 * SectionLength, SectionLength);
    }

    public static PackingVector GenerateRandom(int length)
    {
        Random random = new Random();
        var vector = new double[length];
        for (int i = 0; i < length; i++)
        {
            vector[i] = random.NextDouble();
        }
        return new PackingVector(vector);
        
    }

    public IEnumerator<double> GetEnumerator()
    {
        for (int i = 0; i < Vector.Length; i++)
        {
            yield return Vector[i];
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}