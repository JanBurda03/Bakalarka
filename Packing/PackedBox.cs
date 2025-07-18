public readonly record struct PackedBox
{
    public int ContainerID { get; init; }

    public Region OccupiedRegion { get; init; }

    public BoxProperties BoxProperties { get; init; }

    public Rotation Rotation { get; init; }

    public PackedBox(int containerID, Region occupiedRegion, BoxProperties boxProperties, Rotation rotation)
    {
        ContainerID = containerID;
        OccupiedRegion = occupiedRegion;
        BoxProperties = boxProperties;
        Rotation = rotation;
    }
}