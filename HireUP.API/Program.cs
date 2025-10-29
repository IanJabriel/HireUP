using HireUP.Application.Services;
using HireUP.Domain.Interfaces;
using HireUP.Infra.Repositories;
using HireUP.Infra;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>();

// Repository Registration
builder.Services.AddScoped<IEmployerRepository, EmployerRepository>();
builder.Services.AddScoped<IEnterpriseRepository, EnterpriseRepository>();
builder.Services.AddScoped<IProblemRepository, ProblemRepository>();
builder.Services.AddScoped<ISolutionRepository, SolutionRepository>();
builder.Services.AddScoped<IHackatonRepository, HackatonRepository>();
builder.Services.AddScoped<IEventRepository, EventRepository>();

// Application Service Registration
builder.Services.AddScoped<EmployerApplicationService>();
builder.Services.AddScoped<EnterpriseApplicationService>();
builder.Services.AddScoped<ProblemApplicationService>();
builder.Services.AddScoped<SolutionApplicationService>();
builder.Services.AddScoped<HackatonApplicationService>();
builder.Services.AddScoped<EventApplicationService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
