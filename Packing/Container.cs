public class Container
{
    public int ID { get; init; }

    public int CurrentWeight { get; private set; }

    public int OccupiedVolume { get; private set; }

    private EmptyMaximalRegions EmptyMaximalRegions { get; }

    public ContainerProperties ContainerProperties { get; }

    private List<PackedBox> _packedBoxes { get; set; }


    public IReadOnlyList<PackedBox> PackedBoxes => _packedBoxes;

    public Container(int iD, ContainerProperties containerProperties)
    {
        ID = iD;
        CurrentWeight = 0;
        OccupiedVolume = 0;
        ContainerProperties = containerProperties;
        EmptyMaximalRegions = new EmptyMaximalRegions(ContainerProperties.Dimension.ToSpace());

        _packedBoxes = new List<PackedBox>();
    }

    public void PackBox(BoxToBePacked boxToBePacked, Region occupiedRegion)
    {
        PackedBox packedBox = boxToBePacked.ToPackedBox(occupiedRegion, ID);

        CurrentWeight += packedBox.BoxProperties.Weight;

        if (CurrentWeight > ContainerProperties.MaxWeight)
        {
            throw new Exception("Maximum weight has been exceeded!");
        }

        EmptyMaximalRegions.UpdateEMR(occupiedRegion);

        _packedBoxes.Add(packedBox);

        OccupiedVolume += occupiedRegion.GetVolume();
    }

    public ContainerDataForHeuristics GetDataForHeuristics() 
    {
        return new ContainerDataForHeuristics(ID, CurrentWeight, OccupiedVolume, EmptyMaximalRegions.GetEMR(), PackedBoxes, ContainerProperties);
    }
}

public readonly record struct ContainerDataForHeuristics
{
    public int ID { get; init; }
    public int CurrentWeight { get; init; }

    public int OccupiedVolume { get; init; }

    public IReadOnlyList<Region> EMR {get; init; }

    public IReadOnlyList<PackedBox> PackedBoxes { get; init; }


    public ContainerProperties ContainerProperties { get; init; }

    public ContainerDataForHeuristics(int id, int currentWeight, int occupiedVolume, IReadOnlyList<Region> emr, IReadOnlyList<PackedBox> packedBoxes, ContainerProperties containerProperties)
    {
        ID = id;
        CurrentWeight = currentWeight;
        OccupiedVolume = occupiedVolume;
        EMR = emr;
        ContainerProperties = containerProperties;
        PackedBoxes = packedBoxes;

    }
}
