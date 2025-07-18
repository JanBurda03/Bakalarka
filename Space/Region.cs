
public readonly record struct Region
{
    public Coordinates Start { get; init; }
    public Coordinates End { get; init; }

    public Region(Coordinates start, Coordinates end) => (Start, End) = Standardize(start, end);

    public bool IntersectsWith(Region anotherRegion)
    {
        return (    Start.AllLessOrEqualThan(anotherRegion.Start)    &&  End.AllGreaterThan(anotherRegion.Start)      )       // start of another region is in between the start and the end of our region
            || (    Start.AllLessThan(anotherRegion.End)             &&  End.AllGreaterOrEqualThan(anotherRegion.End) )       // end of another region is in between the start and the end of our region
            || (    Start.AllGreaterOrEqualThan(anotherRegion.Start) &&  Start.AllLessThan(anotherRegion.End)         )       // start of our region is somewhere in between the start and the end of another region
            || (    End.AllGreaterThan(anotherRegion.Start)          &&  End.AllLessOrEqualThan(anotherRegion.End)    );      // end of our region is somewhere in between the start and the end of another region

    }

    public int GetVolume()
    {
        return (End.X - Start.X) * (End.Y - Start.Y) * (End.Z - Start.Z);
    }

    public bool IsSubregionOf(Region region) => Start.AllGreaterOrEqualThan(region.Start) && End.AllLessOrEqualThan(region.End);

    public bool IsOverregionOf(Region region) => Start.AllLessOrEqualThan(region.Start) && End.AllGreaterOrEqualThan(region.End);
    

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

    public Sizes GetSizes() => new Sizes(End.X - Start.X, End.Y - Start.Y, End.Z - Start.Z);
    
}

