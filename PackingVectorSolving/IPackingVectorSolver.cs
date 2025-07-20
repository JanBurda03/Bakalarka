public interface IPackingVectorSolver
{
    public IReadOnlyList<Container> Solve(PackingVector packingVector);
}