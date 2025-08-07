public class HeuristicalBoxSorter : IBoxToBePackedSorter
{
    IComparer<BoxToBePacked> BoxComparer;
    public HeuristicalBoxSorter(IComparer<BoxToBePacked> boxComparer)
    {
        BoxComparer = boxComparer;
    }
    public BoxToBePacked[] Sort(BoxToBePacked[] unsortedBoxes, PackingVector _)
    {
        BoxToBePacked[] sortedBoxes = (BoxToBePacked[])unsortedBoxes.Clone();

        Array.Sort(sortedBoxes, BoxComparer);

        return sortedBoxes;
    }
}