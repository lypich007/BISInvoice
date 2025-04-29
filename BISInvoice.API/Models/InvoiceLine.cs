namespace BISInvoice.API.Models;

public class InvoiceLine
{
    public Guid Id { get; set; }
    public Guid ItemId { get; set; }
    public string ItemDescription { get; set; }
    public int Quantity { get; set; }
    public decimal SalesPrice { get; set; }
}
