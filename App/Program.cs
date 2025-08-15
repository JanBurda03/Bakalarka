
class Program
{
    [STAThread]
    static void Main()
    {
        ProgramSetting? possibleSetting = FormProgram.Run();
        if (possibleSetting != null)
        {
            var setting = (ProgramSetting)possibleSetting;
            var packingInput = PackingInputLoader.LoadFromFile(setting.SourceJson);
            Console.WriteLine(packingInput.GetLowerBound());
            var containers = EvolutionProgram.Run(setting, packingInput);
            PackingOutputSaver.SaveToFile(containers, setting.OutputJson);
        }
    }
}