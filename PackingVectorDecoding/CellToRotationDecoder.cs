public abstract class CellToMultipleRotationsDecoderbase : IPackingVectorCellDecoder<Rotation>
{
    protected readonly Rotation[] rotations;

    protected CellToMultipleRotationsDecoderbase(Rotation[] rotations)
    {
        this.rotations = rotations;
    }

    public Rotation Decode(PackingVectorCell cell)
    {
        int index = Math.Min((int)((double)cell * rotations.Length), rotations.Length - 1);
        return rotations[index];
    }
}

public class CellToAllRotationsDecoder : CellToMultipleRotationsDecoderbase
{
    public CellToAllRotationsDecoder() : base(Enum.GetValues<Rotation>()) { }
}

public class CellToMultipleRotationsDecoder : CellToMultipleRotationsDecoderbase
{
    public CellToMultipleRotationsDecoder(Rotation[] rotations) : base(rotations) { }
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