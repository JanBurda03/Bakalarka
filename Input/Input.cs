
using System.Text.Json;

public record class PackingInput(ContainerProperties Container, List<ItemProperties> Items);

public record class ContainerProperties(Dimensions Dimension, int MaxWeight);

public record class ItemProperties(int Id, Dimensions Dimension, int Weight);

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

    static void Main()
    {
        Console.WriteLine("ahoj");
    }
}


