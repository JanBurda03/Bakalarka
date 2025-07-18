public record class ContainerExport(
    int ContainerID,
    int CurrentWeight,
    int OccupiedVolume,
    IReadOnlyList<PackedBox> PackedBoxes
);


public static class ContainerExtension
{
    public static ContainerExport ExportContainer(this Container container) 
    {
        return new ContainerExport(container.ID, container.CurrentWeight, container.OccupiedVolume, container.PackedBoxes);
    }
}

