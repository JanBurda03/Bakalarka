public readonly struct PackingVector
{
    private PackingVectorCell[] Vector { get; init; }
    public int Length => Vector.Length;
    public int SectionLength => Vector.Length/3;
    public PackingVector(PackingVectorCell[] vector)
    {
        if(vector.Length%3!=0)
        {
            throw new ArgumentException("The Packing Vector must have length divisible by 3!");
        }
        Vector = vector;
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

    public static PackingVector GenerateRandomPackingVector(int length)
    {
        Random random = new Random();
        var vector = new PackingVectorCell[length];
        for (int i = 0; i < length; i++)
        {
            vector[i] = ((PackingVectorCell)random.NextDouble());
        }
        return new PackingVector(vector);
        
    }
}