internal class MultipleRotationsDecoder : PackingVectorUsingPartDecoder<Rotation>
{
    public MultipleRotationsDecoder(IReadOnlyList<Rotation> rotations) : base(rotations) { }
}

internal class AllRotationsDecoder : PackingVectorUsingPartDecoder<Rotation>
{
    public AllRotationsDecoder() : base(Enum.GetValues<Rotation>()) { }
}

internal class OneRotationDecoder : PackingVectorNonUsingPartDecoder<Rotation>
{
    public OneRotationDecoder(Rotation rotation) : base(rotation) { }
}