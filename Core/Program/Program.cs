
public static class Program
{
    public static void Main() { }

    public static void Run(ProgramSetting setting)
    {
        PackingInput inputData = PackingInputLoader.LoadFromFile(setting.SourceJson);


        var initialPopulation = CreateInitialPopulation(inputData, setting.PackingSetting, setting.NumberOfIndividuals);
        var evaluator = CreateEvaluator(inputData, setting.PackingSetting);

        var evolutionary = EvolutionaryAlgorithms.GetEvolutionaryAlgorithm(setting.AlgorithmName, initialPopulation, evaluator);

        evolutionary.Evolve(setting.NumberOfGenerations);
        (var best, _) = evolutionary.GetBest();

        var solver = PackingProgram.CreateSolver(inputData, setting.PackingSetting);

        var containers = solver.Solve(best);

        ValidityChecker(containers);


        PackingOutputSaver.SaveToFile(containers, setting.OutputJson);
    }
  

    private static PackingVectorFintessEvaluator CreateEvaluator(PackingInput inputData, PackingSetting packingSetting)
    {
        PackingVectorSolver packingVectorSolver = PackingProgram.CreateSolver(inputData, packingSetting);
        ContainersFitnessEvaluator containersFitnessEvaluator = new ContainersFitnessEvaluator();
        PackingVectorFintessEvaluator packingVectorFintessEvaluator = new PackingVectorFintessEvaluator(containersFitnessEvaluator, packingVectorSolver);
        return packingVectorFintessEvaluator;
    }


    public static IReadOnlyList<PackingVector> CreateInitialPopulation(PackingInput packingInput, PackingSetting packingSetting, int populationSize)
    {
        IPopulationFactory<PackingVector> packingVectorFactory = new PackingVectorPopulationFactory(PackingProgram.GetPackingVectorExpectedMinimalLength(packingInput, packingSetting));
        return packingVectorFactory.CreatePopulation(populationSize);
        
    }

    public static void ValidityChecker(IReadOnlyList<ContainerData> containers)
    {

        foreach (ContainerData container in containers)
        {
            IReadOnlyList<PackedBox> packedBoxes = container.PackedBoxes;

            foreach (PackedBox box1 in packedBoxes)
            {
                foreach (PackedBox box2 in packedBoxes)
                {
                    if (box1.PlacementInfo.OccupiedRegion.IntersectsWith(box2.PlacementInfo.OccupiedRegion) && box1.BoxProperties.Id != box2.BoxProperties.Id)
                    {
                        throw new Exception("Interscect");
                    }
                }
            }
        }
    }
}

