public interface IEvolutionary<T>
{
    public int CurrentGeneration { get; }
    public IReadOnlyList<T> CurrentGenerationPopulation { get; }
    public IReadOnlyList<double> CurrentGenerationFitness { get; }
    public (T individual, double fitness) CurrentGenerationBest { get; }
    public (T individual, double fitness) GlobalBest { get; }

    public void Evolve(int numberOfGenerations);
}