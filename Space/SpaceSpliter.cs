public interface ISpaceSpliter
{
    public IEnumerable<Space> SplitSpace(Space original, Space newOccupied);
}

public class EmptyMaximalSpaceSpliter : ISpaceSpliter
{
    public IEnumerable<Space> SplitSpace(Space original, Space newOccupied)
    {
        List<Space> spaces = new List<Space>();

        if (original.Start.Z < newOccupied.Start.Z)
        {
            throw new Exception("Newly placed object must always be fully on base");
        }

        if (original.Start.Z > newOccupied.Start.Z)
        {
            throw new Exception("Empty maximal space must always be fully on base");
        }

        int a_start = original.Start.X;
        int b_start = original.Start.Y;

        int a_end = original.End.X;
        int b_end = original.End.Y;

        int x_start = newOccupied.Start.X;
        int y_start = newOccupied.Start.Y;

        int x_end = newOccupied.End.X;
        int y_end = newOccupied.End.Y;

        if (x_start > a_start)
        {
            spaces.Add(new Space(new Coordinates(a_start, b_start, original.Start.Z), new Coordinates(x_start, b_end, original.End.Z)));
        }

        if (y_start > b_start)
        {
            spaces.Add(new Space(new Coordinates(a_start, b_start, original.Start.Z), new Coordinates(a_end, y_start, original.End.Z)));
        }

        if (x_end < a_end)
        {
            spaces.Add(new Space(new Coordinates(x_end, b_start, original.Start.Z), new Coordinates(a_end, b_end, original.End.Z)));
        }

        if (y_end < b_end)
        {
            spaces.Add(new Space(new Coordinates(a_start, y_end, original.Start.Z), new Coordinates(a_end, b_end, original.End.Z)));

        }

        return spaces;
    }
}