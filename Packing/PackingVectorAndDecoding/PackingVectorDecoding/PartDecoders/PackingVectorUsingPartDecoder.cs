internal class PackingVectorUsingPartDecoder<T> : IPackingVectorPartDecoder<T>
{
    public bool IsUsingPackingVector { get; init; }
    private readonly IReadOnlyList<T> _possibilities;
    public PackingVectorUsingPartDecoder(IReadOnlyList<T> possibilities)
    {
        _possibilities = possibilities;
        IsUsingPackingVector = true;
    }

    public IReadOnlyList<T> DecodeMultiple(PackingVector packingVector)
    {
        T[] decoded = new T[packingVector.Count];
        for (int i = 0; i < decoded.Length; i++)
        {
            decoded[i] = Decode(packingVector[i]);
        }
        return decoded;
    }

    public T Decode(PackingVectorCell packingVectorCell)
    {
        int index = Math.Min((int)((double)packingVectorCell * _possibilities.Count), _possibilities.Count - 1);
        return _possibilities[index];
    }
}