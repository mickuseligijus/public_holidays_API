using Holidays_WebAPI.Context;
using Holidays_WebAPI.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApiDocument(c =>
{
    c.DocumentName = "v1";
    c.PostProcess = doc =>
    {
        doc.Info.Version = "v1";
        doc.Info.Title = "Holiday OpenAPI";
    };
});
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSingleton<IHolidayService, HolidayService>();

builder.Services.AddDbContextPool<HolidayDbContext>(options =>
{
    var connetionString = builder.Configuration.GetConnectionString("myconn");
    options.UseMySql(connetionString, ServerVersion.AutoDetect(connetionString));
});

var app = builder.Build();

app.UseOpenApi();

if (app.Environment.IsDevelopment())
{
    app.UseSwaggerUi3();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
