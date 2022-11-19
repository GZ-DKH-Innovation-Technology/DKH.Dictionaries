using DKH.Dictionaries.Api.Data.Initialization;
using DKH.Dictionaries.Application;
using DKH.Dictionaries.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

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

#region DbContext

builder.Services.AddDbContext<DictionaryDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DictionaryDbContext"),
        npgsqlOptions => { npgsqlOptions.EnableRetryOnFailure(5, TimeSpan.FromSeconds(30), null); })
);

#endregion

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddSwaggerGen();
builder.Services.AddApplication();
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
    await app.Services.DictionaryDbInitialize(builder.Environment, supportedCultures);
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();
app.Run();