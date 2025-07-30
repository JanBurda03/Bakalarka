public interface IMutator<T>
{
    public T Mutate(T v1, T v2);
}

public class UniformMutator:IMutator<PackingVector>
{
    private double Prob { get; init; }
    private Random random { get; init; }

    public UniformMutator(double prob)
    {
        Prob = prob;
        random = new Random();
    }

    public PackingVector Mutate(PackingVector a, PackingVector b)
    {
        if (a.Length != b.Length)
            throw new ArgumentException("PackingVectors must have the same length!");

        PackingVectorCell[] result = new PackingVectorCell[a.Length];
        for (int i = 0; i < a.Length; i++)
        {
            if (random.NextDouble() < Prob)
            {
                result[i] = a[i];
            }
            else
            {
                result[i] = b[i];
            }

        }
        return new PackingVector(result);
    }
}

public class DEPackingVectorEvolutionBlock : IEvolutionBlock<EvaluatedIndividual<PackingVector, double>>
{ 
    public ISelector<EvaluatedIndividual<PackingVector, double>> Selector { get; init; }
    public ISelector<EvaluatedIndividual<PackingVector, double>> RandomSelector { get; init; }

    public IMutator<PackingVector> Mutator { get; init; }

    public IFitnessEvaluator<PackingVector, double> FitnessEvaluator { get; init; }

    public DEPackingVectorEvolutionBlock(int numberToSelect, IFitnessEvaluator<PackingVector, double> fitnessEvaluator)
    {
        Selector = new TournamentSelector<EvaluatedIndividual<PackingVector, double>>(numberToSelect, 21, true);
        RandomSelector = new TournamentSelector<EvaluatedIndividual<PackingVector, double>>(numberToSelect, 1, true);
        FitnessEvaluator = fitnessEvaluator;
        Mutator = new UniformMutator(0.8);
    }

    public IReadOnlyList<EvaluatedIndividual<PackingVector, double>> NextPartialGeneration(IReadOnlyList<EvaluatedIndividual<PackingVector, double> > CurrentEvaluatedPopulation)
    {
        IReadOnlyList<EvaluatedIndividual<PackingVector, double>> selected = Selector.Select(CurrentEvaluatedPopulation);
        IReadOnlyList<EvaluatedIndividual<PackingVector, double>> random = RandomSelector.Select(CurrentEvaluatedPopulation);

        EvaluatedIndividual<PackingVector, double>[] newPopulation = new EvaluatedIndividual<PackingVector, double>[selected.Count];

        for (int i = 0; i < selected.Count; i++) 
        {
            


            var newVector = Mutator.Mutate(selected[i].Item, random[i].Item);
            var fitness = FitnessEvaluator.EvaluateFitness(newVector);



            if (fitness < selected[i].Fitness)
            {
                newPopulation[i] = new EvaluatedIndividual<PackingVector, double>(newVector, fitness);
            }
            else
            {
                newPopulation[i] = selected[i];
            }

        }
        return Array.AsReadOnly(newPopulation);

    }

}