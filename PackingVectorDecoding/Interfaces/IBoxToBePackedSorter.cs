public interface IBoxToBePackedSorter
{
    public BoxToBePacked[] Sort(BoxToBePacked[] unsortedBoxes, PackingVector packingVector);
}