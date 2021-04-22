using Namotion.Reflection;
using NJsonSchema;
using NJsonSchema.Generation;
using NSwag;
using NSwag.Generation;
using NSwag.Generation.WebApi;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace OpenApiSpecGenerator
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Assembly assembly = Assembly.LoadFrom(args[0]);

            var info = assembly.ExportedTypes
                .Where(t => t.GetTypeInfo().IsAbstract == false)
                .Where(t => t.Name.EndsWith("OpenApiInfo"))
                .Single();

            FieldInfo[] fileInfo = info.GetFields();
            var version = (string)fileInfo.Where(f => f.Name == "Version").Single().GetValue(null);
            var description = (string)fileInfo.Where(f => f.Name == "Description").Single().GetValue(null);
            var title = (string)fileInfo.Where(f => f.Name == "Title").Single().GetValue(null);
            var contact = (OpenApiContact)fileInfo.Where(f => f.Name == "Contact").Single().GetValue(null);

            var settings = new WebApiOpenApiDocumentGeneratorSettings
            {
                DefaultUrlTemplate = "api/{controller}/{action}/{id}",
                SchemaType = SchemaType.OpenApi3,
                Description = description,
                Version = version,
                Title = title
            };

            var generator = new WebApiOpenApiDocumentGenerator(settings);
            var document = await generator.GenerateForControllersAsync(WebApiOpenApiDocumentGenerator.GetControllerClasses(assembly));
            document.Info.Contact = contact;
            
            var swaggerSpecification = document.ToJson();
            Console.WriteLine(swaggerSpecification);
            await File.WriteAllTextAsync("openapispec.json", swaggerSpecification);
        }
    }
}
