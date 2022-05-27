using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.Sap.Authentication
{
    public class ManagerHeaderFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null) operation.Parameters = new List<OpenApiParameter>();

            var descriptor = context.ApiDescription.ActionDescriptor as ControllerActionDescriptor;

            if (descriptor != null && !descriptor.ControllerName.StartsWith("Weather"))
            {
                operation.Parameters.Add(new OpenApiParameter()
                {
                    Name = "Channel",
                    In = ParameterLocation.Header,
                    Description = "Nombre de la aplicación que consume el microservicio.",
                    Required = true
                });

                operation.Parameters.Add(new OpenApiParameter()
                {
                    Name = "PublicToken",
                    In = ParameterLocation.Header,
                    Description = "PublicToken generado por el Api de Autorización de Arquitectura",
                    Required = true
                });

                operation.Parameters.Add(new OpenApiParameter()
                {
                    Name = "AppUserId",
                    In = ParameterLocation.Header,
                    Description = "AppUserId generado por el Api de Autorización de Arquitectura",
                    Required = true
                });
            }
        }
    }
}