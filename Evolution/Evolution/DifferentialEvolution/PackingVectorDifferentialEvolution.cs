public class PackingVectorDifferentialEvolution : DifferentialEvolution<PackingVector>
{
    private const double Prob = 0.85;
    private const int TournamentSize = 4;

    public PackingVectorDifferentialEvolution(IReadOnlyList<PackingVector> initialPopulation, IMultipleFitnessEvaluator<PackingVector> fitnessEvaluator, IEvolutionData<PackingVector>? data = null, double? hardStop = null) : base(initialPopulation, fitnessEvaluator, new PackingVectorUniformMutator(Prob), TournamentSize, true, data, hardStop)
    {

    }
}