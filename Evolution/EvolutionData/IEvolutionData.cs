public interface IEvolutionData<T>
{
    public void Update(IReadOnlyList<T> currentPopulation, IReadOnlyList<double> currentFitness, (T, double) currentBest, (T, double) currentGenerationBest, int currentGeneration);
}