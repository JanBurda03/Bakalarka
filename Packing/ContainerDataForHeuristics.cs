public readonly record struct ContainerDataForHeuristics
{
    public int ID { get; init; }
    public long CurrentWeight { get; init; }

    public long OccupiedVolume { get; init; }

    public IReadOnlyList<Region> EMR { get; init; }

    public IReadOnlyList<PackedBox> PackedBoxes { get; init; }


    public ContainerProperties ContainerProperties { get; init; }

    public ContainerDataForHeuristics(int id, long currentWeight, long occupiedVolume, IReadOnlyList<Region> emr, IReadOnlyList<PackedBox> packedBoxes, ContainerProperties containerProperties)
    {
        ID = id;
        CurrentWeight = currentWeight;
        OccupiedVolume = occupiedVolume;
        EMR = emr;
        ContainerProperties = containerProperties;
        PackedBoxes = packedBoxes;

    }
}

