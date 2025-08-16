public interface IPackingVectorPartDecoder<T>: IPackingVectorUsing
{
    public T Decode(PackingVectorCell cell);
    public IReadOnlyList<T> DecodeMultiple(PackingVector packingVector);
}

