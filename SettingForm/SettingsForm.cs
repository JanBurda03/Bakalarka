
public class SettingsForm : Form
{
    private TextBox sourceJsonTextBox;
    private TextBox outputJsonTextBox;
    private CheckedListBox placementHeuristicsCheckedListBox;
    private CheckBox allowRotationsCheckBox;
    private ComboBox packingOrderComboBox;
    private ComboBox algorithmComboBox;
    private NumericUpDown numberOfIndividualsNumeric;
    private NumericUpDown numberOfGenerationsNumeric;
    private Button okButton;
    private Button cancelButton;

    public ProgramSetting ProgramSetting { get; private set; }

    public SettingsForm()
    {
        this.Text = "Nastavení programu";
        this.Width = 600;
        this.Height = 500;
        this.StartPosition = FormStartPosition.CenterScreen;

        InitializeComponents();
    }

    private void InitializeComponents()
    {
        int labelWidth = 180;
        int controlWidth = 350;
        int top = 10;
        int spacing = 30;

        AddSourceJSON();
        AddOutputJSON();

        // Placement heuristics
        AddLabel("Placement Heuristics:", 10, top, labelWidth);
        placementHeuristicsCheckedListBox = new CheckedListBox
        {
            Left = 200,
            Top = top,
            Width = controlWidth,
            Height = 80
        };
        placementHeuristicsCheckedListBox.Items.AddRange(PlacementHeuristics.PlacementHeuristicsArray);
        Controls.Add(placementHeuristicsCheckedListBox);
        top += 90;

        // Allow rotations
        allowRotationsCheckBox = new CheckBox
        {
            Left = 200,
            Top = top,
            Text = "Allow Rotations",
            AutoSize = true,
            Checked = true
        };
        this.Controls.Add(allowRotationsCheckBox);
        top += spacing;


        // Use NON-EVOL Heuristics
        var useNonEvolutionaryCheckBox = new CheckBox
        {
            Left = 200,
            Top = top,
            Text = "Use Non-Evolutionary Packing Order Heuristics",
            AutoSize = true,
            Checked = false
        };
        useNonEvolutionaryCheckBox.CheckedChanged += (s, e) =>
        {
            packingOrderComboBox.Enabled = useNonEvolutionaryCheckBox.Checked;
        };
        this.Controls.Add(useNonEvolutionaryCheckBox);
        top += spacing;

        // Packing order heuristic
        packingOrderComboBox = new ComboBox
        {
            Left = 200,
            Top = top,
            Width = controlWidth,
            DropDownStyle = ComboBoxStyle.DropDownList,
            Enabled = false
        };
        packingOrderComboBox.Items.AddRange(OrderHeuristics.OrderHeuristicsArray);
        this.Controls.Add(packingOrderComboBox);
        top += spacing;


        // Algorithm name
        AddLabel("Algorithm:", 10, top, labelWidth);
        algorithmComboBox = new ComboBox
        {
            Left = 200,
            Top = top,
            Width = controlWidth,
            DropDownStyle = ComboBoxStyle.DropDownList
        };
        algorithmComboBox.Items.AddRange(EvolutionaryAlgorithms.EvolutionaryAlgorithmsArray);
        this.Controls.Add(algorithmComboBox);
        top += spacing;

        // Number of Individuals
        AddLabel("Number of Individuals:", 10, top, labelWidth);
        numberOfIndividualsNumeric = new NumericUpDown
        {
            Left = 200,
            Top = top,
            Width = 100,
            Minimum = 1,
            Maximum = 100000,
            Value = 1000,
        };
        this.Controls.Add(numberOfIndividualsNumeric);
        top += spacing;

        // Number of Generations
        AddLabel("Number of Generations:", 10, top, labelWidth);
        numberOfGenerationsNumeric = new NumericUpDown
        {
            Left = 200,
            Top = top,
            Width = 100,
            Minimum = 1,
            Maximum = 100000,
            Value = 100
        };
        Controls.Add(numberOfGenerationsNumeric);
        top += spacing + 10;

        // OK Button
        okButton = new Button
        {
            Text = "OK",
            Left = 200,
            Top = top,
            Width = 100
        };
        okButton.Click += OkButton_Click;
        Controls.Add(okButton);

        // Cancel Button
        cancelButton = new Button
        {
            Text = "Cancel",
            Left = 310,
            Top = top,
            Width = 100
        };
        cancelButton.Click += (s, e) => { DialogResult = DialogResult.Cancel; Close(); };
        Controls.Add(cancelButton);





        void AddSourceJSON()
        {
            AddLabel("Source JSON:", 10, top, labelWidth);
            sourceJsonTextBox = new TextBox
            {
                Left = 200,
                Top = top,
                Width = controlWidth - 90,
                ReadOnly = true
            };
            var browseSourceButton = new Button
            {
                Text = "Browse...",
                Left = 200 + controlWidth - 85,
                Top = top - 1,
                Width = 80
            };
            browseSourceButton.Click += (s, e) =>
            {
                using var ofd = new OpenFileDialog
                {
                    Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*",
                    Title = "Choose Input JSON File"
                };
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    sourceJsonTextBox.Text = ofd.FileName;
                }
            };
            Controls.Add(sourceJsonTextBox);
            Controls.Add(browseSourceButton);
            top += spacing;
        }


