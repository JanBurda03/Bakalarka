class Program
{
    static void Main()
    {
        PackingInput data = PackingInputLoader.LoadFromFile("input2.JSON");

        IEnumerable<BoxToBePacked> boxesToBePacked = DecodeVectorToPackingSequence();


        List<Container> containers = Packer.Pack(boxesToBePacked, data.ContainerProperties);

        PackingOutputSaver.SaveToFile(containers, "output.JSON");
    }
}

