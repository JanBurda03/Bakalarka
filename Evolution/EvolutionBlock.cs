public class EvolutionBlock<T,U>: IEvolutionBlock<T,U> where U: IComparable<U>
{
    TournamentSelector<T> Selector { get; init; }
    public IReadOnlyList<EvaluatedIndividiual<T, U>> NextPartialGeneration(IReadOnlyList<EvaluatedIndividiual<T,U> > CurrentEvaluatedPopulation)
    {
        
    }
}