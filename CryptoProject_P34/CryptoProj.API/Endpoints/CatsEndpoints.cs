using CryptoProj.Domain.Models;
using CryptoProj.Domain.Models.Requests;
using CryptoProj.Domain.Services.Cats;
using Microsoft.AspNetCore.Mvc;

namespace CryptoProj.API.Endpoints
{
    public static class CatsEndpoints
    {
        public static IEndpointRouteBuilder MapCatsEndpoints(this IEndpointRouteBuilder app)
        {
            var endpoint = app.MapGroup("api/v1/cats");

            endpoint.MapGet("/", GetCats);
            endpoint.MapGet("{id}", GetCatsById).WithName("GetCatById");
            endpoint.MapPost("/", AddCat);
            endpoint.MapPut("{id}", UpdateCat);
            endpoint.MapDelete("{id}", DeleteCat);

            return app;
        }

        public static async Task<IResult> GetCats([FromQuery] CatsRequest request, CatsService catsService)
        {
            return Results.Ok(await catsService.GetAll(request));
        }

        public static async Task<IResult> GetCatsById([FromRoute] int id, CatsService catsService)
        {
            return Results.Ok(await catsService.Get(id));
        }

        public static async Task<IResult> AddCat([FromBody] Cat cat, CatsService catsService)
        {
            var createdCat = await catsService.Add(cat);

            return Results.CreatedAtRoute("GetCatById", new { id = createdCat.Id }, createdCat);
        }

        public static async Task<IResult> UpdateCat([FromRoute] int id, [FromBody] Cat cat, CatsService catsService)
        {
            return Results.Ok(await catsService.Update(id, cat));
        }

        public static async Task<IResult> DeleteCat([FromRoute] int id, CatsService catsService)
        {
            await catsService.Delete(id);
            return Results.NoContent();
        }
    }
}