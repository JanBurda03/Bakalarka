
static class Program
{
    static void Main()
    {
        PackingInput data = PackingInputLoader.LoadFromFile("inputGPT.JSON");

        Console.WriteLine(data.GetLowerBound());

        IBoxPacker boxPacker = new BoxPacker(data.ContainerProperties);

        PackingVectorDecoder packingVectorDecoder = new PackingVectorDecoder(new CellToOneHeuristicDecoder(PlacementHeuristics.MaxDistance), new CellToAllRotationsDecoder(), new PackingVectorBoxSorter());

        IPackingVectorSolver packingVectorSolver = new PackingVectorSolver(packingVectorDecoder, data);


        PackingVectorFintessEvaluator evaluator = new PackingVectorFintessEvaluator(new ContainersFitnessEvaluator(), packingVectorSolver);

        var factory = new PackingVectorPopulationFactory(data.BoxesProperties.Length * 3);

        PackingVectorDifferentialEvolution x = new PackingVectorDifferentialEvolution(factory.CreatePopulation(1000), evaluator);



        x.Evolve(4000);

        //PackingOutputSaver.SaveToFile(containers, "output.JSON");








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

