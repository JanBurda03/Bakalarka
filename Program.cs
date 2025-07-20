static class Program
{
    static void Main()
    {
        PackingInput data = PackingInputLoader.LoadFromFile("input4.JSON");

        Console.WriteLine(data.GetLowerBound());



        IPackingVectorDecoder packingVectorDecoder = new PackingVectorDecoder(new CellToOneHeuristicDecoder(PlacementHeuristics.FirstFit), new CellToAllRotationsDecoder(), new PackingVectorBoxSorter());

        IBoxPacker boxPacker = new BoxPacker(data.ContainerProperties);

        IPackingVectorSolver packingVectorSolver = new PackingVectorSolver(packingVectorDecoder, boxPacker, data);

        ContainersFitnessEvaluator containersFitnessEvaluator = new ContainersFitnessEvaluator();

        PackingVectorFintessEvaluator packingVectorFintessEvaluator = new PackingVectorFintessEvaluator(containersFitnessEvaluator, packingVectorSolver);

        IReadOnlyList<Container> containers= new List<Container>();

        for (int i = 0; i < 100; i++)
        {

            Console.WriteLine( packingVectorFintessEvaluator.EvaluateFitness(PackingVector.GenerateRandomPackingVector(3*data.BoxesProperties.Length)));

        }

        PackingOutputSaver.SaveToFile(containers, "output.JSON");

        

        
       



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

