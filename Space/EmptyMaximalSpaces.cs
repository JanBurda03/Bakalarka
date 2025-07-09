public interface IEmptyMaximalSpaces
{
    public IEnumerable<Space> getEmptyMaximalSpaces(Dimensions dimensions);

    public IEnumerable<Space> getEmptyMaximalSpaces();

    public void updateEmptyMaximalSpaces(Space space);
}


public class EmptyMaximalSpaces : IEmptyMaximalSpaces
{

    private IList<Space> emptyMaximalSpaces;

    private int Height; 

    private ISpaceSpliter Spliter;

    public EmptyMaximalSpaces(Space initialSpace)
    {
        emptyMaximalSpaces = new List<Space> { initialSpace };

        Height = initialSpace.End.Z;

        Spliter = new EmptyMaximalSpaceSpliter();
    }

    public EmptyMaximalSpaces(IEnumerable<Space> initialSpaces)
    {
        emptyMaximalSpaces = initialSpaces.ToList();

        Height = emptyMaximalSpaces.Max(x => x.End.Z);

        Spliter = new EmptyMaximalSpaceSpliter();
    }

    public IEnumerable<Space> getEmptyMaximalSpaces(Dimensions dimensions)
    {
        // returns only the spaces in which the object (defined by the dimensions) can be fitted
        return from space in emptyMaximalSpaces where space.GetDimensions().AllGreaterOrEqualThan(dimensions) select space;
    }

    public IEnumerable<Space> getEmptyMaximalSpaces()
    {
        // returns only the spaces in which the object (defined by the dimensions) can be fitted
        return emptyMaximalSpaces;
    }


    public void updateEmptyMaximalSpaces(Space newlyOccupied)
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
                newSpaces.AddRange(Spliter.SplitSpace(space, newlyOccupied));
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

}









