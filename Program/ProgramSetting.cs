public static class PackingProgram
{

    public static Run(ProgramSetting setting)
    {
        PackingInput data = PackingInputLoader.LoadFromFile(setting.SourceJson);

        Evolutionary<PackingVector, double> = new Evolutionary<PackingVector, double>()






        PackingOutputSaver.SaveToFile(containers, setting.OutputJson);
    }

    private static IPackingVectorDecoder CreateDecoder(PlacementHeuristic[] selectedHeuristics, bool allowRotation, IComparer<BoxToBePacked>? packingOrderHeuristic)
    {
        IPackingVectorCellDecoder<PlacementHeuristic> placementHeuristic;
        if (selectedHeuristics.Length == 1)
        {
            placementHeuristic = new CellToOneHeuristicDecoder(selectedHeuristics[0]);
        }
        else
        {
            placementHeuristic = new CellToMultipleHeuristicsDecoder(selectedHeuristics);
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
            sorter = new HeuristicalBoxSorter(packingOrderHeuristic);
        }

        return new PackingVectorDecoder(placementHeuristic, rotations, sorter);
    }

    private static IPackingVectorSolver CreateSolver(PackingInput data, PlacementHeuristic[] selectedHeuristics, bool allowRotation, IComparer<BoxToBePacked>? packingOrderHeuristic)
    {
        IPackingVectorDecoder packingVectorDecoder = CreateDecoder(selectedHeuristics, allowRotation, packingOrderHeuristic);
        IBoxPacker boxPacker = new BoxPacker(data.ContainerProperties);
        IPackingVectorSolver packingVectorSolver = new PackingVectorSolver(packingVectorDecoder, boxPacker, data);
        return packingVectorSolver;
    }

    private static PackingVectorFintessEvaluator CreateEvaluator(PackingInput data, PlacementHeuristic[] selectedHeuristics, bool allowRotation, IComparer<BoxToBePacked>? packingOrderHeuristic)
    {
        IPackingVectorSolver packingVectorSolver = CreateSolver(data, selectedHeuristics, allowRotation, packingOrderHeuristic);
        ContainersFitnessEvaluator containersFitnessEvaluator = new ContainersFitnessEvaluator();
        PackingVectorFintessEvaluator packingVectorFintessEvaluator = new PackingVectorFintessEvaluator(containersFitnessEvaluator, packingVectorSolver);
        return packingVectorFintessEvaluator;
    }

    private static IEvolutionBlock<EvaluatedIndividual<PackingVector, double>> CreateBlock(PackingInput data, PlacementHeuristic[] selectedHeuristics, bool allowRotation, IComparer<BoxToBePacked>? packingOrderHeuristic)
    {
        PackingVectorFintessEvaluator packingVectorFintessEvaluator = CreateEvaluator(data, selectedHeuristics, allowRotation, packingOrderHeuristic);

        IEvolutionBlock<EvaluatedIndividual<PackingVector, double>> block = new DEPackingVectorEvolutionBlock(1000, packingVectorFintessEvaluator);

        return block;

    }
}

public readonly record struct ProgramSetting
{
    public string SourceJson { get; init; }
    public string OutputJson { get; init; }
    public PlacementHeuristic[] SelectedPlacementHeuristics { get; init; }
    public bool AllowRotations { get; init; }
    public IComparer<BoxToBePacked>? PackingOrderHeuristic { get; init; }
    public IAlgorithmParametrs AlgorithmParametrs { get; init; }
}

public interface IAlgorithmParametrs
{
    public string AlgorithmName { get; init; }


}