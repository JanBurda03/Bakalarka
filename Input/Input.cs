
using System.Text.Json;

public record class PackingInput
{
    public ContainerProperties ContainerProperties { get; init; }
    public BoxProperties[] BoxesProperties { get; init; }

    public PackingInput(ContainerProperties containerProperties, BoxProperties[] boxesProperties)
    {
        ContainerProperties = containerProperties;
        BoxesProperties = boxesProperties;
    }

    public int GetLowerBound()
    {
        long boxesWeight = 0;
        long boxesVolume = 0;

        foreach (BoxProperties boxProperties in BoxesProperties)
        {
            boxesWeight += boxProperties.Weight;
            boxesVolume += boxProperties.Sizes.GetVolume();
        }

        double value = Math.Max((double)boxesVolume / ContainerProperties.Sizes.GetVolume(), (double)boxesWeight / ContainerProperties.MaxWeight);

        return (int)Math.Ceiling(value);


    }
}

public record class ContainerProperties(Sizes Sizes, int MaxWeight);

public record class BoxProperties(int Id, Sizes Sizes, int Weight);

public static class PackingInputLoader
{
    public static PackingInput LoadFromFile(string fileName)
    {
        string json = File.ReadAllText(fileName);
        PackingInput? input = JsonSerializer.Deserialize<PackingInput>(json);
        if (input == null)
            throw new Exception("JSON deserialize error!");
        return input;
    }
}


