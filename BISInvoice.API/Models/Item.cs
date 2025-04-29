namespace BISInvoice.API.Models;

public class Item
{
    public Guid Id { get; set; }
    public string ItemName { get; set; }
    public string ItemDescription { get; set; }
    public decimal Cost { get; set; }
    public decimal SalesPrice { get; set; }
}
