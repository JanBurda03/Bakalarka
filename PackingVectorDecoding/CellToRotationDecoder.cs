public class CellToAllRotationsDecoder: IPackingVectorCellDecoder<Rotation>
{
    private Rotation[] Rotations;
    public CellToAllRotationsDecoder()
    {
        Rotations = Enum.GetValues<Rotation>();
    }

    public Rotation Decode(PackingVectorCell cell)
    {
        double doubleValue = cell;
        int index = Math.Min((int)(doubleValue * Rotations.Length), Rotations.Length - 1);
        return Rotations[index];
    }
}

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