
 using System.Text.Json;

namespace CompleteGraphQLDemo.Models
{
    public class MyType
    {
        public string Name { get; set; }
        public JsonDocument Data { get; set; } // JSON data
    }
    public class MyTypeType : ObjectType<MyType>
    {
        protected override void Configure(IObjectTypeDescriptor<MyType> descriptor)
        {
            descriptor.Field(t => t.Name);
            descriptor.Field(t => t.Data)
                .Type<JsonType>()
                .Resolve(context => {
                    var parent = context.Parent<MyType>();
                    return JsonSerializer.Deserialize<object>(parent.Data);
                });
        }
    }
}
