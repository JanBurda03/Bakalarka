
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
        Text = "Program Setting";
        Width = 600;
        Height = 500;
        StartPosition = FormStartPosition.CenterScreen;

        InitializeComponents();


        BackColor = Color.RebeccaPurple;

        foreach (Control ctrl in this.Controls)
        {

            if (ctrl is Label)
            {
                ctrl.ForeColor = Color.White;
            }

            else if (ctrl is Button btn)
            {
                btn.BackColor = Color.ForestGreen;
                btn.ForeColor = Color.White;
                btn.FlatStyle = FlatStyle.Flat;
            }

            else if (ctrl is ComboBox cmb)
            {
                cmb.BackColor = Color.White;
                cmb.ForeColor = Color.Black;
                cmb.FlatStyle = FlatStyle.Flat;
            }


            else if (ctrl is CheckBox cb)
            {
                cb.ForeColor = Color.White;
            }
        }
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
        placementHeuristicsCheckedListBox.Items.AddRange(PlacementHeuristics.PlacementHeuristicsList.ToArray());
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
            Enabled = false,
            DropDownStyle = ComboBoxStyle.DropDownList
    };
        packingOrderComboBox.Items.AddRange(OrderHeuristics.OrderHeuristicsList.ToArray());
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
        algorithmComboBox.SelectedIndex = 0;
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
        Controls.Add(numberOfIndividualsNumeric);
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
        Controls.Add(label);
    }


    private void OkButton_Click(object sender, EventArgs e)
    {

        if (Errors())
        {
            return;
        }


        var selectedPlacementHeuristics = placementHeuristicsCheckedListBox.CheckedItems.Cast<string>().ToArray();
        var allowRotations = allowRotationsCheckBox.Checked;
        var selectedPackingOrderHeuristic = packingOrderComboBox.Enabled ? packingOrderComboBox.SelectedItem?.ToString() : null;


        ProgramSetting = new ProgramSetting(
            sourceJsonTextBox.Text,
            outputJsonTextBox.Text,
            new PackingSetting(selectedPlacementHeuristics, allowRotations, selectedPackingOrderHeuristic),
            algorithmComboBox.SelectedItem.ToString(),
            (int)numberOfIndividualsNumeric.Value,
            (int)numberOfGenerationsNumeric.Value
            );

        DialogResult = DialogResult.OK;
        Close();
    }

    private bool Errors()
    {
        if (string.IsNullOrWhiteSpace(sourceJsonTextBox.Text))
        {
            MessageBox.Show("Please select a source JSON file.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return true;
        }

        if (string.IsNullOrWhiteSpace(outputJsonTextBox.Text))
        {
            MessageBox.Show("Please select an output JSON file.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return true;
        }

        if (placementHeuristicsCheckedListBox.CheckedItems.Count == 0)
        {
            MessageBox.Show("Please select at least one placement heuristic.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return true;
        }

        if (algorithmComboBox.SelectedItem == null)
        {
            MessageBox.Show("Please select an algorithm.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return true;
        }

        if (packingOrderComboBox.Enabled && packingOrderComboBox.SelectedItem == null)
        {
            MessageBox.Show("Please select a packing order heuristic.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return true;
        }
        return false;
    }
}
