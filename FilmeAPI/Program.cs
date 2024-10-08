using Data;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Repositories;
using Mapping;
using Data.Settings;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<MongoDbSettings>(
    builder.Configuration.GetSection("MongoDbSettings"));

builder.Services.AddSingleton(sp =>
    sp.GetRequiredService<IOptions<MongoDbSettings>>().Value);

builder.Services.AddSingleton<MongoDbContext>();

builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));

builder.Services.AddScoped<IFilmeRepository, FilmeRepository>();

builder.Services.AddControllers();

builder.Services.AddAutoMapper(typeof(FilmeMappingProfile)); 

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Filme API",
        Version = "v1",
        Description = "A Filme API permite gerenciar um catálogo de filmes" +
                      "Acesse o código fonte: [Repositório](https://github.com/caiorfer/CP5-crud)",
        License = new OpenApiLicense
        {
            Name = "MIT License",
            Url = new Uri("https://github.com/caiorfer/CP5-crud")
        }
    });

    options.EnableAnnotations();
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Filme API v1");
        c.RoutePrefix = "";
    });
}

app.UseAuthorization();

app.MapControllers();

app.Run();
