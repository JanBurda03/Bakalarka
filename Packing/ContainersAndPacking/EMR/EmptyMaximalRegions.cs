internal class EmptyMaximalRegions : IEmptyMaximalRegions
{
    public IReadOnlyList<Region> EmptyMaximalRegionsList => _emptyMaximalRegions.AsReadOnly();


    private IList<Region> _emptyMaximalRegions;


    private readonly IRegionSpliter _splitter;

    public EmptyMaximalRegions(Region initial)
    {
        _emptyMaximalRegions = new List<Region> { initial };

        _splitter = new EMRNoTopNoBottomSpliter();
    }

    public EmptyMaximalRegions(IEnumerable<Region> initials)
    {
        _emptyMaximalRegions = initials.ToList();

        _splitter = new EMRNoTopNoBottomSpliter();
    }



    public void UpdateEMR(Region newOccupied)
    {
        if (!IsValidPlacement(newOccupied))
        {
            throw new Exception("The region is already occupied!");
        }

        List<Region> newRegions = new List<Region>();
        List<Region> unchangedRegions = new List<Region>();

        foreach (Region region in _emptyMaximalRegions) 
        {
            if (region.IntersectsWith(newOccupied))
            {
                newRegions.AddRange(_splitter.SplitRegion(region, newOccupied));
            }
            else
            {
                unchangedRegions.Add(region);
            }
        }

        newRegions = DeleteSubregions(newRegions, unchangedRegions);

        unchangedRegions.AddRange(newRegions);
        unchangedRegions.Add(addUpperEMS(newOccupied));

        _emptyMaximalRegions = unchangedRegions;
    }

    private bool IsValidPlacement(Region newlyOccupied) 
    {
        bool valid = false;
        foreach (Region space in _emptyMaximalRegions)
        {
            if (newlyOccupied.IsSubregionOf(space))
            {
                
                valid = true;
            }
        }
        return valid;
    }
    
    private List<Region> DeleteSubregions(List<Region> newRegions, List<Region> unchangedRegions)
    {
        newRegions = newRegions.Distinct().ToList();

        List<Region> toRemove = new List<Region>();

        for (int i = 0; i < newRegions.Count; i++)
        {
            for (int j = i + 1; j < newRegions.Count; j++)
            {
                var a = newRegions[i];
                var b = newRegions[j];

                if (a.IsSubregionOf(b))
                    toRemove.Add(a);
                else if (b.IsSubregionOf(a))
                    toRemove.Add(b);
            }
        }

        foreach (Region region in unchangedRegions)
        {
            foreach (Region newRegion in newRegions)
            {
                if (newRegion.IsSubregionOf(region))
                {
                    toRemove.Add(newRegion);
                }
            }
        }

        foreach (Region region in toRemove)
        {
            newRegions.Remove(region);
        }

        return newRegions;
    }

    private int GetHeight()
    {
        return _emptyMaximalRegions[0].End.Z;
    }


    private Region addUpperEMS(Region occupied)
    {
        return new Region(new Coordinates(occupied.Start.X, occupied.Start.Y, occupied.End.Z), new Coordinates(occupied.End.X, occupied.End.Y, GetHeight()));
        // TODO: Implement that two spaces starting at the same height and neigbouring are merged
    }

}









