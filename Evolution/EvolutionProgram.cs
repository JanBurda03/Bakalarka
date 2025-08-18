
public static class EvolutionProgram
{
    public static void Main() { }

    public static IReadOnlyList<ContainerData> Run(ProgramSetting setting, PackingInput packingInput)
    {
        var initialPopulation = CreateInitialPopulation(packingInput, setting.PackingSetting, setting.NumberOfIndividuals);
        var evaluator = PackingVectorFitnessEvaluator.Create(packingInput, setting.PackingSetting);

        double stopValue = packingInput.GetLowerBound() + 1;
        var evolutionary = EvolutionaryAlgorithms.GetEvolutionaryAlgorithm(setting.AlgorithmName, initialPopulation, evaluator, null, stopValue);
        evolutionary.Evolve(setting.NumberOfGenerations);
        var best = evolutionary.GlobalBest.individual;

        var solution = PackingProgram.Solve(best, packingInput, setting.PackingSetting);

        PackingValidityChecker(solution);
        return solution;
    }
  

    public static IReadOnlyList<PackingVector> CreateInitialPopulation(PackingInput packingInput, PackingSetting packingSetting, int populationSize)
    {
        IPopulationFactory<PackingVector> packingVectorFactory = new PackingVectorPopulationFactory(PackingProgram.GetPackingVectorExpectedMinimalLength(packingInput, packingSetting));
        return packingVectorFactory.CreatePopulation(populationSize);
        
    }

    public static void PackingValidityChecker(IReadOnlyList<ContainerData> containers)
    {

        foreach (ContainerData container in containers)
        {
            var containerRegion = container.ContainerProperties.Sizes.ToRegion(new Coordinates(0, 0, 0));

            IReadOnlyList<PackedBox> packedBoxes = container.PackedBoxes;

            for (int i = 0; i < packedBoxes.Count; i++) 
            { 
                for (int j = i+1;  j < packedBoxes.Count; j++)
                {
                    var box1 = packedBoxes[i];
                    var box2 = packedBoxes[j];

                    if (box1.PlacementInfo.OccupiedRegion.IntersectsWith(box2.PlacementInfo.OccupiedRegion) || !box1.PlacementInfo.OccupiedRegion.IsSubregionOf(containerRegion) || !box2.PlacementInfo.OccupiedRegion.IsSubregionOf(containerRegion))
                    {
                        throw new Exception($"Fatal packing program error, in container {container.ID}, box {box1} intersects with {box2}!");
                    }
                }
            }
        }
    }
}

