using Manatee.Json;
using Manatee.Json.Schema;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace JsonSchemaIssue
{
    class Program
    {
        static void Main(string[] args)
        {
            var schemaPath = Path.Combine(
              Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
              "schema-width-examples.json"
            );

            var schemaJson = JsonValue.Parse(File.ReadAllText(schemaPath));
            var schema = JsonSchemaFactory.FromJson(schemaJson);

            var targetJson = JsonValue.Parse(" { \"test\": \"a valid string\" } ");
            var validationResults = schema.Validate(targetJson);  // Should pass.

            Console.WriteLine("Schema with property that has examples key");
            Console.WriteLine($"validationResults.Valid: {validationResults.Valid}");
            Console.WriteLine(validationResults.Errors.ToList()[0].ToString());
            // => validationResults.Valid: False
            //    Property: test - Expected: Array; Actual: String

            schemaPath = Path.Combine(
            Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                "schema-without-examples.json"
            );

            schemaJson = JsonValue.Parse(File.ReadAllText(schemaPath));
            schema = JsonSchemaFactory.FromJson(schemaJson);

            validationResults = schema.Validate(targetJson);  // Should pass.

            Console.WriteLine("\nSchema without examples key in property");
            Console.WriteLine($"validationResults.Valid: {validationResults.Valid}");
            // => validationResults.Valid: True

            Console.ReadLine();
        }
    }
}
