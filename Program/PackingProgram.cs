
public static class PackingProgram
{

    public static void Run(ProgramSetting setting)
    {
        PackingInput inputData = PackingInputLoader.LoadFromFile(setting.SourceJson);

        IFitnessEvaluator<PackingVector> fitnessEvaluator = CreateEvaluator(inputData, setting.SelectedPlacementHeuristics, setting.AllowRotations, setting.SelectedPackingOrderHeuristic);


        var initialPopulation = CreateInitialPopulation(inputData, setting.NumberOfIndividuals);
        var evaluator = CreateEvaluator(inputData, setting.SelectedPlacementHeuristics, setting.AllowRotations, setting.SelectedPackingOrderHeuristic);

        var evolutionary = EvolutionaryAlgorithms.GetEvolutionaryAlgorithm(setting.AlgorithmName, initialPopulation, evaluator);

        evolutionary.Evolve(setting.NumberOfGenerations);
        (var best, var bestFit) = evolutionary.GetBest();

        var solver = CreateSolver(inputData, setting.SelectedPlacementHeuristics, setting.AllowRotations, setting.SelectedPackingOrderHeuristic);
        
        var containers = solver.Solve(best);
      

        PackingOutputSaver.SaveToFile(containers, setting.OutputJson);
    }

    

    private static IPackingVectorSolver CreateSolver(PackingInput inputData, string[] selectedPlacementHeuristics, bool allowRotation, string? selectedPackingOrderHeuristic)
    {
        PackingVectorDecoder packingVectorDecoder = PackingVectorDecoder.Create(selectedPlacementHeuristics, allowRotation, selectedPackingOrderHeuristic);
        IBoxPacker boxPacker = new BoxPacker(inputData.ContainerProperties);
        IPackingVectorSolver packingVectorSolver = new PackingVectorSolver(packingVectorDecoder, inputData);
        return packingVectorSolver;
    }

    private static PackingVectorFintessEvaluator CreateEvaluator(PackingInput inputData, string[] selectedPlacementHeuristics, bool allowRotation, string? selectedPackingOrderHeuristic)
    {
        IPackingVectorSolver packingVectorSolver = CreateSolver(inputData, selectedPlacementHeuristics, allowRotation, selectedPackingOrderHeuristic);
        ContainersFitnessEvaluator containersFitnessEvaluator = new ContainersFitnessEvaluator();
        PackingVectorFintessEvaluator packingVectorFintessEvaluator = new PackingVectorFintessEvaluator(containersFitnessEvaluator, packingVectorSolver);
        return packingVectorFintessEvaluator;
    }


    public static IReadOnlyList<PackingVector> CreateInitialPopulation(PackingInput inputData, int populationSize)
    {
        IPopulationFactory<PackingVector> packingVectorFactory = new PackingVectorPopulationFactory(3*inputData.BoxesProperties.Length);
        return packingVectorFactory.CreatePopulation(populationSize);
        
    }
}

