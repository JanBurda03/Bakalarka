public readonly record struct PackingVectorCell:IComparable<PackingVectorCell>
{
    private const int MaxValue = ushort.MaxValue; // 65535
    public ushort Value { get; }

    public PackingVectorCell(double value)
    {
        if (value < 0.0 || value >= 1.0)
            throw new ArgumentOutOfRangeException(nameof(value), "Value must be in range [0, 1)");
        Value = (ushort)(value * MaxValue);
    }

    public int CompareTo(PackingVectorCell other)
    {
        return Value.CompareTo(other.Value);
    }

    public double ToDouble() => Value / (double)MaxValue;

    // any packing vector cell can be implicitly converted into double
    public static implicit operator double(PackingVectorCell v) => v.ToDouble();
    // conversion from double to packing vector must always be explicit because of the constaint of the number being between 0 and 1
    public static explicit operator PackingVectorCell(double d) => new PackingVectorCell(d);
}