public class PackingVectorSolver
{
    private PackingVectorDecoder PackingVectorDecoder { get; init; }
    private PackingInput PackingInput { get; init; }
    public PackingVectorSolver(PackingVectorDecoder packingVectorDecoder, PackingInput packingInput)
    {
        PackingVectorDecoder = packingVectorDecoder;
        PackingInput = packingInput;
    }
    public IReadOnlyList<ContainerData> Solve(PackingVector packingVector)
    {
        
        IReadOnlyList<BoxToBePacked> boxesToBePacked = PackingVectorDecoder.Decode(packingVector);

        IBoxPacker BoxPacker = new BoxPacker(PackingInput.ContainerProperties);
        BoxPacker.PackBoxes(boxesToBePacked);
        var containers =  BoxPacker.ContainersData;


        return containers;
    }

    public static PackingVectorSolver CreateSolver(PackingInput packingInput, PackingSetting packingSetting)
    {
        PackingVectorDecoder packingVectorDecoder = PackingVectorDecoder.Create(packingSetting, packingInput);
        return new PackingVectorSolver(packingVectorDecoder, packingInput);
    }
}

