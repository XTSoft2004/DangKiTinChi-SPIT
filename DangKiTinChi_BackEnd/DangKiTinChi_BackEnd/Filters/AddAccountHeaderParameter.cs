using DangKiTinChi_BackEnd.Attribute;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace DangKiTinChi_BackEnd.Filters
{
    public class AddAccountHeaderParameter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            // Kiểm tra xem method có gắn Attribute không
            var hasAttribute = context.MethodInfo
                .GetCustomAttributes(true)
                .OfType<RequireAccountIdHeaderAttribute>()
                .Any();

            if (!hasAttribute)
                return; // Không có thì không thêm header

            // Có Attribute thì thêm header
            operation.Parameters ??= new List<OpenApiParameter>();

            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "X-Account-Id",
                In = ParameterLocation.Header,
                Required = true,
                Schema = new OpenApiSchema { Type = "string" },
                Description = "ID của Account đang sử dụng"
            });
        }
    }
}
