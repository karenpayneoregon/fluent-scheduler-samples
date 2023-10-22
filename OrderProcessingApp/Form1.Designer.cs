namespace OrderProcessingApp;

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
        OrdersCheckListBox = new CheckedListBox();
        ProcessButton = new Button();
        SuspendLayout();
        // 
        // OrdersCheckListBox
        // 
        OrdersCheckListBox.FormattingEnabled = true;
        OrdersCheckListBox.Location = new Point(64, 45);
        OrdersCheckListBox.Name = "OrdersCheckListBox";
        OrdersCheckListBox.Size = new Size(456, 202);
        OrdersCheckListBox.TabIndex = 0;
        // 
        // ProcessButton
        // 
        ProcessButton.Location = new Point(526, 45);
        ProcessButton.Name = "ProcessButton";
        ProcessButton.Size = new Size(201, 29);
        ProcessButton.TabIndex = 1;
        ProcessButton.Text = "Process";
        ProcessButton.UseVisualStyleBackColor = true;
        ProcessButton.Click += ProcessButton_Click;
        // 
        // Form1
        // 
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(800, 272);
        Controls.Add(ProcessButton);
        Controls.Add(OrdersCheckListBox);
        FormBorderStyle = FormBorderStyle.FixedToolWindow;
        Name = "Form1";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Orders";
        ResumeLayout(false);
    }

    #endregion

    private CheckedListBox OrdersCheckListBox;
    private Button ProcessButton;
}
