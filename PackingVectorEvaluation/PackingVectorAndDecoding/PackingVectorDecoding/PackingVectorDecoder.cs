

public class PackingVectorDecoder
{
    private IPackingVectorCellDecoder<PlacementHeuristic> CellToHeuristicDecoder { get; init; }
    private IPackingVectorCellDecoder<Rotation> CellToRotationDecoder { get; init; }

    private IBoxToBePackedSorter Sorter { get; init; }

    public PackingVectorDecoder(IPackingVectorCellDecoder<PlacementHeuristic> cellToHeuristicDecoder, IPackingVectorCellDecoder<Rotation> cellToRotationDecoder, IBoxToBePackedSorter sorter)
    {
        CellToHeuristicDecoder = cellToHeuristicDecoder;
        CellToRotationDecoder = cellToRotationDecoder;
        Sorter = sorter;
    }

    public BoxToBePacked[] Decode(PackingVector packingVector, BoxProperties[] boxes)
    {
        BoxToBePacked[] boxesToBePackedUnsorted = ConvertUnsorted(packingVector, boxes);
        return Sorter.Sort(boxesToBePackedUnsorted, packingVector);
    }

    private BoxToBePacked[] ConvertUnsorted(PackingVector packingVector, BoxProperties[] boxes)
    {
        ReadOnlySpan<PackingVectorCell> valuesHeuristics = packingVector.GetPlacementHeuristicPart();
        ReadOnlySpan<PackingVectorCell> valuesRorations = packingVector.GetRotationPart();

        BoxToBePacked[] boxesToBePacked = new BoxToBePacked[valuesHeuristics.Length];

        if (boxes.Length > valuesHeuristics.Length || boxes.Length > valuesRorations.Length)
        {
            throw new Exception();
        }

        for (int i = 0; i < boxes.Length;i++)
        {
            boxesToBePacked[i] = new BoxToBePacked(boxes[i], CellToRotationDecoder.Decode(valuesRorations[i]), CellToHeuristicDecoder.Decode(valuesHeuristics[i]));
        }
        return boxesToBePacked;
    }

    public static PackingVectorDecoder Create(string[] selectedHeuristics, bool allowRotation, string? packingOrderHeuristic)
    {
        IPackingVectorCellDecoder<PlacementHeuristic> placementHeuristic;
        if (selectedHeuristics.Length == 1)
        {
            placementHeuristic = new CellToOneHeuristicDecoder(PlacementHeuristics.GetPlacementHeuristic(selectedHeuristics[0]));
        }
        else
        {
            placementHeuristic = new CellToMultipleHeuristicsDecoder(PlacementHeuristics.GetMultiplePlacementHeuristics(selectedHeuristics));
        }

        IPackingVectorCellDecoder<Rotation> rotations;
        if (allowRotation)
        {
            rotations = new CellToAllRotationsDecoder();
        }
        else
        {
            rotations = new CellToOneRotationDecoder(Rotation.XYZ);
        }

        IBoxToBePackedSorter sorter;
        if (packingOrderHeuristic == null)
        {
            sorter = new PackingVectorBoxSorter();
        }
        else
        {
            sorter = new HeuristicalBoxSorter(OrderHeuristics.GetOrderHeuristic(packingOrderHeuristic));
        }

        return new PackingVectorDecoder(placementHeuristic, rotations, sorter);
    }
}