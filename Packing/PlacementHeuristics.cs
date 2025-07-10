public delegate (Container?, Coordinates) WhereToPackHeuristics(ItemToBePacked item, IEnumerable<Container> containers);

public static class PlacementHeuristics
{
    public static (Container?, Coordinates) FirstFit(ItemToBePacked item, IEnumerable<Container> containers)
    {
        foreach (var container in containers)
        {
            if (container.CurrentWeight + item.Item.ItemProperties.Weight <= container.ContainerProperties.MaxWeight)
            {
                IEnumerable<Space> EMS = container.EmptyMaximalSpaces.getEmptyMaximalSpaces(item.Item.GetRotatedItemDimensions());

                if (EMS.Any())
                {
                    Space space = EMS.First();
                    return (container, space.Start);
                }
            }
        }
        return (null, new Coordinates(0,0,0));
    }
}