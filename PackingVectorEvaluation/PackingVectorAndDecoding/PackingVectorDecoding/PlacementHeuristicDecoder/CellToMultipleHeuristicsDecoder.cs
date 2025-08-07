public class CellToMultipleHeuristicsDecoder : IPackingVectorCellDecoder<PlacementHeuristic>
{
    IReadOnlyList<PlacementHeuristic> Heuristics { get; init; }
    public CellToMultipleHeuristicsDecoder(IReadOnlyList<PlacementHeuristic> heuristics)
    {
        Heuristics = heuristics;
    }

    public PlacementHeuristic Decode(PackingVectorCell cell)
    {
        int index = Math.Min((int)((double)cell * Heuristics.Count), Heuristics.Count - 1);
        return Heuristics[index];
    }
}

