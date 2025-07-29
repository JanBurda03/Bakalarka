public class PackingVectorArrayFintessesEvaluator : IMultipleFitnessesEvaluator<PackingVector, double> 
{
    IFitnessEvaluator<PackingVector, double> PackingVectorFitnessEvaluator { get; init; }

    public PackingVectorArrayFintessesEvaluator(IFitnessEvaluator<PackingVector, double> packingVectorFitnessEvaluator)
    {
        PackingVectorFitnessEvaluator = packingVectorFitnessEvaluator;
    }

    public double[] EvaluateFitnesses(IReadOnlyList<PackingVector> packingVectors)
    {
        double[] results = new double[packingVectors.Count];

        for (int i = 0; i < packingVectors.Count; i++)
        {
            results[i] = PackingVectorFitnessEvaluator.EvaluateFitness(packingVectors[i]);
        }

        return results;
    }
}