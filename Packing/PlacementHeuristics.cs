public delegate PlacementInfo? PlacementHeuristic(BoxToBePacked boxToBePlaced, IEnumerable<ContainerDataForHeuristics> containersData);

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
                        return new PlacementInfo(containerData.ID, boxToBePlaced.GetRotatedSizes().ToRegion(region.Start));
                    }
                }
            }     
        }
        return null;
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

