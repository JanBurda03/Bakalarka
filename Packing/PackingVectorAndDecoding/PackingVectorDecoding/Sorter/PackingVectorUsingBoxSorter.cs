internal class PackingVectorUsingBoxSorter : IBoxToBePackedSorter
{
    public bool IsUsingPackingVector { get; init; }
    private readonly PackingPairComparer _packingPairComparer;
    public PackingVectorUsingBoxSorter()
    {
        _packingPairComparer = new PackingPairComparer();
        IsUsingPackingVector = true;
    }
    public IReadOnlyList<BoxToBePacked> Sort(IReadOnlyList<BoxToBePacked> unsortedBoxes, PackingVector packingVector)
    {

        if(packingVector.Count < unsortedBoxes.Count) 
        {
            throw new ArgumentException("Packing vector is too short!");
        }

        (PackingVectorCell Value, BoxToBePacked Box)[] pairs = new (PackingVectorCell, BoxToBePacked)[unsortedBoxes.Count];
        for (int i = 0; i < unsortedBoxes.Count; i++)
        {
            pairs[i] = (packingVector[i], unsortedBoxes[i]);
        }


        Array.Sort(pairs, _packingPairComparer);

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

