public class CellToOneRotationDecoder : IPackingVectorCellDecoder<Rotation>
{
    private Rotation Rotation { get; init; }
    public CellToOneRotationDecoder(Rotation rotation)
    {
        Rotation = rotation;
    }
    public Rotation Decode(PackingVectorCell cell)
    {
        return Rotation;
    }
}