public delegate (Container?, Coordinates) WhereToPackHeuristics(ItemToBePacked item, IEnumerable<Container> containers);

public static class PlacementHeuristics
{
    public static (Container?, Coordinates) FirstFit(ItemToBePacked item, IEnumerable<Container> containers)
    {
        foreach (Container container in containers)
        {
            
            IEnumerable<Space> EMS = container.EmptyMaximalSpaces.getEmptyMaximalSpaces(item.Item.GetRotatedItemDimensions());

            if (EMS.Any())
            {
                Space space = EMS.First();
                return (container, space.Start);
            }
            
        }
        return (null, new Coordinates(0,0,0));
    }

    public static (Container?, Coordinates) SmallZFirst(ItemToBePacked item, IEnumerable<Container> containers)
    {
        (Container?, Coordinates) returnValue = (null, new Coordinates(0, 0, int.MaxValue));

        foreach (Container container in containers)
        {
            IEnumerable<Space> EMS = container.EmptyMaximalSpaces.getEmptyMaximalSpaces(item.Item.GetRotatedItemDimensions());

            foreach (Space space in EMS) 
            { 
                if (space.Start.Z < returnValue.Item2.Z)
                {
                    returnValue = (container, space.Start);
                }
            }
        }
        return returnValue;
    }

    public static (Container?, Coordinates) SmallXFirst(ItemToBePacked item, IEnumerable<Container> containers)
    {
        (Container?, Coordinates) returnValue = (null, new Coordinates(int.MaxValue, 0, 0));

        foreach (var container in containers)
        {
            
            IEnumerable<Space> EMS = container.EmptyMaximalSpaces.getEmptyMaximalSpaces(item.Item.GetRotatedItemDimensions());

            foreach (Space space in EMS)
            {
                if (space.Start.X < returnValue.Item2.X)
                {
                    returnValue = (container, space.Start);
                }
            }
            
        }
        return returnValue;
    }

    public static (Container?, Coordinates) SmallYFirst(ItemToBePacked item, IEnumerable<Container> containers)
    {
        (Container?, Coordinates) returnValue = (null, new Coordinates(0, int.MaxValue, 0));

        foreach (var container in containers)
        {

            IEnumerable<Space> EMS = container.EmptyMaximalSpaces.getEmptyMaximalSpaces(item.Item.GetRotatedItemDimensions());

            foreach (Space space in EMS)
            {
                if (space.Start.Y < returnValue.Item2.Y)
                {
                    returnValue = (container, space.Start);
                }
            }

        }
        return returnValue;
    }

    public static (Container?, Coordinates) CloseToOrigin(ItemToBePacked item, IEnumerable<Container> containers)
    {
        (Container?, Coordinates) returnValue = (null, new Coordinates (0, 0, 0));

        double distance = double.MaxValue;

        Coordinates origin = new Coordinates(0, 0, 0);

        foreach (var container in containers)
        {

            IEnumerable<Space> EMS = container.EmptyMaximalSpaces.getEmptyMaximalSpaces(item.Item.GetRotatedItemDimensions());

            foreach (Space space in EMS)
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
            Dimensions contDim = containers.First().ContainerProperties.Dimension;

            Coordinates end = new Coordinates(contDim.X, contDim.Y, contDim.Z);

            foreach (var container in containers)
            {

                IEnumerable<Space> EMS = container.EmptyMaximalSpaces.getEmptyMaximalSpaces(item.Item.GetRotatedItemDimensions());

                foreach (Space space in EMS)
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
}