public class ContainersFitnessEvaluator : IFitnessEvaluator<IReadOnlyList<Container>, double>
{
    public double EvaluateFitness(IReadOnlyList<Container> containers)
    {
        double currentMin = double.MaxValue;
        foreach (var container in containers)
        {
            double value = Math.Max(container.GetRelativeWeight(), container.GetRelativeVolume());
            if (value < currentMin)
            {
                currentMin = value;
            }
        }
        return currentMin + containers.Count;
    }
}