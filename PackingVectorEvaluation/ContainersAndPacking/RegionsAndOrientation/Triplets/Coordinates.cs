﻿public readonly record struct Coordinates : ITriplet
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