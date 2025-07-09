class PackingInstance
{
    List<Container> Containers;

    List<ItemToBePacked> Items;

    public void PackAll()
    {
        Container container;
        Coordinates coordinate;
        PlacementHeuristics heuristics;

        foreach (ItemToBePacked itemToBePacked in Items) 
        { 
            heuristics = itemToBePacked.Heuristics;
            (container, coordinate) = heuristics.WhereToPack(itemToBePacked, Containers);
            container.PackItem(itemToBePacked, coordinate);
        }
    }
}








