# FluentScheduler code sample

Learn how to create a background service using NuGet package [FluentScheduler](https://fluentscheduler.github.io/) in a Windows Forms project without a need for a service running. FluentScheduler is not as robust as the popular Quartz.NET library but for small task FluentScheduler works great.

Only downside to FluentScheduler is the lack of documentation which can turn away novice developers. All code samples are presented with console projects using SeriLog to log information within jobs.

Since most samples are done in console projects, we will walk through running two Windows Forms projects where one adds data and the other receives data in real time from the first application and alters the data. Code for the most part has been kept simple for ease of learning.

The code sample is to mimic taking an order in one Windows Form application and report the new order in another Windows Form application. Think of the first application as a waitress taking an order while the second application is for the cook to see and process.

To keep code simple there are no order details, just an order with a primary key, order date and time. All data is exposed in a CheckedListBox so that in the second application by checking one or more items and clicking a button the items are marked as being processed and then removed from the CheckedListBox.

## Dependency Injection

Currently, the library supports dependency injection of jobs (via **IJobFactory**). However, you shouldn't use it, it's a bad idea on its way to be deprecated.

## Database operations

- A SQL-Server localdb database is used.
- To start off create the database with the script under the project OrdersLibrary/Scripts (which has instructions)
- All data operations are done in OrdersLibrary library using Microsoft Entity Framework Core (EF Core).
- Both Windows Forms projects use OrdersLibrary library.
- Classes for EF Core were created with a Visual Studio extension [EF Power Tools](https://marketplace.visualstudio.com/items?itemName=ErikEJ.EFCorePowerTools)

## Step through code on the processing side

JobRegistry class constructor sets up a job to run which specifies a methods in the Schedule of the base Registry.

```csharp
Schedule(PerformWork)
    .WithName(jobName)
    .ToRunEvery(interval)
    .Seconds();
```

- PerformWork is passed as a [method group](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/language-specification/expressions#122-expression-classifications).
- WithName allows code later to stop this job in JobController
- ToRunEvery accepts a value to run every n seconds. In this case, when triggered 10 seconds will passed before PerformWork is triggered

Suppose PerformWork should run immediately then every 10 seconds, use the following where ToRunNow indicates to run immediately and AndEvery, in this case every 10 seconds.

```csharp
Schedule(PerformWork)
    .WithName(jobName)
    .ToRunNow()
    .AndEvery(interval)
    .Seconds();
```

PerformWork method gets orders not processed followed by invoking ReportNewOrders event. The form listens via

```csharp
JobRegistry.ReportNewOrders += JobRegistry_ReportNewOrders;
```

Which **and this is important** in the event we must call Invoke done in the language extension InvokeIfRequired as data is coming from another thread. If we did not use Invoke, no run time errors but the CheckedListBox would never show new orders coming in.

```csharp
private void JobRegistry_ReportNewOrders(List<Orders> list)
{
    this.InvokeIfRequired(form => UpdateCheckedListBox(list));
}
```

## Form Code

From the constructor, orders are read into a BindingList&lt;Orders> which feeds into a BindingSource and the BindingSource becomes the DataSource for the CheckedListBox.

Next the job is triggered via `JobController.Start();` followed by subscribing to `ReportNewOrders` event of `JobRegistry` which in turn allows real time updates from the database table to the CheckedListBox.

Next, we subscribe to form closing event to stop the job else its possible to get a runtime exception if an object is not set to an instance of an object in the process.

### Processing an order

Check an item/order, press the process button. This in turn gets the checked items (more than one can be checked and processed but in real life it would be one order at a time), sends the orders to the database class which updates the orders followed by ensuring no items are checked (ran into this once or twice) then sets the selected item as the last item/order.

Wait a minute, how about processing an order when checking an order in the CheckedListBox?

Yes we can but must call [BeginInvoke](https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.control.begininvoke?view=windowsdesktop-7.0) that without this call when asking for checked orders we get a count of 0, had to Google this and it turns out to be an issue with the actual CheckedListBox.

So let's take the code from the process button and place the code into a new method.

```csharp
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
```

For the CheckedListBox, subscribe to [ItemCheck](https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.checkedlistbox.itemcheck?view=windowsdesktop-7.0) event.

```csharp
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
```

And now the Button click event uses the same code as the CheckedListBox.

```csharp
private void ProcessButton_Click(object sender, EventArgs e)
{
    ProcessCurrentOrder();
}
```

## Step through code on adding orders

The purpose of this project is to setup one or more orders for the OrderProcessingApp to work with.

There are two buttons. 

The first to add a new order with the current date/time.

```csharp
public static async Task<Orders> AddNewOrder()
{
    await using var context = new Context();
    Orders order = new Orders()
    {
        OrderDate = new DateOnly(Now.Year, Now.Month, Now.Day),
        OrderTime = new TimeOnly(Now.Hour, Now.Minute, Now.Second),
        OrderIsNew = true
    };

    context.Add(order);
    await context.SaveChangesAsync();
    return order;
}
```

The second to reset all orders for the current day using EF Core [ExecuteUpdate method](https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.relationalqueryableextensions.executeupdate?view=efcore-7.0).

```csharp
public static void UpdateTodayOrders()
{
    var today = TodayDate();
    using var context = new Context();
    context.Orders.AsNoTracking()
        .Where(order => order.OrderDate!.Value == today)
        .ExecuteUpdate(s => s
            .SetProperty(order => order.OrderIsNew,
                order => true));
}
```

### Alternate

Consider working directly with data in SSMS by manually adding records and resetting records.

Add records by right clicking the table.

Reset records

```sql
UPDATE dbo.Orders
SET OrderIsNew = 1
WHERE OrderDate = '2023-10-22';
```

## Summary

In the article, code has been presented to get started with FluentScheduler library which has been kept relatively simple rather than complex for ease of learning.

Take time to first run the projects then go back and study the code to understand how the scheduler works and interacts with the user interface.

Since the time span for the scheduler is at ten seconds debugging will not work, instead depend on SeriLog for logging what needs to be inspected.

If Windows Forms is not preferred there is always console and ASP.NET Core options while there are other libraries to explore too if this library does not suit your needs.

## Source code

Clone the following GitHub repository and open the solution with Microsoft VS2022 or later.

