using CompleteGraphQLDemo.Data;
using CompleteGraphQLDemo.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System.Text.Json;


namespace CompleteGraphQLDemo.GraphQL
{
    public class Query
    {
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Platform> GetPlatform([Service] AppDbContext context)
        {
            return context.Platforms;
        }

        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Command> GetCommand([Service] AppDbContext context)
        {
            return context.Commands;
        }


        public async Task<Dictionary<string, object>> Get([Service] AppDbContext context)
        {

            var result = new Dictionary<string, object>
    {
        { "id", "2" },
        { "name","hhh"},
        { "department","iiii" }
        // Add other properties as needed
    };

            return result;
        }


        public MyType GetItem() =>
      new MyType
      { 
          Name = "Example",
          Data = JsonDocument.Parse("{ \"key\": \"value\" }") 
      };



        public MyType GetItemss()
        {
            // Example list
            var myList = new List<string> { "value1", "value2", "value3" };

            // Serialize the list to a JSON string
            string jsonString = JsonSerializer.Serialize(myList);

            // Parse the JSON string into a JsonDocument
            JsonDocument jsonDocument = JsonDocument.Parse(jsonString);

            return new MyType
            {
                Name = "Example",
                Data = jsonDocument // Use JsonElement directly
            };
        }





        public async Task<Dictionary<string, object>> GetEmployee(
    [Service] AppDbContext context,
    [GraphQLType(typeof(JsonType))] Dictionary<string, object> paramsDict)
        {
            var result = new Dictionary<string, object>
    {
        { "id", "2" },
        { "name", "hhh" },
        { "department", "iiii" }
    };

            foreach (var kvp in paramsDict)
            {
                result[kvp.Key] = kvp.Value;
            }

            return result;
        }
        //public async Task<JsonElement> GetEmployeeDetails([Service] AppDbContext context)
        //{
        //    var results = new List<Dictionary<string, object>>();

        //    using (var command = context.Database.GetDbConnection().CreateCommand())
        //    {
        //        command.CommandText = "dbo.GetEmployeeDetails";
        //        command.CommandType = System.Data.CommandType.StoredProcedure;

        //        if (command.Connection.State != System.Data.ConnectionState.Open)
        //            await command.Connection.OpenAsync();

        //        using (var reader = await command.ExecuteReaderAsync())
        //        {
        //            while (await reader.ReadAsync())
        //            {
        //                var row = new Dictionary<string, object>();
        //                for (int i = 0; i < reader.FieldCount; i++)
        //                {
        //                    row[reader.GetName(i)] = reader.GetValue(i);
        //                }
        //                results.Add(row);
        //            }
        //        }
        //    }

        //    // Serialize the list of dictionaries to a JSON string
        //    string jsonString = JsonSerializer.Serialize(results);

        //    // Parse the JSON string into a JsonDocument and return the RootElement
        //    using (JsonDocument jsonDocument = JsonDocument.Parse(jsonString))
        //    {
        //        return jsonDocument.RootElement.Clone();
        //    }
        //}
        public async Task<MyType> GetEmployeeDetails([Service] AppDbContext context, List<KeyValuePairInput> parameters)
        {
            var results = new List<Dictionary<string, object>>();

            using (var command = context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "dbo.GetEmployeeDetails";
                command.CommandType = System.Data.CommandType.StoredProcedure;
                foreach (var param in parameters)
                {
                    var dbParam = command.CreateParameter();
                    dbParam.ParameterName = param.Key;
                    dbParam.Value = param.Value; 
                    command.Parameters.Add(dbParam);
                }

                if (command.Connection.State != System.Data.ConnectionState.Open)
                    await command.Connection.OpenAsync();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var row = new Dictionary<string, object>();
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            row[reader.GetName(i)] = reader.GetValue(i);
                        }
                        results.Add(row);
                    }
                }
            }

            // Serialize the list of dictionaries to a JSON string
            string jsonString = JsonSerializer.Serialize(results);

            // Parse the JSON string into a JsonDocument
            JsonDocument jsonDocument = JsonDocument.Parse(jsonString);
            return new MyType
            {
                Name = "Example",
                Data = jsonDocument // Use JsonElement directly
            };
        }
        public   List<MetaData> GetMetaData() => new List<MetaData>
        {
            new MetaData("something","1"),
                        new MetaData("something", """{"a":"B"}""" )

        };


    }
    public class KeyValuePairInput
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
