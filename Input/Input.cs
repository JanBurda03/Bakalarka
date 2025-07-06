
using System.Text.Json;

public record class PackingInput(Container Container, List<Item> Items);

public record class Container(int Width, int Height, int Depth, int MaxWeight);

public record class Item(int Id, int Width, int Height, int Depth, int Weight);

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

    }
}


