public interface IEvolutionary<T>
{
    public IReadOnlyList<T> CurrentPopulation { get; }
    public IReadOnlyList<double> CurrentFitness { get; }
    public int CurrentGeneration { get; }

    public void Evolve(int numberOfGenerations);

    public (T, double) GetBest();
}