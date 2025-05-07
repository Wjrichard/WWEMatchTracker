using Microsoft.AspNetCore.Http.HttpResults;
using TestAPI.Models;
using TestAPI.Repositories;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Routes;

public static class UserRoutes2
{
    public static void MapUserRoutes(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("Users").WithTags("Users");

        group.MapGet("/GetUsers",
            async Task<Ok<List<User>>> (UserRepository userRepository) =>
                TypedResults.Ok(await userRepository.GetUsers()))
            .WithName(nameof(User))
            .Produces<List<User>>();

        group.MapPost("/AddUser/",
            async ([FromBody] User user, UserRepository userRepository, IMediator mediator) =>
                    await mediator.Send(userRepository.AddUser(user)))
            .WithName("Add User")
            .Produces<int>();
    }
}