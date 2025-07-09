public class Container
{
    int ID;

    int CurrentWeight;

    int OccupiedVolume;

    EmptyMaximalSpaces EmptyMaximalSpaces;

    ContainerProperties ContainerProperties;

    List<PackedItem> PackedItems;

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
        PackedItem packedItem = itemToBePacked.PackAt(coordinates, ID);

        CurrentWeight += packedItem.Item.ItemProperties.Weight;

        if (CurrentWeight > ContainerProperties.MaxWeight)
        {
            throw new Exception("Maximum weight has been exceeded!");
        }

        EmptyMaximalSpaces.updateEmptyMaximalSpaces(packedItem.GetOccupiedSpace());

        PackedItems.Add(packedItem);

        OccupiedVolume += packedItem.Item.ItemProperties.Dimension.GetVolume();

    }


}