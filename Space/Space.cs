
public readonly record struct Space
{
    public Coordinates Start { get; }
    public Coordinates End { get;  }

    public bool IntersectsWith(Space anotherSpace)
    {
        return (    Start.AllLessOrEqualThan(anotherSpace.Start)    &&  End.AllGreaterThan(anotherSpace.Start)      )       // start of another space is in between the start and the end of our space
            || (    Start.AllLessThan(anotherSpace.End)             &&  End.AllGreaterOrEqualThan(anotherSpace.End) )       // end of another space is in between the start and the end of our space
            || (    Start.AllGreaterOrEqualThan(anotherSpace.Start) &&  Start.AllLessThan(anotherSpace.End)         )       // start of our space is somewhere in between the start and the end of another space
            || (    End.AllGreaterThan(anotherSpace.Start)          &&  End.AllLessOrEqualThan(anotherSpace.End)    );      // end of our space is somewhere in between the start and the end of another space

    }


    public bool IsSubspaceOf(Space space) => Start.AllGreaterOrEqualThan(space.Start) && End.AllLessOrEqualThan(space.End);

    public bool IsOverspaceOf(Space space) => Start.AllLessOrEqualThan(space.Start) && End.AllGreaterOrEqualThan(space.End);
    

    public Space(Coordinates start, Coordinates end) => (Start, End) = Standardize(start, end);
    

    private (Coordinates, Coordinates ) Standardize(Coordinates a, Coordinates b)
    {
        int xStart = Math.Min(a.X, b.X);
        int yStart = Math.Min(a.Y, b.Y);
        int zStart = Math.Min(a.Z, b.Z);

        int xEnd = Math.Max(a.X, b.X);
        int yEnd = Math.Max(a.Y, b.Y);
        int zEnd = Math.Max(a.Z, b.Z);

        return (new Coordinates(xStart, yStart, zStart), new Coordinates(xEnd, yEnd, zEnd));
    }

    public Dimensions GetDimensions() => new Dimensions(End.X - Start.X, End.Y - Start.Y, End.Z - Start.Z);
    
}