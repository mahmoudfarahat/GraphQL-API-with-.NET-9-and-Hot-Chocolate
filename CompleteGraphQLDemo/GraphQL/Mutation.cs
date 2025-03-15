using CompleteGraphQLDemo.Data;
using CompleteGraphQLDemo.GraphQL.Commands;
using CompleteGraphQLDemo.GraphQL.Platforms;
using CompleteGraphQLDemo.Models;
using HotChocolate.Execution.Processing;
using HotChocolate.Subscriptions;

namespace CompleteGraphQLDemo.GraphQL
{
    [GraphQLDescription("Represents the mutations available.")]
    public class Mutation
    {
  
         [GraphQLDescription("Adds a platform.")]
        public async Task<AddPlatformPayload> AddPlatformAsync(AddPlatformInput input,[Service] AppDbContext context, [Service] ITopicEventSender eventSender,
             CancellationToken cancellationToken)
        {
            var platform = new Platform
            {
                Name = input.Name
                
            };

            context.Platforms.Add(platform);
            await context.SaveChangesAsync(cancellationToken);

             await eventSender.SendAsync(nameof(Subscription.OnPlatformAdded), platform, cancellationToken);

            return new AddPlatformPayload(platform);
        }
        [GraphQLDescription("Adds a command.")]
        public async Task<AddCommandPayload> AddCommandAsync(AddCommandInput input,
         [Service] AppDbContext context, [Service] ITopicEventSender eventSender,
             CancellationToken cancellationToken)
        {
            var command = new Command
            {
                HowTo = input.HowTo,
                CommandLine = input.CommandLine,
                PlatformId = input.PlatformId
            };

            context.Commands.Add(command);
            await context.SaveChangesAsync(cancellationToken);

            return new AddCommandPayload(command);
        }
    }
}
