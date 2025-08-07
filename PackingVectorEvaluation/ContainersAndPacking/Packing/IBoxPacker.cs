public interface IBoxPacker
{
    public void PackBoxes(IEnumerable<BoxToBePacked> boxesToBePacked);
    public IReadOnlyList<Container> GetContainers();
}