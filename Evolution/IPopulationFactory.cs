public interface IPopulationFactory<T>
{
    public T[] CreatePopulation(int numberOfIndividuals);
}