        void AddOutputJSON()
        {
            AddLabel("Output JSON:", 10, top, labelWidth);
            outputJsonTextBox = new TextBox
            {
                Left = 200,
                Top = top,
                Width = controlWidth - 90,
                ReadOnly = true
            };
            var browseOutputButton = new Button
            {
                Text = "Browse",
                Left = 200 + controlWidth - 85,
                Top = top - 1,
                Width = 80
            };
            browseOutputButton.Click += (s, e) =>
            {
                using var sfd = new SaveFileDialog
                {
                    Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*",
                    Title = "Choose Output JSON File"
                };
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    outputJsonTextBox.Text = sfd.FileName;
                }
            };
            Controls.Add(outputJsonTextBox);
            Controls.Add(browseOutputButton);
            top += spacing;
        }
    }

    private void AddLabel(string text, int left, int top, int width)
    {
        var label = new Label
        {
            Text = text,
            Left = left,
            Top = top + 3,
            Width = width
        };
        this.Controls.Add(label);
    }

    private void InitializeComponent()
    {

    }

    private void OkButton_Click(object sender, EventArgs e)
    {

        // Ověření povinných vstupů
        if (string.IsNullOrWhiteSpace(sourceJsonTextBox.Text))
        {
            MessageBox.Show("Please select a source JSON file.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        if (string.IsNullOrWhiteSpace(outputJsonTextBox.Text))
        {
            MessageBox.Show("Please select an output JSON file.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        if (placementHeuristicsCheckedListBox.CheckedItems.Count == 0)
        {
            MessageBox.Show("Please select at least one placement heuristic.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        if (algorithmComboBox.SelectedItem == null)
        {
            MessageBox.Show("Please select an algorithm.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        if (packingOrderComboBox.Enabled && packingOrderComboBox.SelectedItem == null)
        {
            MessageBox.Show("Please select a packing order heuristic.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }


        ProgramSetting = new ProgramSetting
        {
            SourceJson = sourceJsonTextBox.Text,
            OutputJson = outputJsonTextBox.Text,
            SelectedPlacementHeuristics = placementHeuristicsCheckedListBox.CheckedItems.Cast<string>().ToArray(),
            AllowRotations = allowRotationsCheckBox.Checked,
            SelectedPackingOrderHeuristic = packingOrderComboBox.Enabled ? packingOrderComboBox.SelectedItem?.ToString() : null,
            AlgorithmName = algorithmComboBox.SelectedItem?.ToString() ?? "",
            NumberOfIndividuals = (int)numberOfIndividualsNumeric.Value,
            NumberOfGenerations = (int)numberOfGenerationsNumeric.Value
        };

        this.DialogResult = DialogResult.OK;
        this.Close();
    }
}
