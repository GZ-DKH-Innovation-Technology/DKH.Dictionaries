using System.Text.Json.Serialization;
using DKH.Dictionaries.Application;
using DKH.Dictionaries.Infrastructure;
using DKH.Dictionaries.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

#region CORS

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policyBuilder =>
    {
        policyBuilder.WithOrigins("*")
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

#endregion

builder.Services
    .AddControllers()
    .AddJsonOptions(options => { options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()); });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddSwaggerGen();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApplicationServices();
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

var app = builder.Build();
var defaultCulture = builder.Configuration.GetValue<string>("Localization:DefaultCulture");
var supportedCultures = builder.Configuration.GetSection("Localization:SupportedCultures").Get<string[]>();

if (defaultCulture != null && supportedCultures != null)
{
    app.UseRequestLocalization(options =>
    {
        options.SetDefaultCulture(defaultCulture);
        options.AddSupportedCultures(supportedCultures);
        options.AddSupportedUICultures(supportedCultures);
    });
}

using var scope = app.Services.CreateScope();
var initializer = scope.ServiceProvider.GetRequiredService<DictionaryDbContextInitializer>();

await initializer.Initialise();
await initializer.Seed();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();
app.Run();