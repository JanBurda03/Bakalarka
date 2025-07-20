public readonly record struct PackingVectorCell
{
    private const int MaxValue = ushort.MaxValue; // 65535
    public ushort Value { get; }

    public PackingVectorCell(double value)
    {
        if (value < 0.0 || value > 1.0)
            throw new ArgumentOutOfRangeException(nameof(value), "Value must be in range [0, 1]");
        Value = (ushort)Math.Floor(value * MaxValue);
    }

    public double ToDouble() => Value / (double)MaxValue;

    public static implicit operator double(PackingVectorCell v) => v.ToDouble();
    public static explicit operator PackingVectorCell(double d) => new PackingVectorCell(d);
}