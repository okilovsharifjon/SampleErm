using Microsoft.OpenApi.Models;
using Microsoft.OpenApi.Any;

using Swashbuckle.AspNetCore.SwaggerGen;

namespace Erm.PresentationLayer.WebApi
{
    public class AddRequiredHeaderParametr : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation == null)
                operation.Parameters = new List<OpenApiParameter>();

            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "Key",
                Schema = new OpenApiSchema
                {
                    Type = "string",
                    Default = new OpenApiString("123")
                },
                In = ParameterLocation.Header,
                Required = false
            });
            
        }
    }
}
