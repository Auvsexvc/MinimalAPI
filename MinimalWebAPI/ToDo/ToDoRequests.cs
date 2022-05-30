using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;

namespace MinimalWebAPI.ToDo
{
    public static class ToDoRequests
    {
        public static WebApplication RegisterEndpoints(this WebApplication app)
        {
            app.MapGet("/todos", ToDoRequests.GetAll)
                .Produces<List<ToDo>>()
                .WithTags("To Dos");

            app.MapGet("/todos/{id}", ToDoRequests.GetById)
                .Produces<ToDo>()
                .Produces(StatusCodes.Status404NotFound)
                .WithTags("To Dos");

            app.MapPost("/todos", ToDoRequests.Create)
                .Produces<ToDo>(StatusCodes.Status201Created)
                .Accepts<ToDo>("application/json")
                .WithTags("To Dos")
                .WithValidator<ToDo>();

            app.MapPut("/todos/{id}", ToDoRequests.Update)
                .WithValidator<ToDo>()
                .Produces<ToDo>(StatusCodes.Status204NoContent)
                .Produces(StatusCodes.Status404NotFound)
                .Accepts<ToDo>("application/json")
                .WithTags("To Dos")
                .RequireAuthorization();

            app.MapDelete("/todos/{id}", ToDoRequests.Delete)
                .Produces<ToDo>(StatusCodes.Status204NoContent)
                .Produces(StatusCodes.Status404NotFound)
                .WithTags("To Dos")
                .ExcludeFromDescription();

            return app;
        }
        
        public static IResult GetAll(IToDoService service)
        {
            var todos = service.GetAll();
            return Results.Ok(todos);
        }
        public static IResult GetById(IToDoService service, int id)
        {
            var todo = service.GetById(id);
            if(todo == null)
            {
                return Results.NotFound();
            }
            return Results.Ok(todo);
        }

        [Authorize]
        public static IResult Create(IToDoService service, ToDo toDo)
        {
            service.Create(toDo);

            return Results.Created($"/todos/{toDo.Id}", toDo);
        }

        public static IResult Update(IToDoService service, int id, ToDo toDo)
{
            service.Create(toDo);

            var todo = service.GetById(id);
            if (todo == null)
            {
                return Results.NotFound();
            }

            service.Update(todo);

            return Results.NoContent();
        }

        public static IResult Delete(IToDoService service, int id)
        {
            var todo = service.GetById(id);
            if (todo == null)
            {
                return Results.NotFound();
            }

            service.Delete(id);

            return Results.NoContent();
        }
    }
}
