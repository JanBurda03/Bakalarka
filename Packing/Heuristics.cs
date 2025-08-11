public static class Heuristics
{
    public static IReadOnlyList<string> PlacementHeuristicsList => PlacementHeuristics.PlacementHeuristicsDictionary.Keys.ToArray();
    public static IReadOnlyList<string> OrderHeuristicsList => OrderHeuristics.OrderHeuristicsDictionary.Keys.ToArray();
}