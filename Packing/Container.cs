public class Container
{
    public int ID { get; init; }

    public long CurrentWeight { get; private set; }

    public long OccupiedVolume { get; private set; }

    private EmptyMaximalRegions EmptyMaximalRegions { get; }

    public ContainerProperties ContainerProperties { get; }

    private List<PackedBox> _packedBoxes { get; set; }


    private ContainerDataForHeuristics? _data { get; set; }
    public ContainerDataForHeuristics Data
    {
        get
        {
            if (_data == null)
            {
                _data = new ContainerDataForHeuristics(
                    ID,
                    CurrentWeight,
                    OccupiedVolume,
                    EmptyMaximalRegions.GetEMR(),
                    PackedBoxes,
                    ContainerProperties
                );
            }
            return (ContainerDataForHeuristics)_data;
        }
    }

    public IReadOnlyList<PackedBox> PackedBoxes => _packedBoxes;

    public Container(int iD, ContainerProperties containerProperties)
    {
        ID = iD;
        CurrentWeight = 0;
        OccupiedVolume = 0;
        ContainerProperties = containerProperties;
        EmptyMaximalRegions = new EmptyMaximalRegions(ContainerProperties.Sizes.ToRegion(new Coordinates(0,0,0)));
        _data = null;
        _packedBoxes = new List<PackedBox>();
    }

    public void PackBox(BoxToBePacked boxToBePacked, PlacementInfo placementInfo)
    {
        if (ID != placementInfo.ContainerID) 
        {
            throw new Exception($"The item is supposed to be packed to container {placementInfo.ContainerID}, this is container {ID}");
        }

        _data = null;

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

    public double GetRelativeVolume()
    {
        return (double)OccupiedVolume /ContainerProperties.Sizes.GetVolume();
    }

    public double GetRelativeWeight()
    {
        return (double)CurrentWeight /ContainerProperties.MaxWeight;
    }
}


