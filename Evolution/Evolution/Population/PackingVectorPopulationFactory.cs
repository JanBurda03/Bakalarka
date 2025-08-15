public class PackingVectorPopulationFactory : IPopulationFactory <PackingVector>
{
    private readonly int _packingVectorLength;

    public PackingVectorPopulationFactory(int packingVectorLength)
    {
        _packingVectorLength = packingVectorLength;
    }

    public IReadOnlyList<PackingVector> CreatePopulation(int numberOfIndividuals)
    {
        PackingVector[] population = new PackingVector[numberOfIndividuals];

        for (int i = 0; i < population.Length; i++) 
        {
            population[i] = PackingVector.CreateRandom(_packingVectorLength);
        }
        return Array.AsReadOnly(population);
    }


}
