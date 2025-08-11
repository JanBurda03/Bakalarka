internal class MultipleHeuristicsDecoder : PackingVectorUsingPartDecoder<PlacementHeuristic>
{
    public MultipleHeuristicsDecoder(IReadOnlyList<PlacementHeuristic> heuristics) : base(heuristics) { }
}

internal class OneHeuristicDecoder : PackingVectorNonUsingPartDecoder<PlacementHeuristic>
{
    public OneHeuristicDecoder(PlacementHeuristic heuristic) : base(heuristic) { }
}