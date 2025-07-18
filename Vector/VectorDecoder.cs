
public static class X
{
    public static IList<BoxToBePacked> DecodeVectorToPackingSequence(IList<BoxProperties> boxesProperties)
    {
        
        return (from BoxProperties boxProperties in boxesProperties select new BoxToBePacked(boxProperties, Rotation.XYZ, PlacementHeuristics.FirstFit)).ToList();
    }
}
