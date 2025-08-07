
public class PackingVectorFintessEvaluator:IMultipleFitnessEvaluator<PackingVector>
{
    private IFitnessEvaluator<IReadOnlyList<Container>> ContainersFitnessEvaluator { get; init; }
    private IPackingVectorSolver PackingVectorSolver { get; init; }

    public PackingVectorFintessEvaluator(IFitnessEvaluator<IReadOnlyList<Container>> containersFitnessEvaluator, IPackingVectorSolver packingVectorSolver)
    {
        ContainersFitnessEvaluator = containersFitnessEvaluator;
        PackingVectorSolver = packingVectorSolver;
    }

    public double EvaluateFitness(PackingVector packingVector)
    {
        IReadOnlyList<Container> containers = PackingVectorSolver.Solve(packingVector);
        return ContainersFitnessEvaluator.EvaluateFitness(containers);
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
}