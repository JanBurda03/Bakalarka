public class CellToAllRotationsDecoder : CellToMultipleRotationsDecoderBase
{
    public CellToAllRotationsDecoder() : base(Enum.GetValues<Rotation>()) { }
}
