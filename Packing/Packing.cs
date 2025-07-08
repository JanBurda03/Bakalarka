class PackingInstance
{
    List<ContainerInstance> ContainerInstances;

    

    public void Pack()
    {

    }
}


class ContainerInstance
{
    int CurrentWeight;

    Container ContainerProperties;

    List<PlacedItem> PlacedItems;


}

public enum Orientation
{
    XYZ,
    XZY,
    YXZ,
    YZX,
    ZXY,
    ZYX
}


record struct PlacedItem(Coordinates ItemCoordinates, Item ItemProperties, Orientation ItemOrientation)
{
   
}

record struct ItemToBePacked
{
    Item ItemProperties;

    Orientation ItemOrientation;

    PlacementHeuristics heuristics;

    public Coordinates getPlacedItemDimensions()
    {
        int x = ItemProperties.Width;
        int y = ItemProperties.Depth;
        int z = ItemProperties.Height;

        return ItemOrientation switch
        {
            Orientation.XYZ => new Coordinates(x, y, z),
            Orientation.XZY => new Coordinates(x, z, y),
            Orientation.YXZ => new Coordinates(y, x, z),
            Orientation.YZX => new Coordinates(y, z, x),
            Orientation.ZXY => new Coordinates(z, x, y),
            Orientation.ZYX => new Coordinates(z, y, x),
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}

class PlacementHeuristics;




