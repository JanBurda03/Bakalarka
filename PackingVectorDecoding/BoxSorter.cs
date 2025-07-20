public class PackingVectorBoxSorter:IBoxToBePackedSorter
{
    PackingPairComparer packingPairComparer;
    public PackingVectorBoxSorter()
    {
        packingPairComparer = new PackingPairComparer();
    }
    public BoxToBePacked[] Sort(BoxToBePacked[] unsortedBoxes, PackingVector packingVector)
    {
        ReadOnlySpan<PackingVectorCell> sortingVector = packingVector.GetBoxSortingPart();

        if(sortingVector.Length < unsortedBoxes.Length) 
        {
            throw new Exception();
        }

        (PackingVectorCell Value, BoxToBePacked Box)[] pairs = new (PackingVectorCell, BoxToBePacked)[unsortedBoxes.Length];

        for (int i = 0; i < unsortedBoxes.Length; i++)
        {
            pairs[i] = (sortingVector[i], unsortedBoxes[i]);
        }


        Array.Sort(pairs, packingPairComparer);

        BoxToBePacked[] sortedBoxesToBePacked = new BoxToBePacked[pairs.Length];

        for (int i = 0; i < sortedBoxesToBePacked.Length; i++)
        {
            sortedBoxesToBePacked[i] = pairs[i].Box;
        }

        return sortedBoxesToBePacked;
    }

    protected class PackingPairComparer : IComparer<(PackingVectorCell value, BoxToBePacked box)>
    {
        public int Compare((PackingVectorCell value, BoxToBePacked box) a, (PackingVectorCell value, BoxToBePacked box) b)
        {
            return a.value.Value.CompareTo(b.value.Value);
        }
    }
}

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