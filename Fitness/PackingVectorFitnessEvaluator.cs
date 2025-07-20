
public class PackingVectorFintessEvaluator:IFitnessEvaluator<PackingVector>
{
    IFitnessEvaluator<IReadOnlyList<Container>> ContainersFitnessEvaluator { get; init; }
    IPackingVectorSolver PackingVectorSolver { get; init; }

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
}