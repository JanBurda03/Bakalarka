class PackingInstance
{
    private ContainerProperties ContainerProperties;

    private List<Container> Containers;

    private List<ItemToBePacked> ItemsToBePacked;

    public PackingInstance(ContainerProperties containerProperties, List<ItemToBePacked> items)
    {
        ContainerProperties = containerProperties;
        Containers = new List<Container>();
        ItemsToBePacked = items;
    }

    public void PackAll()
    {
        Container? container;
        Coordinates coordinate;

        foreach (ItemToBePacked itemToBePacked in ItemsToBePacked) 
        {
            Console.WriteLine();
            Console.WriteLine($"Trying to pack item {itemToBePacked.Item.ItemProperties.Id}");

            (container, coordinate) = itemToBePacked.WhereToPackHeuristics(itemToBePacked, Containers);
            if (container != null)
            {
                Console.WriteLine($"Using heuristics {itemToBePacked.WhereToPackHeuristics.Method.Name}, a beautiful new place for the item was found in container {container.ID} at {coordinate.ToString()}");
                container.PackItem(itemToBePacked, coordinate);
            }
            else
            {
                Console.WriteLine($"Using heuristics, no place for the item could be found, so new container is being added");

                AddContainer();

                (container, coordinate) = itemToBePacked.WhereToPackHeuristics(itemToBePacked, Containers);
                if (container != null)
                {
                    Console.WriteLine($"After adding new container, using heuristics, a beautiful new place for the item was found in container {container.ID} at {coordinate.ToString()}");
                    container.PackItem(itemToBePacked, coordinate);
                }
                else
                {
                    throw new Exception($"Item {itemToBePacked.Item.ItemProperties.Id} is too large!");
                }
            }
        }

        ItemsToBePacked.Clear();

    }

    private void AddContainer()
    {
        Container newContainer = new Container(Containers.Count, ContainerProperties);
        Containers.Add(newContainer);

        Console.WriteLine($"New container with ID {newContainer.ID} was added");
        Console.WriteLine($"Current number of containers is {Containers.Count}");
    }

    public void AddItems(List<ItemToBePacked> newItems)
    {
        ItemsToBePacked.AddRange(newItems);
        Console.WriteLine("New items have been added for packing");
    }

    public List<Container> GetContainers()
    {
        Console.WriteLine("Returning containers...");
        if (ItemsToBePacked.Count != 0)
        {
            Console.WriteLine("Warning! Not all items have been packed, the returned containers do not container all items");
        }
        return Containers;
    }
}








