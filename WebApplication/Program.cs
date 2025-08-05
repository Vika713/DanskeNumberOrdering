using BusinessServices;
using Microsoft.Extensions.Options;
using WebApplication.Configuration;

var builder = Microsoft.AspNetCore.Builder.WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddOpenApi();

builder.Services.Configure<FileServiceOptions>(builder.Configuration.GetSection("FileService"));
builder.Services.AddScoped<INumberStorageService>(sp =>
{
    var options = sp.GetRequiredService<IOptions<FileServiceOptions>>().Value;
    return new FileService(options.FilePath);
});
builder.Services.AddScoped<ISortingService, SortingService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
