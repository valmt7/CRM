using CRM;
using CRM.MidMiddleware;
using CRM.Services;
using Microsoft.EntityFrameworkCore;
using static CRM.MidMiddleware.GlobalExceptionHandler;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IFleetService, FleetService>();
builder.Services.AddScoped<IRouteService, RouteService>();
builder.Services.AddScoped<IMailService, MailService>();
builder.Services.AddScoped<IDriversService, DriversService>();
builder.Services.AddScoped<IManagerService, ManagerService>();
builder.Services.AddScoped<IReportService, ReportService>();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();
DotNetEnv.Env.Load();
builder.Configuration.AddEnvironmentVariables();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.UseExceptionHandler(_ => { });
app.Run();