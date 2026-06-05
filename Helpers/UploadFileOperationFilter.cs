using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace LibraryApi1.Helpers
{
    public class UploadFileOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            // نتحقق إذا كان هناك باراميتر IFormFile
            var hasFileUpload = context.MethodInfo
                .GetParameters()
                .Any(p => p.ParameterType == typeof(IFormFile) || 
                          p.ParameterType == typeof(IFormFileCollection));

            if (!hasFileUpload)
                return;

            // تعديل الاستهلاك في Swagger
            operation.RequestBody = new OpenApiRequestBody
            {
                Content =
                {
                    ["multipart/form-data"] = new OpenApiMediaType
                    {
                        Schema = new OpenApiSchema
                        {
                            Type = "object",
                            Properties =
                            {
                                ["file"] = new OpenApiSchema
                                {
                                    Type = "string",
                                    Format = "binary"
                                }
                            },
                            Required = new HashSet<string> { "file" }
                        }
                    }
                }
            };
        }
    }
}
