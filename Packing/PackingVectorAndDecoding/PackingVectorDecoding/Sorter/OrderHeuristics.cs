internal static class OrderHeuristics
{
    public static readonly IReadOnlyDictionary<string, IComparer<BoxToBePacked>> OrderHeuristicsDictionary = new Dictionary<string, IComparer<BoxToBePacked>>
        {
            {"HighVolumeFirstHeuristic", new HighVolumeFirstHeuristic() },
            {"HighAreaBaseFirstHeuristic", new HighAreaBaseFirstHeuristic()},
            {"LongestFirstHeuristic", new LongestFirstHeuristic()}
           
        };

    public static IComparer<BoxToBePacked> GetOrderHeuristic(string orderHeuristic)
    {
        if (OrderHeuristicsDictionary.TryGetValue(orderHeuristic, out var comparer))
            return comparer;

        throw new ArgumentException($"Unknown heuristic: {orderHeuristic}");
    }
}


internal class HighVolumeFirstHeuristic : IComparer<BoxToBePacked>
{
    public int Compare(BoxToBePacked a, BoxToBePacked b)
    {
        long sa = a.BoxProperties.Sizes.GetVolume();
        long sb = b.BoxProperties.Sizes.GetVolume();
        return sb.CompareTo(sa);
    }
}

internal class HighAreaBaseFirstHeuristic : IComparer<BoxToBePacked>
{
    public int Compare(BoxToBePacked a, BoxToBePacked b)
    {
        Sizes sa = a.GetRotatedSizes();
        Sizes sb = b.GetRotatedSizes();

        return ((long)sb.X * sb.Y).CompareTo((long)sa.X * sa.Y);
    }
}

internal class LongestFirstHeuristic : IComparer<BoxToBePacked>
{
    public int Compare(BoxToBePacked a, BoxToBePacked b)
    {
        Sizes sa = a.BoxProperties.Sizes;
        Sizes sb = b.BoxProperties.Sizes;

        return (Math.Max(Math.Max(sb.X, sb.Y), sb.Z)).CompareTo(Math.Max(Math.Max(sa.X, sa.Y), sa.Z));
    }
}

