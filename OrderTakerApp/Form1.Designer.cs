namespace OrderTakerApp;

partial class Form1
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        AddOrderButton = new Button();
        listBox1 = new ListBox();
        ResetButton = new Button();
        SuspendLayout();
        // 
        // AddOrderButton
        // 
        AddOrderButton.Location = new Point(30, 28);
        AddOrderButton.Name = "AddOrderButton";
        AddOrderButton.Size = new Size(150, 29);
        AddOrderButton.TabIndex = 0;
        AddOrderButton.Text = "Add new order";
        AddOrderButton.UseVisualStyleBackColor = true;
        AddOrderButton.Click += AddOrderButton_Click;
        // 
        // listBox1
        // 
        listBox1.FormattingEnabled = true;
        listBox1.ItemHeight = 20;
        listBox1.Location = new Point(186, 28);
        listBox1.Name = "listBox1";
        listBox1.Size = new Size(334, 204);
        listBox1.TabIndex = 1;
        // 
        // ResetButton
        // 
        ResetButton.Location = new Point(30, 63);
        ResetButton.Name = "ResetButton";
        ResetButton.Size = new Size(150, 29);
        ResetButton.TabIndex = 2;
        ResetButton.Text = "Reset";
        ResetButton.UseVisualStyleBackColor = true;
        ResetButton.Click += ResetButton_Click;
        // 
        // Form1
        // 
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(544, 256);
        Controls.Add(ResetButton);
        Controls.Add(listBox1);
        Controls.Add(AddOrderButton);
        Name = "Form1";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Take orders";
        ResumeLayout(false);
    }

    #endregion

    private Button AddOrderButton;
    private ListBox listBox1;
    private Button ResetButton;
}
