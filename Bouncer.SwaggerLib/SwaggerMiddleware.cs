using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using NSwag;
using NSwag.Generation.Processors.Security;
using System;

namespace SwaggerLib
{
    public static class SwaggerMiddleware
    {
        public static IApplicationBuilder SetSwagger(this IApplicationBuilder app)
        {
            app.UseOpenApi();
            app.UseSwaggerUi3();
            return app;
        }

        public static IServiceCollection SetSwagger(this IServiceCollection service, string title)
        {
            service
                .AddSwaggerDocument(document =>
                {
                    document.Title = title;
                    document.DocumentName = $"{title} authentication";
                    document.AddSecurity("Bearer", new OpenApiSecurityScheme()
                    {
                        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                        Name = "Authorization",
                        In = OpenApiSecurityApiKeyLocation.Header,
                        Type = OpenApiSecuritySchemeType.ApiKey
                    });
                    document.OperationProcessors.Add(
                    new AspNetCoreOperationSecurityScopeProcessor("Bearer"));

                    document.PostProcess = d => d.Info.Title = title;
                })
                .AddOpenApiDocument(document => document.DocumentName = "openapi"); 
                
            return service;
        }
    }
}
