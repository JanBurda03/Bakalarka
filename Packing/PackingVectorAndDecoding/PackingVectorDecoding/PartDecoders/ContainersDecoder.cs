internal class MultipleContainersDecoder : PackingVectorUsingPartDecoder<ContainerProperties>
{
    public MultipleContainersDecoder(IReadOnlyList<ContainerProperties> containers) : base(containers) { }
}

internal class OneContainerDecoder : PackingVectorNonUsingPartDecoder<ContainerProperties>
{
    public OneContainerDecoder(ContainerProperties container) : base(container) { }
}