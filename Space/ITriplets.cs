public interface ITriplet
{
    public int X { get; init; }
    public int Y { get; init; }
    public int Z { get; init; }
}


public static class TripletExtensions
{
    public static bool AllGreaterThan(this ITriplet a, ITriplet b) => a.X > b.X && a.Y > b.Y && a.Z > b.Z;

    public static bool AllLessThan(this ITriplet a, ITriplet b) => a.X < b.X && a.Y < b.Y && a.Z < b.Z;

    public static bool AllGreaterOrEqualThan(this ITriplet a, ITriplet b) => a.X >= b.X && a.Y >= b.Y && a.Z >= b.Z;

    public static bool AllLessOrEqualThan(this ITriplet a, ITriplet b) => a.X <= b.X && a.Y <= b.Y && a.Z <= b.Z;
}





