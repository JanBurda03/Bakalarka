public interface IEmptyMaximalRegions
{
    public IEnumerable<Region> GetFitEMR(Sizes dimensions);

    public IEnumerable<Region> GetEMR();

    public void UpdateEMR(Region space);
}
