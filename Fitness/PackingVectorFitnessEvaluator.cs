
public class PackingVectorFintessEvaluator:IFitnessEvaluator<PackingVector, double>
{
    IFitnessEvaluator<IReadOnlyList<Container>, double> ContainersFitnessEvaluator { get; init; }
    IPackingVectorSolver PackingVectorSolver { get; init; }

    public PackingVectorFintessEvaluator(IFitnessEvaluator<IReadOnlyList<Container>, double> containersFitnessEvaluator, IPackingVectorSolver packingVectorSolver)
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