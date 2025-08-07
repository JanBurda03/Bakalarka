
public class PackingVectorSolver : IPackingVectorSolver
{
    private PackingVectorDecoder PackingVectorDecoder { get; init; }
    private PackingInput PackingInput { get; init; }
    public PackingVectorSolver(PackingVectorDecoder packingVectorDecoder, PackingInput packingInput)
    {
        PackingVectorDecoder = packingVectorDecoder;
        PackingInput = packingInput;
    }
    public IReadOnlyList<Container> Solve(PackingVector packingVector)
    {
        
        BoxToBePacked[] boxesToBePacked = PackingVectorDecoder.Decode(packingVector, PackingInput.BoxesProperties);

        IBoxPacker BoxPacker = new BoxPacker(PackingInput.ContainerProperties);
        BoxPacker.PackBoxes(boxesToBePacked);
        var containers =  BoxPacker.GetContainers();


        return containers;
    }
}

