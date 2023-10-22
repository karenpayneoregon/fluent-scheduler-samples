using System.ComponentModel;
using System.Diagnostics;
using OrderProcessingApp.Classes;
using OrdersLibrary;
using OrdersLibrary.Models;
using Serilog;


namespace OrderProcessingApp;

public partial class Form1 : Form
{
    private BindingList<Orders> _bindingList;
    private BindingSource _bindingSource = new();
    public Form1()
    {
        InitializeComponent();

        Initialize();

        Closing += Form1_Closing;
        OrdersCheckListBox.ItemCheck += OrdersCheckListBox_ItemCheck;
    }

    private void Initialize()
    {
        _bindingList = new BindingList<Orders>(DataOperations.GetNewOrders());
        _bindingSource.DataSource = _bindingList;

        OrdersCheckListBox.DataSource = _bindingSource;

        JobController.Start();
        JobRegistry.ReportNewOrders += JobRegistry_ReportNewOrders;
    }

    /// <summary>
    /// Stop current job to prevent a run time exception
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Form1_Closing(object? sender, CancelEventArgs e)
    {
        JobController.Stop();
    }

    /// <summary>
    /// Get orders
    /// </summary>
    /// <param name="list"></param>
    private void JobRegistry_ReportNewOrders(List<Orders> list)
    {
        this.InvokeIfRequired(form => UpdateCheckedListBox(list));
    }

    /// <summary>
    /// Update OrdersCheckListBox if there are new orders.
    /// Note we must use InvokeRequired via
    /// <see cref="ControlExtensions.InvokeIfRequired&lt;T&gt;"/> since
    /// information is coming from anther thread in FluentScheduler library
    /// </summary>
    /// <param name="list">list of <see cref="Orders"/></param>
    private void UpdateCheckedListBox(List<Orders> list)
    {
        _bindingList = new BindingList<Orders>(list);
        _bindingSource.DataSource = _bindingList;
        
        OrdersCheckListBox.InvokeIfRequired(clb 
            => clb.DataSource = _bindingSource);

        CheckedListBoxCleanUp();
    }

    /// <summary>
    /// Process checked items of orders if any
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ProcessButton_Click(object sender, EventArgs e)
    {
        ProcessCurrentOrder();
    }

    private void ProcessCurrentOrder()
    {

        List<Orders> checkedOrders = OrdersCheckListBox.CheckedList<Orders>();
        Log.Information(checkedOrders.Count.ToString());
        if (checkedOrders.Count <= 0) return;

        DataOperations.ProcessOrders(checkedOrders);

        Log.Information("After processed orders in form");

        OrdersCheckListBox.DataSource = DataOperations.GetNewOrders();
        CheckedListBoxCleanUp();

    }

    private void OrdersCheckListBox_ItemCheck(object? sender, ItemCheckEventArgs e)
    {

        BeginInvoke(() =>
        {
            if (e.NewValue == CheckState.Checked)
            {
                ProcessCurrentOrder();
            }
            
        });

    }

    private void CheckedListBoxCleanUp()
    {
        if (_bindingSource.Count <= 0) return;

        OrdersCheckListBox.UnCheckAll();
        OrdersCheckListBox.SelectedIndex = OrdersCheckListBox.Items.Count - 1;

    }
}