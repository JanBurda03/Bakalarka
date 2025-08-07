public delegate PlacementInfo? PlacementHeuristic(BoxToBePacked boxToBePlaced, IEnumerable<ContainerDataForHeuristics> containersData);



public static class PlacementHeuristics
{
    private static readonly Dictionary<string, PlacementHeuristic> PlacementHeuristicsDictionary = new Dictionary<string, PlacementHeuristic>
        {
            {"FirstFit", FirstFit},
            {"BestFit", BestFit},
            {"MaxDistance", MaxDistance},
            {"MinDistance", MinDistance}
        };

    public static string[] PlacementHeuristicsList => PlacementHeuristicsDictionary.Keys.ToArray();

    public static PlacementHeuristic GetPlacementHeuristic(string placementHeuristicName)
    {
        if (PlacementHeuristicsDictionary.TryGetValue(placementHeuristicName, out var placementHeuristic))
            return placementHeuristic;

        throw new ArgumentException($"Unknown heuristic: {placementHeuristicName}");
    }

    public static IReadOnlyList<PlacementHeuristic> GetMultiplePlacementHeuristics(string[] placementHeuristicsNames)
    {
        PlacementHeuristic[] placementHeuristics = new PlacementHeuristic[placementHeuristicsNames.Length];
        for (int i = 0; i < placementHeuristicsNames.Length; i++) 
        {
            placementHeuristics[i] = GetPlacementHeuristic(placementHeuristicsNames[i]);
        }
        return placementHeuristics;
    }


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

    public static PlacementInfo? BestFit(BoxToBePacked boxToBePlaced, IEnumerable<ContainerDataForHeuristics> containersData)
    {
        PlacementInfo? info = null;

        foreach (ContainerDataForHeuristics containerData in containersData)
        {
            if (ValidWeight(boxToBePlaced, containerData))
            {
                foreach (Region region in containerData.EMR)
                {
                    if (ValidSides(boxToBePlaced, region))
                    {
                        if (info == null || ((PlacementInfo)info).OccupiedRegion.GetVolume() > region.GetVolume())
                        {
                            info = new PlacementInfo(containerData.ID, boxToBePlaced.GetRotatedSizes().ToRegion(region.Start));
                        }
                    }
                }
            }
        }
        return info;
    }

    public static PlacementInfo? MaxDistance(BoxToBePacked boxToBePlaced, IEnumerable<ContainerDataForHeuristics> containersData)
    {
        PlacementInfo? info = null;
        int maxDistance = int.MaxValue;
        

        foreach (ContainerDataForHeuristics containerData in containersData)
        {
            Coordinates containerEnd = containerData.ContainerProperties.Sizes.ToRegion(new Coordinates(0, 0, 0)).End;
            if (ValidWeight(boxToBePlaced, containerData))
            {
                foreach (Region region in containerData.EMR)
                {
                    if (ValidSides(boxToBePlaced, region))
                    {
                        if (info == null || maxDistance < region.Start.GetEuclidanDistanceTo(containerEnd))
                        {
                            info = new PlacementInfo(containerData.ID, boxToBePlaced.GetRotatedSizes().ToRegion(region.Start));
                        }
                    }
                }
            }
        }
        return info;
    }

    public static PlacementInfo? MinDistance(BoxToBePacked boxToBePlaced, IEnumerable<ContainerDataForHeuristics> containersData)
    {
        PlacementInfo? info = null;
        int minDistance = int.MaxValue;
        Coordinates containerStart = new Coordinates(0, 0, 0);


        foreach (ContainerDataForHeuristics containerData in containersData)
        {
            
            if (ValidWeight(boxToBePlaced, containerData))
            {
                foreach (Region region in containerData.EMR)
                {
                    if (ValidSides(boxToBePlaced, region))
                    {
                        if (info == null || minDistance > region.Start.GetEuclidanDistanceTo(containerStart))
                        {
                            info = new PlacementInfo(containerData.ID, boxToBePlaced.GetRotatedSizes().ToRegion(region.Start));
                        }
                    }
                }
            }
        }
        return info;
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

