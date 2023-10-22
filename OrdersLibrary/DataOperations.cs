using Microsoft.Win32;
using OrdersLibrary.Data;
using OrdersLibrary.Models;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using static System.DateTime;


namespace OrdersLibrary;
public class DataOperations
{
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

    public static List<Orders> GetNewOrders()
    {
        using var context = new Context();
        var today = TodayDate();
        return context
            .Orders
            .Where(x => x.OrderDate == today && x.OrderIsNew == true)
            .ToList();
    }

    private static DateOnly TodayDate()
    {
        DateOnly today = new DateOnly(Now.Year, Now.Month, Now.Day);
        return today;
    }

    public static void ProcessOrders(List<Orders> list)
    {
        using var context = new Context();

        foreach (var order in list)
        {
            order.OrderIsNew = false;
            context.Update(order);
        }

        context.SaveChanges();
    }

    /// <summary>
    /// Update orders with today's date for OrderDate.
    /// </summary>
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
    public static async Task UpdateTodayOrdersAsync()
    {
        var today = TodayDate();
        await using var context = new Context();
        await context.Orders.AsNoTracking()
            .Where(order => order.OrderDate!.Value == today)
            .ExecuteUpdateAsync(s => s
                .SetProperty(order => order.OrderIsNew,
                    order => true));
    }
}

