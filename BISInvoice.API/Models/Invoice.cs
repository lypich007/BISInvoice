namespace BISInvoice.API.Models;

public class Invoice
{
    public Guid Id { get; set; }
    public DateTime TxnDate { get; set; }
    public string RefNumber { get; set; }
    public Guid CustomerId { get; set; }
    public string BillAddress { get; set; }
    public string Phone {  get; set; }
    public List<InvoiceLine> InvoiceLines { get; set; }
}
