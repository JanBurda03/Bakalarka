public readonly record struct BoxToBePacked
{
    public BoxProperties Box { get; init; }

    public Rotation Rotation { get; init; }

    public PlacementHeuristic PlacementHeuristic { get; init; }

    public BoxToBePacked(BoxProperties boxProperties,  Rotation rotation, PlacementHeuristic placementHeuristic)
    {
        this.Box = boxProperties;
        this.Rotation = rotation;
        this.PlacementHeuristic = placementHeuristic;
    }

    public PackedBox ToPackedBox(PlacementInfo placementInfo)
    {

        if (placementInfo.OccupiedRegion.GetSizes() != GetRotatedSizes())
        {
            throw new Exception("The sizes of the occupied region do not correspond to the sizes of the rotated box!");
        }
        return new PackedBox(placementInfo, Box, Rotation);
    }

    public Sizes GetRotatedSizes()
    {
        return Box.Sizes.GetRotatedSizes(Rotation);
    }
}
