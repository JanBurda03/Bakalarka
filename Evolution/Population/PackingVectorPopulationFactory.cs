public class PackingVectorPopulationFactory : IPopulationFactory <PackingVector>
{
    int PackingVectorLength { get; init; }

    public PackingVectorPopulationFactory(int packingVectorLength)
    {
        PackingVectorLength = packingVectorLength;
    }

    public IReadOnlyList<PackingVector> CreatePopulation(int numberOfIndividuals)
    {
        PackingVector[] population = new PackingVector[numberOfIndividuals];

        for (int i = 0; i < population.Length; i++) 
        {
            population[i] = PackingVector.GenerateRandomPackingVector(PackingVectorLength);
        }
        return Array.AsReadOnly(population);
    }


}
