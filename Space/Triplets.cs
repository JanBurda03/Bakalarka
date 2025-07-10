public interface ITriplet
{
    public int X { get; init; }
    public int Y { get; init; }
    public int Z { get; init; }
}

public readonly record struct Coordinates : ITriplet
{
    public int X { get; init; }
    public int Y { get; init; }
    public int Z { get; init; }

    public Coordinates(int X, int Y, int Z)
    {
        this.X = X;
        this.Y = Y;
        this.Z = Z;
    }

    public double GetEuclidanDistanceTo(Coordinates coordinates)
    {
        int dx = this.X - coordinates.X;
        int dy = this.Y - coordinates.Y;
        int dz = this.Z - coordinates.Z;
        return Math.Sqrt(dx * dx + dy * dy + dz * dz);
    }

}

public readonly record struct Dimensions : ITriplet
{
    public int X { get; init; }
    public int Y { get; init; }
    public int Z { get; init; }

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





