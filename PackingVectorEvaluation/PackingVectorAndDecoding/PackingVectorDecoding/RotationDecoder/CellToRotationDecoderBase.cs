public abstract class CellToMultipleRotationsDecoderBase : IPackingVectorCellDecoder<Rotation>
{
    protected readonly IReadOnlyList<Rotation> rotations;

    protected CellToMultipleRotationsDecoderBase(IReadOnlyList<Rotation> rotations)
    {
        this.rotations = rotations;
    }

    public Rotation Decode(PackingVectorCell cell)
    {
        int index = Math.Min((int)((double)cell * rotations.Count), rotations.Count - 1);
        return rotations[index];
    }
}


