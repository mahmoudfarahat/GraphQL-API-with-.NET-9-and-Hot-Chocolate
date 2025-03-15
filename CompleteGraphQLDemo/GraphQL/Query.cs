using CompleteGraphQLDemo.Data;
using CompleteGraphQLDemo.Models;
 

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
    }
}
