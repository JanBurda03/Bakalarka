
using System.Text.Json;

public record class PackingInput(ContainerProperties ContainerProperties, List<BoxProperties> BoxesProperties);

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


