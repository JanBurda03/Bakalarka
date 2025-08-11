public record class PackingInput
{
    public ContainerProperties ContainerProperties { get; init; }
    public IReadOnlyList<BoxProperties> BoxPropertiesList { get; init; }

    public PackingInput(ContainerProperties containerProperties, IReadOnlyList<BoxProperties> boxPropertiesList)
    {
        ContainerProperties = containerProperties;
        BoxPropertiesList = boxPropertiesList;
    }
}

public record class ContainerProperties(Sizes Sizes, int MaxWeight);

public record class BoxProperties(int Id, Sizes Sizes, int Weight);