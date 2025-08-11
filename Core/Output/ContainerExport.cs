public record class ContainerExport(
    int ContainerID,
    long CurrentWeight,
    long OccupiedVolume,
    IReadOnlyList<PackedBox> PackedBoxes
);


public static class ContainerExtensionForExport
{
    public static ContainerExport ExportContainer(this ContainerData container) 
    {
        return new ContainerExport(container.ID, container.CurrentWeight, container.OccupiedVolume, container.PackedBoxes);
    }
}

