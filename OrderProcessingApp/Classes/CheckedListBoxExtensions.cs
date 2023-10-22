namespace OrderProcessingApp.Classes;
public static class CheckedListBoxExtensions
{
    /// <summary>
    /// Get checked items from a strong type DataSource
    /// </summary>
    /// <typeparam name="T">Type of item</typeparam>
    /// <param name="sender">CheckedListBox</param>
    /// <returns></returns>
    public static List<T> CheckedList<T>(this CheckedListBox sender)
        => sender.Items.Cast<T>()
            .Where((_, index) => sender.GetItemChecked(index))
            .Select(item => item)
            .ToList();

    /// <summary>
    /// Uncheck all items
    /// </summary>
    /// <param name="sender">CheckedListBox</param>
    public static void UnCheckAll(this CheckedListBox sender)
    {
        foreach (int index in sender.CheckedIndices)
        {
            sender.SetItemCheckState(index, CheckState.Unchecked);
        }
    }
}