
public class PackingVectorSolver : IPackingVectorSolver
{
    private IPackingVectorDecoder PackingVectorDecoder { get; init; }
    private IBoxPacker BoxPacker { get; init; }
    private PackingInput PackingInput { get; init; }
    public PackingVectorSolver(IPackingVectorDecoder packingVectorDecoder, IBoxPacker boxPacker, PackingInput packingInput)
    {
        PackingVectorDecoder = packingVectorDecoder;
        BoxPacker = boxPacker;
        PackingInput = packingInput;
    }
    public IReadOnlyList<Container> Solve(PackingVector packingVector)
    {
        BoxToBePacked[] boxesToBePacked = PackingVectorDecoder.Decode(packingVector, PackingInput.BoxesProperties);
        BoxPacker.PackBoxes(boxesToBePacked);
        var containers =  BoxPacker.GetContainers();
        BoxPacker.Reset();
        return containers;
    }

    public IPackingVectorSolver ParallelSafeClone()
    {
        return new PackingVectorSolver(PackingVectorDecoder, BoxPacker.ParallelSafeClone(), PackingInput); 
    }
}
