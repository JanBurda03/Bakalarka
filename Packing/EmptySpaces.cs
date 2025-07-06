public class EmptyMaximalSpaces
{
    private IList<Space> emptyMaximalSpaces;

    public EmptyMaximalSpaces(IEnumerable<Space> initialSpaces)
    {
        emptyMaximalSpaces = initialSpaces.ToList();
    }

    private IEnumerable<Space> UpdateSingleEmptySpace(Space emptySpace, Space newlyOccupied)
    {
        if (emptySpace.IntersectsWith(newlyOccupied))
        {
            return SplitRemainingSpace(emptySpace, newlyOccupied);
        }

        return new[] { emptySpace };
    }

    public void UpdateEmptySpaces(Space newlyOccupied)
    {
        emptyMaximalSpaces = emptyMaximalSpaces
            .SelectMany(space => UpdateSingleEmptySpace(space, newlyOccupied))
            .ToList();

        mergeSpacesWithHeight(newlyOccupied);

        deleteSubspaces();
        
    }

    public IEnumerable<Space> GetCurrentSpaces() => emptyMaximalSpaces;
    
    private void deleteSubspaces()
    {

    }


    private void mergeSpacesWithHeight(Space occupied)
    {

    }

    public IEnumerable<Space> SplitRemainingSpace(Space original, Space occupied)
    {
        List<Space> spaces = new List<Space>();

        if (original.start.Z < occupied.start.Z)
        {
            throw new Exception("Newly placed object must always be fully on base");
        }

        if (original.start.Z > occupied.start.Z)
        {
            throw new Exception("Empty maximal space must always be fully on base");
        }

        int a_start = original.start.X;
        int b_start = original.start.Y;

        int a_end = original.end.X;
        int b_end = original.end.Y;

        int x_start = occupied.start.X;
        int y_start = occupied.start.Y;

        int x_end = occupied.end.X;
        int y_end = occupied.end.Y;

        if (x_start > a_start)
        {
            spaces.Add(new Space(new Coordinates(a_start, b_start, original.start.Z), new Coordinates(x_start, b_end, original.start.Z)));
        }

        if (y_start > b_start)
        {
            spaces.Add(new Space(new Coordinates(a_start, b_start, original.start.Z), new Coordinates(a_end, y_start, original.start.Z)));
        }

        if (x_end < a_end)
        {
            spaces.Add(new Space(new Coordinates(x_end, b_start, original.start.Z), new Coordinates(a_end, b_end, original.start.Z)));
        }

        if (y_end < b_end)
        {
            spaces.Add(new Space(new Coordinates(a_start, y_end, original.start.Z), new Coordinates(a_end, b_end, original.start.Z)));

        }

        if (original.end.Z > occupied.end.Z)
        {
            spaces.Add(new Space(new Coordinates(x_start, y_start, occupied.end.Z), new Coordinates(x_end, y_end, original.end.Z)));
        }

        return spaces;
    }
}

public record struct Space
{
    public Coordinates start;
    public Coordinates end;

    public bool IntersectsWith(Space anotherSpace)
    {
        return (    (start.allLessThan(anotherSpace.start) || end.allEqual(anotherSpace.start))         &&  end.allGreaterThan(anotherSpace.start)                                      ) // start of another space is in between the start and the end of our space
            || (    start.allLessThan(anotherSpace.end)                                                 && (end.allGreaterThan(anotherSpace.end) || end.allEqual(anotherSpace.end))     ) // end of another space is in between the start and the end of our space
            || (    (start.allGreaterThan(anotherSpace.start) || start.allEqual(anotherSpace.start))    && start.allLessThan(anotherSpace.end)                                          ) // start of our space is somewhere in between the start and the end of another space
            || (    end.allGreaterThan(anotherSpace.start)                                              && (end.allLessThan(anotherSpace.end) || end.allEqual(anotherSpace.end))        );// end of our space is somewhere in between the start and the end of another space

    }


    public bool isSubspaceOf(Space space)
    {
        return (start.allGreaterThan(space.start) || start.allEqual(space.start))
            && (end.allLessThan(space.end) || end.allEqual(space.end));
    }

    public bool isOverspaceOf(Space space)
    {
        return (start.allLessThan(space.start) || start.allEqual(space.start))
            && (end.allGreaterThan(space.end) || end.allEqual(space.end));
    }

    public Space(Coordinates start, Coordinates end)
    {
        this.start = start;
        this.end = end;
    }
}

public record struct Coordinates
{
    public int X;
    public int Y;
    public int Z;

    public Coordinates(int X, int Y, int Z)
    {
        this.X = X;
        this.Y = Y;
        this.Z = Z;
    }

    public bool allEqual(Coordinates anotherCoordinates)
    {
        return (X == anotherCoordinates.X) && (Y == anotherCoordinates.Y) && (Z == anotherCoordinates.Z);
    }

    public bool allGreaterThan(Coordinates anotherCoordinates)
    {
        return (X > anotherCoordinates.X) && (Y > anotherCoordinates.Y) && (Z > anotherCoordinates.Z);
    }

    public bool allLessThan(Coordinates anotherCoordinates)
    {
        return (X < anotherCoordinates.X) && (Y < anotherCoordinates.Y) && (Z < anotherCoordinates.Z);
    }
}



