public static class Packer
{
    public static List<Container> Pack(IEnumerable<BoxToBePacked> boxesToBePacked, ContainerProperties containerProperties)
    {
        List<Container> containers = new List<Container>();

        foreach (BoxToBePacked boxToBePacked in boxesToBePacked)
        {
            PlacementInfo? possiblePlacementInfo;
            PlacementInfo placementInfo;

            possiblePlacementInfo = boxToBePacked.PlacementHeuristic(boxToBePacked, containers.Select(container => container.GetDataForHeuristics()));
            if (possiblePlacementInfo != null)
            {
                placementInfo = (PlacementInfo)possiblePlacementInfo;
                containers[placementInfo.ContainerID].PackBox(boxToBePacked, placementInfo.Region);
            }
            else
            {
                containers.Add(new Container(containers.Count, containerProperties));

                possiblePlacementInfo = boxToBePacked.PlacementHeuristic(boxToBePacked, containers.Select(container => container.GetDataForHeuristics()));
                if (possiblePlacementInfo != null)
                {
                    placementInfo = (PlacementInfo)possiblePlacementInfo;
                    containers[placementInfo.ContainerID].PackBox(boxToBePacked, placementInfo.Region);
                }
                else
                {
                    throw new Exception($"Item is too large!");
                }
            }
        }

        return containers;
    }
}











