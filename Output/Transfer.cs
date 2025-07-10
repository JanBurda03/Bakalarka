public record PackedItemExport(
    int ItemID,
    Coordinates Coordinates,
    string Orientation,
    int ContainerID
);

public record class ContainerExport(
    int ContainerID,
    int CurrentWeight,
    int OccupiedVolume,
    List<PackedItemExport> PackedItems
);

public static class PackedItemExtension
{
    public static PackedItemExport ExportPackedItem(this PackedItem packedItem)
    {
        return new PackedItemExport(packedItem.Item.ItemProperties.Id, packedItem.ItemCoordinates, packedItem.Item.ItemOrientation.ToString(), packedItem.ContainerID);
    }
}

public static class ContainerExtension
{
    public static ContainerExport ExportContainer(this Container container) 
    {
        List <PackedItemExport> packedItemExports = (from PackedItem packedItem in container.PackedItems select packedItem.ExportPackedItem()).ToList();
        return new ContainerExport(container.ID, container.CurrentWeight, container.OccupiedVolume, packedItemExports);
    }
}

