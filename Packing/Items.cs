public enum Orientation
{
    XYZ,
    XZY,
    YXZ,
    YZX,
    ZXY,
    ZYX
}


public record struct PackedItem
{
    public Coordinates ItemCoordinates;

    public int ContainerID;

    public OrientedItem Item;

    public PackedItem(OrientedItem item, Coordinates itemCoordinates, int containerID)
    {
        ItemCoordinates = itemCoordinates;
        Item = item;
        ContainerID = containerID;
    }

    public Space GetOccupiedSpace()
    {
        Dimensions itemDimension = Item.GetRotatedItemDimensions();
        Coordinates itemEndCoordinates = new Coordinates(ItemCoordinates.X + itemDimension.X, ItemCoordinates.Y + itemDimension.Y, ItemCoordinates.Z + itemDimension.Z);

        return new Space(ItemCoordinates, itemEndCoordinates);
    }
}

public record struct OrientedItem
{
    public ItemProperties ItemProperties;

    public Orientation ItemOrientation;

    public Dimensions GetRotatedItemDimensions()
    {
        int x = ItemProperties.Dimension.X;
        int y = ItemProperties.Dimension.Y;
        int z = ItemProperties.Dimension.Z;

        return ItemOrientation switch
        {
            Orientation.XYZ => new Dimensions(x, y, z),
            Orientation.XZY => new Dimensions(x, z, y),
            Orientation.YXZ => new Dimensions(y, x, z),
            Orientation.YZX => new Dimensions(y, z, x),
            Orientation.ZXY => new Dimensions(z, x, y),
            Orientation.ZYX => new Dimensions(z, y, x),
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}

public record struct ItemToBePacked
{
    public OrientedItem Item;

    public PlacementHeuristics Heuristics;

    public PackedItem PackAt(Coordinates coordinates, int ContainerID)
    {
        return new PackedItem(Item, coordinates, ContainerID);
    }
}