public interface IPopulationReproducer<T>
{
    public IReadOnlyList<T> Reproduce(IReadOnlyList<T> currentPopulation, IReadOnlyList<IComparable> currentPopulationFitnesses);
}