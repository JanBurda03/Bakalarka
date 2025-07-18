public readonly record struct BoxToBePacked
{
    public BoxProperties Box { get; init; }

    public Rotation Rotation { get; init; }

    public PlacementHeuristic PlacementHeuristic { get; init; }

    public PackedBox ToPackedBox(Region occupiedRegion, int containerID)
    {
        if (occupiedRegion.GetSizes() != GetRotatedSizes())
        {
            throw new Exception("The sizes of the occupied region do not correspond to the sizes of the rotated box!");
        }
        return new PackedBox(containerID, occupiedRegion, Box, Rotation);
    }

    public Sizes GetRotatedSizes()
    {
        return Box.Dimension.GetRotatedSizes(Rotation);
    }
}
