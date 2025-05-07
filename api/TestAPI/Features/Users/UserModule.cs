using MediatR;
using Microsoft.AspNetCore.Mvc;
using TestAPI.Features.Users.Commands;
using TestAPI.Features.Users.Queries;
using TestAPI.Models;

namespace TestAPI.Features.Users;

public static class UserModule
{
    public static void MapUserRoutes(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("Users").WithTags("Users");

        group.MapGet("/GetUsers",
                async (IMediator mediator) => await mediator.Send(new GetUsers.Query()))
            .WithName(nameof(GetUsers))
            .Produces<List<User>>();

        group.MapPost("/AddUser",
                async ([FromBody] User user, IMediator mediator) => await mediator.Send(new AddUser.Command(user)))
            .WithName(nameof(AddUser))
            .Produces<int>();
    }
}