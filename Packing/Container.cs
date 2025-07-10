public class Container
{
    public readonly int ID;

    public int CurrentWeight;

    public int OccupiedVolume;

    public readonly EmptyMaximalSpaces EmptyMaximalSpaces;

    public readonly ContainerProperties ContainerProperties;

    public readonly List<PackedItem> PackedItems;

    public Container(int iD, ContainerProperties containerProperties)
    {
        ID = iD;
        CurrentWeight = 0;
        OccupiedVolume = 0;
        ContainerProperties = containerProperties;
        EmptyMaximalSpaces = new EmptyMaximalSpaces(ContainerProperties.Dimension.ToSpace());

        PackedItems = new List<PackedItem>();
    }

    public void PackItem(ItemToBePacked itemToBePacked, Coordinates coordinates)
    {
        Console.WriteLine($"Trying to pack item into container {ID} at {coordinates.ToString()}");

        PackedItem packedItem = itemToBePacked.ToPackedItem(coordinates, ID);

        CurrentWeight += packedItem.Item.ItemProperties.Weight;

        if (CurrentWeight > ContainerProperties.MaxWeight)
        {
            throw new Exception("Maximum weight has been exceeded!");
        }

        Space newOccupied = packedItem.GetOccupiedSpace();

        Console.WriteLine($"Item will ocupy space {newOccupied.ToString()}");

        EmptyMaximalSpaces.updateEmptyMaximalSpaces(newOccupied);

        PackedItems.Add(packedItem);

        OccupiedVolume += packedItem.Item.ItemProperties.Dimension.GetVolume();

        Console.WriteLine($"Item succesfully packed into container {ID} at {coordinates.ToString()}");
    }
}
