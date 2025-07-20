public class CellToMultipleHeuristicsDecoder : IPackingVectorCellDecoder<PlacementHeuristic>
{
    PlacementHeuristic[] Heuristics;
    public CellToMultipleHeuristicsDecoder(PlacementHeuristic[] heuristics)
    {
        Heuristics = heuristics;
    }

    public PlacementHeuristic Decode(PackingVectorCell cell)
    {
        int index = Math.Min((int)((double)cell * Heuristics.Length), Heuristics.Length - 1);
        return Heuristics[index];
    }
}

public class CellToOneHeuristicDecoder : IPackingVectorCellDecoder<PlacementHeuristic>
{
    PlacementHeuristic Heuristic;
    public CellToOneHeuristicDecoder(PlacementHeuristic heuristic)
    {
        Heuristic = heuristic;
    }
    public PlacementHeuristic Decode(PackingVectorCell cell)
    {
        return Heuristic;
    }
}