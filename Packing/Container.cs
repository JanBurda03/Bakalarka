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
        EmptyMaximalRegions = new EmptyMaximalRegions(ContainerProperties.Sizes.ToRegion(new Coordinates(0,0,0)));

        _packedBoxes = new List<PackedBox>();
    }

    public void PackBox(BoxToBePacked boxToBePacked, PlacementInfo placementInfo)
    {
        if (ID != placementInfo.ContainerID) 
        {
            throw new Exception($"The item is supposed to be packed to container {placementInfo.ContainerID}, this is container {ID}");
        }

        PackedBox packedBox = boxToBePacked.ToPackedBox(placementInfo);

        CurrentWeight += packedBox.BoxProperties.Weight;

        if (CurrentWeight > ContainerProperties.MaxWeight)
        {
            throw new Exception("Maximum weight has been exceeded!");
        }

        EmptyMaximalRegions.UpdateEMR(placementInfo.OccupiedRegion);

        _packedBoxes.Add(packedBox);

        OccupiedVolume += placementInfo.OccupiedRegion.GetVolume();
    }

    public ContainerDataForHeuristics GetDataForHeuristics()
    {
        return new ContainerDataForHeuristics(ID, CurrentWeight, OccupiedVolume, EmptyMaximalRegions.GetEMR(), PackedBoxes, ContainerProperties);
    }
}


