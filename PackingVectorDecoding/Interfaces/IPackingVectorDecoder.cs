public interface IPackingVectorDecoder
{
    public BoxToBePacked[] Decode(PackingVector packingVector, BoxProperties[] boxes);
}

