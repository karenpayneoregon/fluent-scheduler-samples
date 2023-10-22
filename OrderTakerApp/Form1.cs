using OrdersLibrary;
using OrdersLibrary.Models;

namespace OrderTakerApp;

public partial class Form1 : Form
{
    public Form1()
    {
        InitializeComponent();
    }
    /// <summary>
    /// Add a new order to the database. 
    /// </summary>
    private async void AddOrderButton_Click(object sender, EventArgs e)
    {
        Orders order = await DataOperations.AddNewOrder();
        listBox1.Items.Add(order);
    }

    private async void ResetButton_Click(object sender, EventArgs e)
    {
        await DataOperations.UpdateTodayOrdersAsync();
    }
}
