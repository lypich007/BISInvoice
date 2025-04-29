var builder = WebApplication.CreateBuilder(args);

// Register the AppDbContext to use in-memory database
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("InvoiceDB"));

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Seed initial test data
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    // Check if data already exists
    if (!dbContext.Customers.Any())
    {
        // Adding a customer record
        dbContext.Customers.Add(new Customer
        {
            Name = "ABA Bank",
            CompanyName = "ABA Bank Plc",
            Address = "Phnom Penh",
            Phone = "012345678"
        });
        dbContext.Customers.Add(new Customer
        {
            Name = "Acleda Bank",
            CompanyName = "Acleda Bank Plc",
            Address = "Phnom Penh, Cambodia",
            Phone = "016335678"
        });
    }

    if (!dbContext.Items.Any())
    {
        dbContext.Items.Add(new Item
        {
            ItemName = "CAM 05A/80A",
            ItemDescription = "CAM-TONER 05A/80A/Works with HP P2035N/P2055dn/Pro400-M401dn (2.7K)",
            Cost = 12.0M,
            SalesPrice = 16.0M
        });
        dbContext.Items.Add(new Item
        {
            ItemName = "CAM 16A",
            ItemDescription = "CAM-TONER 16A/Works with HP 5200 (12K)",
            Cost = 40.0M,
            SalesPrice = 45.0M
        });
        dbContext.Items.Add(new Item
        {
            ItemName = "HP 05A",
            ItemDescription = "Original HP 05A Black LaserJet Toner Cartridge, CE505A",
            Cost = 90.0M,
            SalesPrice = 95.0M
        });
    }
    dbContext.SaveChanges();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Items
app.MapGet("/item", async (AppDbContext dbContext) =>
    await dbContext.Items.ToListAsync()
)
.WithName("Item")
.WithOpenApi();

// Customer
app.MapGet("/customer", async (AppDbContext dbContext) =>
    await dbContext.Customers.ToListAsync()
)
.WithName("Customer")
.WithOpenApi();

// Invoice
app.MapGet("/invoice", async (AppDbContext dbContext) =>
    await dbContext.Invoices.Include(i => i.InvoiceLines).ToListAsync()
)
.WithOpenApi();
// Invoice by Id
app.MapGet("/invoice/{id:guid}", async (AppDbContext dbContext, Guid id) =>
{
    var invoice = await dbContext.Invoices.Include(i => i.InvoiceLines)
    .FirstOrDefaultAsync(i => i.Id == id);
    return invoice is not null ? Results.Ok(invoice) : Results.NotFound();
}).WithOpenApi();
app.MapPost("/invoice", async (AppDbContext dbContext, Invoice inv) =>
{
    //var invoice = new Invoice
    //{
    //    CustomerId = Guid.NewGuid(),
    //    InvoiceDate = DateTime.UtcNow,
    //    DueDate = DateTime.UtcNow.AddDays(30),
    //    TotalAmount = 0
    //};
    dbContext.Invoices.Add(inv);
    await dbContext.SaveChangesAsync();
    return Results.Created($"/invoice/{inv.Id}", inv);
})
.WithName("Invoice")
.WithOpenApi();

app.Run();