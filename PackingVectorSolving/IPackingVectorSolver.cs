public interface IPackingVectorSolver : IParallelSafeCloneable<IPackingVectorSolver>
{
    public IReadOnlyList<Container> Solve(PackingVector packingVector);
}