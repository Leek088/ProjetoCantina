using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ProjetoCantina.API.Services.Swagger
{
    public class ConfigureSwaggerOptions : IConfigureNamedOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider _provider;

        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider)
        {
            _provider = provider;
        }

        
        public void Configure(SwaggerGenOptions options)
        {
            foreach (var description in _provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(
                    description.GroupName,
                    CreateVersionInfo(description));

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. " +
                    "\r\n\r\n Enter 'Bearer' [space] and then your token in the text input below." +
                    "\r\n\r\n Example: \"Bearer 1safsfsdfdfd\"",
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement {
                {
                    new OpenApiSecurityScheme {
                        Reference = new OpenApiReference {
                            Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
            }
        }
      
        public void Configure(string? name, SwaggerGenOptions options)
        {
            Configure(options);
        }
       
        private OpenApiInfo CreateVersionInfo(
                ApiVersionDescription desc)
        {
            var info = new OpenApiInfo()
            {
                Title = "Projeto de teste para Cantina.",
                Version = desc.ApiVersion.ToString(),
                Description = ".NET Core (.NET 8) Web API",
                Contact = new OpenApiContact
                {
                    Name = "Leonardo Nunes",
                    Email = "leonardoleeko88@gmail.com",
                },               
            };

            if (desc.IsDeprecated)
            {
                info.Description += " Esta versão da API está descontinuada. Por favor, use uma das novas APIs definidas nas opções.";
            }

            return info;
        }

    }
}
