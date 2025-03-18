 using System.Text.Json;

namespace CompleteGraphQLDemo.GraphQL
{
    public record Book(string Title, Author Author , List<MetaData> MetaData);

  
 
    public record MetaData(string Key, [property: GraphQLType<JsonType>] string Value);


}

