using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Newsy.Api.Infrastructure;

public class ExampleSchemaFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        var obj = new OpenApiObject()
        {
        };

        var properties = context.Type.GetProperties();

        if (properties.Any(x => x.Name == "NewsRoomId"))
        {
            obj.Add("newsRoomId", new OpenApiString("8a4cd724-df4d-4d7c-ae4f-0fcef538a05e"));
        }

        if (properties.Any(x => x.Name == "FolderId"))
        {
            obj.Add("folderId", new OpenApiString("D6012FA7-C9EC-490C-BC0F-48E2295D2450"));
        }

        if (properties.Any(x => x.Name == "SourceId"))
        {
            obj.Add("sourceId", new OpenApiString("01605e68-6670-499d-a01b-b963067a9e36"));
        }

        schema.Example = obj;
    }
}

