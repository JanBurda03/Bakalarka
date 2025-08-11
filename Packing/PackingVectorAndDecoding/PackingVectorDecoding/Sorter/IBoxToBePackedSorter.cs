public interface IBoxToBePackedSorter:IPackingVectorUsing
{
    public IReadOnlyList<BoxToBePacked> Sort(IReadOnlyList<BoxToBePacked> unsortedBoxes, PackingVector packingVector);
}