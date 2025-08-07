public class CellToOneHeuristicDecoder : IPackingVectorCellDecoder<PlacementHeuristic>
{
    PlacementHeuristic Heuristic { get; init; }
    public CellToOneHeuristicDecoder(PlacementHeuristic heuristic)
    {
        Heuristic = heuristic;
    }
    public PlacementHeuristic Decode(PackingVectorCell cell)
    {
        return Heuristic;
    }
}