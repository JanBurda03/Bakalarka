public interface IElitism<T>
{
    public (IReadOnlyList<T> individuals, IReadOnlyList<double> fitnesses) GetElites(IReadOnlyList<T> population, IReadOnlyList<double> fitness, int numberOfElites);
}

