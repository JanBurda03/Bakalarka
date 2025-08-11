
class Program
{
    [STAThread]
    static void Main()
    {
        ProgramSetting? setting = FormProgram.Run();
        if (setting != null)
        {
            PackingProgram.Run((ProgramSetting)setting);
        }
    }
}