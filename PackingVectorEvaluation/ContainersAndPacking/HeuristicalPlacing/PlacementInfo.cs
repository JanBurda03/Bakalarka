public readonly record struct PlacementInfo
{
    public int ContainerID { get; init; }
    public Region OccupiedRegion { get; init; }

    public PlacementInfo(int containerID, Region occupiedRegion)
    {
        ContainerID = containerID;
        OccupiedRegion = occupiedRegion;
    }
}