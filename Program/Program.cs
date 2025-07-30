
static class Program
{
    static void Main()
    {
        PackingInput data = PackingInputLoader.LoadFromFile("inputGPT.JSON");

        Console.WriteLine(data.GetLowerBound());

        IBoxPacker boxPacker = new BoxPacker(data.ContainerProperties);

        IPackingVectorDecoder packingVectorDecoder = new PackingVectorDecoder(new CellToOneHeuristicDecoder(PlacementHeuristics.MaxDistance), new CellToAllRotationsDecoder(), new PackingVectorBoxSorter());

        IPackingVectorSolver packingVectorSolver = new PackingVectorSolver(packingVectorDecoder, boxPacker, data);

        ContainersFitnessEvaluator containersFitnessEvaluator = new ContainersFitnessEvaluator();

        PackingVectorFintessEvaluator packingVectorFintessEvaluator = new PackingVectorFintessEvaluator(containersFitnessEvaluator, packingVectorSolver);

        EvaluatedIndividualPopulationFactory<PackingVector, double> factory = new EvaluatedIndividualPopulationFactory<PackingVector, double>(new PackingVectorPopulationFactory(3 * data.BoxesProperties.Length), packingVectorFintessEvaluator);


        var initialEvaluatedPopulation = factory.CreatePopulation(4000);

        var blocks = CreateBlocks(data, 4);

        Evolutionary<PackingVector, double> evolutionary = new Evolutionary<PackingVector, double>(initialEvaluatedPopulation, blocks, new Elitism<EvaluatedIndividual<PackingVector, double>>(0, true));


        evolutionary.Evolve(1000);

        // PackingOutputSaver.SaveToFile(containers, "output.JSON");








    }

    public static IReadOnlyList<IEvolutionBlock<EvaluatedIndividual<PackingVector, double>>> CreateBlocks(PackingInput data, int number)
    {
        IEvolutionBlock<EvaluatedIndividual<PackingVector, double>>[] blocks = new IEvolutionBlock<EvaluatedIndividual<PackingVector, double>>[number];

        for (int i = 0; i < number; i++) 
        {
            blocks[i] = CreateBlock(data);
        }
        return blocks;
    }

    public static IEvolutionBlock<EvaluatedIndividual<PackingVector, double>> CreateBlock(PackingInput data)
    {
        IPackingVectorDecoder packingVectorDecoder = new PackingVectorDecoder(new CellToOneHeuristicDecoder(PlacementHeuristics.FirstFit), new CellToAllRotationsDecoder(), new PackingVectorBoxSorter());

        IBoxPacker boxPacker = new BoxPacker(data.ContainerProperties);

        IPackingVectorSolver packingVectorSolver = new PackingVectorSolver(packingVectorDecoder, boxPacker, data);

        ContainersFitnessEvaluator containersFitnessEvaluator = new ContainersFitnessEvaluator();

        PackingVectorFintessEvaluator packingVectorFintessEvaluator = new PackingVectorFintessEvaluator(containersFitnessEvaluator, packingVectorSolver);

        IEvolutionBlock<EvaluatedIndividual<PackingVector, double>> block = new DEPackingVectorEvolutionBlock(1000, packingVectorFintessEvaluator);

        return block;

    }

    public static double GetFitness(IReadOnlyList<Container> containers)
    {
        double currentMin = double.MaxValue;
        foreach (var container in containers)
        {
            double value = Math.Max(container.GetRelativeWeight(), container.GetRelativeVolume());
            if (value < currentMin)
            {
                currentMin = value;
            }
        }
        return currentMin + containers.Count;
    }



    public static void ValidityChecker(IReadOnlyList<Container> containers)
    {

        foreach (Container container in containers)
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

