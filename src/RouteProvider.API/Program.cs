using AutoMapper;
using RouteProvider.API;
using RouteProvider.API.Mapping;
using RouteProvider.API.Providers;
using RouteProvider.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddHttpClient();
builder.Services.Configure<ProviderSettingsConfiguration>(builder.Configuration.GetSection(nameof(ProviderSettingsConfiguration)));
builder.Services.AddSingleton(_ => new MapperConfiguration(cfg => cfg.AddProfile(new FiltersMappingProfile())).CreateMapper());

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IExternalProviderOne, ExternalProviderOne>();
builder.Services.AddSingleton<IExternalProviderTwo, ExternalProviderTwo>();
builder.Services.AddSingleton<ISearchService, SearchService>();

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