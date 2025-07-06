class ContainerInstance
{
    Container ContainerProperties;

    List<PlacedItem> PlacedItems;
}

record struct PlacedItem(Coordinates ItemCoordinates, Item ItemProperties);





/*

class BinPacking
{
    List<ContainerInstance> Containers;

    EmptySpaces ES;

    public 
}

*/