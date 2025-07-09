public interface ITriplet
{
    int X { get; }
    int Y { get; }
    int Z { get; }
}

public readonly record struct Coordinates : ITriplet
{
    public int X { get; }
    public int Y { get; }
    public int Z { get; }

    public Coordinates(int X, int Y, int Z)
    {
        this.X = X;
        this.Y = Y;
        this.Z = Z;
    }

}

public readonly record struct Dimensions : ITriplet
{
    public int X { get; }
    public int Y { get; }
    public int Z { get; }

    public Dimensions(int X, int Y, int Z)
    {
        this.X = X;
        this.Y = Y;
        this.Z = Z;
    }

    public int GetVolume()
    {
        return X * Y * Z;
    }

    public Space ToSpace()
    {
        return new Space(new Coordinates(0,0,0), new Coordinates(X,Y,Z));
    }

}

public static class TripletExtensions
{
    public static bool AllGreaterThan(this ITriplet a, ITriplet b) => a.X > b.X && a.Y > b.Y && a.Z > b.Z;

    public static bool AllLessThan(this ITriplet a, ITriplet b) => a.X < b.X && a.Y < b.Y && a.Z < b.Z;

    public static bool AllGreaterOrEqualThan(this ITriplet a, ITriplet b) => a.X >= b.X && a.Y >= b.Y && a.Z >= b.Z;

    public static bool AllLessOrEqualThan(this ITriplet a, ITriplet b) => a.X <= b.X && a.Y <= b.Y && a.Z <= b.Z;
}





