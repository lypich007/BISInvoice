using BISInvoice.API.Models;

namespace BISInvoice.API;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Invoice> Invoices { get; set; } = default!;
    public DbSet<Customer> Customers { get; set; } = default!;
    public DbSet<Item> Items { get; set; } = default!;
    public DbSet<InvoiceLine> InvoiceLines { get; set; } = default!;
}
