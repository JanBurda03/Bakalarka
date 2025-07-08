public class EmptyMaximalSpaces
{

    private IList<Space> emptyMaximalSpaces;

    private int Height; 

    public EmptyMaximalSpaces(Space initialSpace)
    {
        emptyMaximalSpaces = new List<Space> { initialSpace };

        Height = initialSpace.End.Z;
    }

    public EmptyMaximalSpaces(IEnumerable<Space> initialSpaces)
    {
        emptyMaximalSpaces = initialSpaces.ToList();

        Height = emptyMaximalSpaces.Max(x => x.End.Z);
    }

    public IEnumerable<Space> getEmptySpaces(Dimensions dimensions)
    {
        // returns only the spaces in which the object (defined by the dimensions) can be fitted
        return from space in emptyMaximalSpaces where space.GetDimensions().AllGreaterOrEqualThan(dimensions) select space;
    }

    public IEnumerable<Space> getEmptySpaces()
    {
        // returns only the spaces in which the object (defined by the dimensions) can be fitted
        return emptyMaximalSpaces;
    }


    public void updateEmptySpaces(Space newlyOccupied)
    {
        if (!IsValidPlacement(newlyOccupied))
        {
            throw new Exception("Space is already occupied");
        }

        List<Space> newSpaces = new List<Space>();
        List<Space> unchangedSpaces = new List<Space>();

        foreach (Space space in emptyMaximalSpaces) 
        {
            if (space.IntersectsWith(newlyOccupied))
            {
                newSpaces.AddRange(SplitRemainingSpace(space, newlyOccupied));
            }
            else
            {
                unchangedSpaces.Add(space);
            }
        }

        newSpaces = deleteSubspaces(newSpaces, unchangedSpaces);

        unchangedSpaces.AddRange(newSpaces);
        unchangedSpaces.Add(addUpperEMS(newlyOccupied));

        emptyMaximalSpaces = unchangedSpaces;
    }

    private bool IsValidPlacement(Space newlyOccupied) 
    {
        bool valid = false;
        foreach (Space space in emptyMaximalSpaces)
        {
            Console.WriteLine(newlyOccupied.IsSubspaceOf(space));
            if (newlyOccupied.IsSubspaceOf(space))
            {
                
                valid = true;
            }
        }
        return valid;
    }
    
    public List<Space> deleteSubspaces(List<Space> newSpaces, List<Space> unchangedSpaces)
    {
        newSpaces = newSpaces.Distinct().ToList();

        List<Space> toRemove = new List<Space>();

        for (int i = 0; i < newSpaces.Count; i++)
        {
            for (int j = i + 1; j < newSpaces.Count; j++)
            {
                var a = newSpaces[i];
                var b = newSpaces[j];

                if (a.IsSubspaceOf(b))
                    toRemove.Add(a);
                else if (b.IsSubspaceOf(a))
                    toRemove.Add(b);
            }
        }

        foreach (Space space in unchangedSpaces)
        {
            foreach (Space newSpace in newSpaces)
            {
                if (newSpace.IsSubspaceOf(space))
                {
                    toRemove.Add(newSpace);
                }
            }
        }

        foreach (Space space in toRemove)
        {
            newSpaces.Remove(space);
        }

        return newSpaces;
    }


    private Space addUpperEMS(Space occupied)
    {
        return new Space(new Coordinates(occupied.Start.X, occupied.Start.Y, occupied.End.Z), new Coordinates(occupied.End.X, occupied.End.Y, Height));
        // TODO: Implement that two spaces starting at the same height and neigbouring are merged
    }



    public IEnumerable<Space> SplitRemainingSpace(Space original, Space occupied)
    {
        List<Space> spaces = new List<Space>();

        if (original.Start.Z < occupied.Start.Z)
        {
            throw new Exception("Newly placed object must always be fully on base");
        }

        if (original.Start.Z > occupied.Start.Z)
        {
            throw new Exception("Empty maximal space must always be fully on base");
        }

        int a_start = original.Start.X;
        int b_start = original.Start.Y;

        int a_end = original.End.X;
        int b_end = original.End.Y;

        int x_start = occupied.Start.X;
        int y_start = occupied.Start.Y;

        int x_end = occupied.End.X;
        int y_end = occupied.End.Y;

        if (x_start > a_start)
        {
            spaces.Add(new Space(new Coordinates(a_start, b_start, original.Start.Z), new Coordinates(x_start, b_end, original.End.Z)));
        }

        if (y_start > b_start)
        {
            spaces.Add(new Space(new Coordinates(a_start, b_start, original.Start.Z), new Coordinates(a_end, y_start, original.End.Z)));
        }

        if (x_end < a_end)
        {
            spaces.Add(new Space(new Coordinates(x_end, b_start, original.Start.Z), new Coordinates(a_end, b_end, original.End.Z)));
        }

        if (y_end < b_end)
        {
            spaces.Add(new Space(new Coordinates(a_start, y_end, original.Start.Z), new Coordinates(a_end, b_end, original.End.Z)));

        }

        return spaces;
    }
}







