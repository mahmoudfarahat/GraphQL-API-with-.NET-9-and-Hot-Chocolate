using CompleteGraphQLDemo.Data;
using CompleteGraphQLDemo.Models;
 

namespace CompleteGraphQLDemo.GraphQL
{
    public class Query
    {
        [UseProjection]
        public IQueryable<Platform> GetPlatform([Service] AppDbContext context)
        {
            return context.Platforms;
        }

        [UseProjection]
        public IQueryable<Command> GetCommand([Service] AppDbContext context)
        {
            return context.Commands;
        }
    }
}
