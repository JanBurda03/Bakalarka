public class BoxPacker
{
    ContainerProperties ContainerProperties { get; init; }

    List<Container> Containers;

    public BoxPacker(ContainerProperties containerProperties)
    {
        ContainerProperties = containerProperties;
        Containers = new List<Container>();
    }



    public IReadOnlyList<Container> PackBoxes(IEnumerable<BoxToBePacked> boxesToBePacked)
    {
        foreach (BoxToBePacked boxToBePacked in boxesToBePacked)
        {
            PackBox(boxToBePacked);
        }
        return Containers.AsReadOnly();
    }

    public void PackBox(BoxToBePacked boxToBePacked) 
    {            
        if(! TryPackBox(boxToBePacked)) // trying to pack
        {
            // if packing failed

            if (TooLargeSizes(boxToBePacked)) // checking whether any of the sizes of the rotated box are greater that the sizes of an empty container; in that case, there is no solution for that rotation
            {
                boxToBePacked = ChangeBoxRotation(boxToBePacked); // changing the rotation, because there is no chance that the previus one would fit even in an empty container
                if (!TryPackBox(boxToBePacked)) // if the box still cannot be packed, it is because containers are already too loaded and a new container is opened 
                {
                    AddContainer();
                }
            }
            else // if the problem is not that there is no valid solution for that rotation, the problem must be the lack of space in any of the containers, so new one is opened
            {
                AddContainer();
            }


            if (!TryPackBox(boxToBePacked)) // if there is still a problem with the loading, it means that the box itself is heavier than the capacity of container
            {
                throw new Exception("The box is too heavy!");
            }
        }

        
    }

    private void AddContainer()
    {
        Containers.Add(new Container(Containers.Count, ContainerProperties));
    }

    private BoxToBePacked ChangeBoxRotation(BoxToBePacked boxToBePacked)
    {
        // in case that there is no valid solution for that particular rotation (even after adding an empty container), the rotation must be changed

        int count = Enum.GetValues(typeof(Rotation)).Length;
        BoxToBePacked newBoxToBePacked;
        for (int i = 1; i < count; i++)
        {
            // using first fit, the first rotation that ensures the box can fit in empty container is returned
            Rotation newRotation = (Rotation)((((int)boxToBePacked.Rotation) + i) % count);
            newBoxToBePacked = new BoxToBePacked(boxToBePacked.Box, newRotation, boxToBePacked.PlacementHeuristic);

            if (newBoxToBePacked.GetRotatedSizes().AllLessOrEqualThan(ContainerProperties.Sizes))
            {
                return newBoxToBePacked;
            }
        }

        // if the box doesnt fit in an empty container with any rotation, the exception is thrown

        throw new Exception("The box is too large to fit in container with these sizes!");
    }

    private bool TooLargeSizes(BoxToBePacked boxToBePacked)
    {
        // ckecking whether the rotated box can fit in an empty container

        return !boxToBePacked.GetRotatedSizes().AllLessOrEqualThan(ContainerProperties.Sizes);
    }

    private IEnumerable<ContainerDataForHeuristics>GetContainersData()
    {
        // collecting the data for heuristics from all the opened containers

        return Containers.Select(container => container.GetDataForHeuristics());
    }

    private bool TryPackBox(BoxToBePacked boxToBePacked)
    {
        // trying to pack to a container and at a coordinate chosen by the heuristics; in case no valid region is found, false is returned
        PlacementInfo? possiblePlacementInfo = boxToBePacked.PlacementHeuristic(boxToBePacked, GetContainersData());
        if (possiblePlacementInfo != null)
        {
            PlacementInfo placementInfo = (PlacementInfo)possiblePlacementInfo;
            Containers[placementInfo.ContainerID].PackBox(boxToBePacked, placementInfo);
            return true;
        }
        return false;
    }
}











