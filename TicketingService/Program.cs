using TicketingService.API;
using TicketingService.DataSource.Ticketing;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString(AppSettingKey.TicketingDb);

if (string.IsNullOrEmpty(connectionString))
{
    throw new ArgumentNullException(nameof(connectionString), "The connection string for the TicketingDb is not configured.");
}

builder.Services.ConfigureDataSourceServices(connectionString);
builder.Services.AddMediatR(cf => cf.RegisterServicesFromAssembly(AppDomain.CurrentDomain.Load("TicketingService.Domain")));

builder.Logging.ClearProviders();

var app = builder.Build();

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();