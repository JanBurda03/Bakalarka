

public class PackingVectorFitnessEvaluator:IMultipleFitnessEvaluator<PackingVector>
{
    // evaluating the fitness of packing vector firstly by finding a solution to it (filling the containers and returning list of them) and then evaluationg fitness of that solution
    private readonly IFitnessEvaluator<IReadOnlyList<ContainerData>> _containersFitnessEvaluator;
    private readonly PackingVectorSolver _packingVectorSolver;

    public PackingVectorFitnessEvaluator(IFitnessEvaluator<IReadOnlyList<ContainerData>> containersFitnessEvaluator, PackingVectorSolver packingVectorSolver)
    {
        _containersFitnessEvaluator = containersFitnessEvaluator;
        _packingVectorSolver = packingVectorSolver;
    }

    public double EvaluateFitness(PackingVector packingVector)
    {
        IReadOnlyList<ContainerData> containers = _packingVectorSolver.Solve(packingVector);
        return _containersFitnessEvaluator.EvaluateFitness(containers);
    }

    public IReadOnlyList<double> EvaluateFitnesses(IReadOnlyList<PackingVector> packingVectors)
    {
        double[] fitnesses = new double[packingVectors.Count];

        Parallel.For(0, packingVectors.Count, i =>
        {
            fitnesses[i] = EvaluateFitness(packingVectors[i]);
        }); 

        return fitnesses;
    }

    public static PackingVectorFitnessEvaluator Create(PackingInput inputData, PackingSetting packingSetting)
    {
        PackingVectorSolver packingVectorSolver = PackingProgram.CreateSolver(inputData, packingSetting);
        ContainersFitnessEvaluator containersFitnessEvaluator = new ContainersFitnessEvaluator();
        PackingVectorFitnessEvaluator packingVectorFintessEvaluator = new PackingVectorFitnessEvaluator(containersFitnessEvaluator, packingVectorSolver);
        return packingVectorFintessEvaluator;
    }
}