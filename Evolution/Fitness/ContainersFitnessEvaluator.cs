public class ContainersFitnessEvaluator : IFitnessEvaluator<IReadOnlyList<ContainerData>>
{
    public double EvaluateFitness(IReadOnlyList<ContainerData> containers)
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