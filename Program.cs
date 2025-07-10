static class Program
{
    static void Main()
    {
        PackingInput data = PackingInputLoader.LoadFromFile("input.JSON");

        ContainerProperties contProp = data.ContainerProperties;

        

        List<ItemProperties> itemsProp = data.ItemsProperties;


        List<ItemToBePacked> items = (from ItemProperties itemProperties in itemsProp select new ItemToBePacked(new OrientedItem(itemProperties, Orientation.XYZ), PlacementHeuristics.FirstFit)).ToList();

        PackingInstance packing = new PackingInstance(contProp, items);

        packing.PackAll();

        List<Container> containers = packing.GetContainers();

        PackingOutputSaver.SaveToFile(containers, "output.JSON");
    }
}
