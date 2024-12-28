using BubberBreakFastAPI.ApiKey;
using BubberBreakFastAPI.Data;
using BubberBreakFastAPI.Interfaces;
using BubberBreakFastAPI.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<ApplicationDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("BubberBreakFastDB"));
});
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IBreakFastRepository, BreakFastRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}
app.UseMiddleware<ApiKeyMiddleWare>();
app.UseHttpsRedirection();
app.MapControllers();
app.Run();

