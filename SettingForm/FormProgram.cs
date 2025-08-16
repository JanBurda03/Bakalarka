public static class FormProgram
{
    [STAThread]
    public static void Main() { Run(); }

    public static ProgramSetting? Run()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);

        using (var form = new SettingsForm())
        {
            if (form.ShowDialog() == DialogResult.OK)
            {
                return form.ProgramSetting;
            }
            else
            {
                return null;
            }
        }
    }
}