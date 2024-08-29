using PowerPlantChallenge.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IMeritOrderService, MeritOrderService>();
builder.Services.AddSingleton<ISwitchPowerplantService, SwitchPowerplantService>();
builder.Services.AddSingleton<IPowerplantCostService, PowerplantCostService>();
builder.Services.AddSingleton<IPowerplantResponseService, PowerplantResponseService>();

builder.WebHost.ConfigureKestrel(options => options.ListenAnyIP(8888)); //For docker

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
