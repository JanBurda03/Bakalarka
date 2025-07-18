public delegate PlacementInfo? PlacementHeuristic(BoxToBePacked boxToBePlaced, IEnumerable<ContainerDataForHeuristics> containersData);

public readonly record struct PlacementInfo
{
    public int ContainerID { get; init; }
    public Region Region { get; init; }

    public PlacementInfo(int containerID, Region region)
    {
        ContainerID = containerID;
        Region = region;
    }
}

public static class PlacementHeuristics
{
    public static PlacementInfo? FirstFit(BoxToBePacked boxToBePlaced, IEnumerable<ContainerDataForHeuristics> containersData)
    {
        foreach (ContainerDataForHeuristics containerData in containersData)
        {  
            if (ValidWeight(boxToBePlaced, containerData))
            {
                foreach(Region region in containerData.EMR)
                {
                    if (ValidSides(boxToBePlaced, region))
                    {
                        return new PlacementInfo(containerData.ID, boxToBePlaced.GetRotatedSizes().ToSpace(region.Start));
                    }
                }
            }     
        }
        return null;
    }


    public static (Container?, Coordinates) CloseToOrigin(ItemToBePacked item, IEnumerable<Container> containers)
    {
        (Container?, Coordinates) returnValue = (null, new Coordinates (0, 0, 0));

        double distance = double.MaxValue;

        Coordinates origin = new Coordinates(0, 0, 0);

        foreach (var container in containers)
        {

            IEnumerable<Region> EMS = container.EmptyMaximalRegions.getEmptyMaximalSpaces(item.Item.GetRotatedItemDimensions());

            foreach (Region space in EMS)
            {
                double newDistance = space.Start.GetEuclidanDistanceTo(origin);

                if (newDistance < distance)
                {
                    distance = newDistance;
                    returnValue = (container, space.Start);
                }
            }

        }
        return returnValue;
    }

    public static (Container?, Coordinates) FarFromEnd(ItemToBePacked item, IEnumerable<Container> containers)
    {
        (Container?, Coordinates) returnValue = (null, new Coordinates(0, 0, 0));

        double distance = 0;

        if (containers.Any())
        {
            Sizes contDim = containers.First().ContainerProperties.Dimension;

            Coordinates end = new Coordinates(contDim.X, contDim.Y, contDim.Z);

            foreach (var container in containers)
            {

                IEnumerable<Region> EMS = container.EmptyMaximalRegions.getEmptyMaximalSpaces(item.Item.GetRotatedItemDimensions());

                foreach (Region space in EMS)
                {
                    double newDistance = space.Start.GetEuclidanDistanceTo(end);

                    if (newDistance > distance)
                    {
                        distance = newDistance;
                        returnValue = (container, space.Start);
                    }
                }

            }
        }
        return returnValue;
    }

    private static bool ValidWeight(BoxToBePacked boxToBePlaced, ContainerDataForHeuristics containerData)
    {
        return containerData.CurrentWeight + boxToBePlaced.Box.Weight <= containerData.ContainerProperties.MaxWeight;

    }

    private static bool ValidSides(BoxToBePacked boxToBePlaced, Region region)
    {
        return boxToBePlaced.GetRotatedSizes().AllLessOrEqualThan(region.GetSizes());
    }

}

