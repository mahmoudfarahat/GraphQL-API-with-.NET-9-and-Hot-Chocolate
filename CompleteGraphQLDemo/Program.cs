using CompleteGraphQLDemo.Data;
using CompleteGraphQLDemo.GraphQL;
using Microsoft.EntityFrameworkCore;
using GraphQL.Server.Ui.Voyager;
using CompleteGraphQLDemo.GraphQL.Platforms;
using System.Data;

namespace CompleteGraphQLDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //AddPooledDbContextFactory 
            builder.Services.AddDbContext<AppDbContext>(options =>
           options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services
                .AddGraphQLServer()
                .AddQueryType<Query>()
                .AddMutationType<Mutation>()
                .AddSubscriptionType<Subscription>()
                .AddType<PlatformType>()
                .AddType<CommandType>()
                .AddProjections()
                .AddFiltering()
                .AddSorting()
                .AddInMemorySubscriptions();

            var app = builder.Build();

            app.UseWebSockets();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGraphQL();
            });
            //app.UseGraphQLVoyager(new GraphQLVoyagerOptions());
            app.Run();
        }
    }
}
