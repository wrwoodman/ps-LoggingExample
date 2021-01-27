using System.Collections.Generic;
using Microsoft.OpenApi.Models;
using PostSharp.Patterns.Diagnostics;
using Swashbuckle.AspNetCore.SwaggerGen;
#pragma warning disable 1591  // Disable XML comment warning

namespace Argo.MyProject.Swagger
{
    /// <summary>
    /// TODO: Customize as needed for the project.  It is recommended that the UniqueId be kept as it is used for log tracing across systems.
    /// Add other fields that will be needed for all calls.
    /// </summary>
    [Log(AttributeExclude = true)]
    public class SwaggerAddHeaderParameters : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            operation.Parameters ??= new List<OpenApiParameter>();

            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "uniqueId",
                In = ParameterLocation.Header,
                Description = "Unique Id for this request.  Used to correlate logs across different tiers.  It is recommended that a GUID be used for the value.",
                Required = true,
                Schema = new OpenApiSchema
                {
                    Type = "string"
                }
            });
        }

    }
}
