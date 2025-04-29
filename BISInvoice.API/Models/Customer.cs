namespace BISInvoice.API.Models;

public class Customer
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string CompanyName { get; set; }
    public string Address  { get; set; }
    public string Phone { get; set; }
}
