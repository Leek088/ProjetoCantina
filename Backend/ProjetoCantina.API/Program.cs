using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using ProjetoCantina.API.Contexts;
using ProjetoCantina.API.Services.Interfaces;
using ProjetoCantina.API.Services.Service;
using ProjetoCantina.API.Services.Swagger;
using ProjetoCantina.API.UnitOfWork;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

//Configuração para ignorar as chamadas em loop
builder.Services.AddControllers().AddJsonOptions(options =>
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles
);

//Configuração para conexão ao banco de dados
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString,
ServerVersion.AutoDetect(connectionString)));

//Configuração para o mapeamento entre entidades e dtos
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//Configuração da injeção de dependencia para serviços
builder.Services.AddScoped<ICategoriaService, CategoriaService>();
builder.Services.AddScoped<IProdutoService, ProdutoService>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<ICaixaService, CaixaService>();
builder.Services.AddScoped<IFluxoCaixaService, FluxoCaixaService>();
builder.Services.AddScoped<IVendaService, VendaService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

//Configuração do versionamento da API
builder.Services.AddApiVersioning(opt =>
    {
        opt.DefaultApiVersion = new ApiVersion(1, 0);
        opt.AssumeDefaultVersionWhenUnspecified = true;
        opt.ReportApiVersions = true;
        opt.ApiVersionReader = ApiVersionReader
            .Combine(new UrlSegmentApiVersionReader(),
                new HeaderApiVersionReader("x-api-version"),
                new MediaTypeApiVersionReader("x-api-version"));
    })
    .AddApiExplorer(opt =>
    {
        opt.GroupNameFormat = "'v'VVV";
        opt.FormatGroupName = (group, version) => $"{group} - {version}";
        opt.SubstituteApiVersionInUrl = true;
    }
);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Adiciona a classe que gerencia as versões dentro do Swagger
builder.Services.ConfigureOptions<ConfigureSwaggerOptions>();

var app = builder.Build();

var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions.Reverse())
        {
            options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                description.GroupName.ToUpperInvariant());
        }
    });

}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
