using AspWebApi.DTO;
using AspWebApi.Models;
using AspWebApi.Services;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

namespace AspWebApi.Endpoint
{
    public static class PersonneEndpoint
    {
        public static RouteGroupBuilder MapPersonEndpoint(this RouteGroupBuilder group)
        {
            //on dois remplacer app par group 
            //et enlever la route person 

            #region Utilisation ILogger 

            // on ajoute un Service ILogger de Type Program avec la variable logger   pour accéeder au méthode de ce service 
            //service pour logger 
            //app.MapGet("/hello", (
            //    [FromServices] ILogger<Program> logger) =>
            //{
            //    // le ILogger vas nous donais accée a des méthodes qui vos retournée des message du ILogger  
            //    logger.LogInformation("lod depuis l'endpoint hello");
            //    Results.Ok("hi");

            //});


            //app.MapGet("/hello/{nom}", (
            //    [FromRoute] string nom,
            //    [FromServices] ILogger<Program> logger) =>
            //{
            //    logger.LogInformation($"on a salué{nom}", nom);
            //    Results.Ok($"Bonjour {nom}");

            //});

            #endregion

            #region Validation des Objet en entrée

            //la Façon manuel 
            //app.MapPost("/Person", ([FromBody] Personne p) =>
            //{
            //    //pour gérer les donnée d'entrer Manuellement 
            //    if (string.IsNullOrEmpty(p.LastName)) return Results.BadRequest("le nom est requis");
            //    if (string.IsNullOrEmpty(p.Name)) return Results.BadRequest("le prenom est requis");
            //    return Results.Ok(p);
            //});

            //app.MapPost("/Person", (
            //    [FromBody] Personne p,
            //    // on recuper un IValidator 
            //    [FromServices] IValidator<Personne> validator) =>
            //{
            //    //pour gérer les donnée d'entrer avec le package FluentValidation.DependencyInjectionExtens
            //    //et FluentValidation.AspNetCore

            //    /// on peut stocker validator.Validate(p) dans une variable 
            //    if (!validator.Validate(p).IsValid)
            //    {
            //        return Results.BadRequest(validator.Validate(p).Errors.Select(e => new
            //        {
            //            Message = e.ErrorMessage,
            //            e.PropertyName
            //        }));
            //    }
            //    return Results.Ok(p);
            //});
            #endregion

            #region EF & BDD

            //app.MapGet("/Persons", (
            //    [FromServices] ApiDbContext context) =>
            //{
            //    var peopole = context.People.ToList();
            //    return Results.Ok(peopole);

            //});


            //app.MapGet("/Persons/{id:int}", (
            //    [FromRoute] int id,
            //    [FromServices] ApiDbContext context) =>
            //{
            //    var peopole = context.People.Where(p => p.Id == id).FirstOrDefault();
            //    if (peopole is null) return Results.NotFound();

            //    return Results.Ok(peopole);
            //});

            //app.MapDelete("Persons/{id:int}", (
            //    [FromRoute] int id,
            //    [FromServices] ApiDbContext context) =>
            //    {
            //        //Approche moderne Bulc: en masse
            //        var result = context.People.Where(p => p.Id == id).ExecuteDelete();
            //        if (result > 0) return Results.NoContent();
            //        return Results.NotFound();
            //        // Approche ancienne 
            //        //var prson = context.People.Where(p => p.Id == id).FirstOrDefault();
            //        //if (prson is not null)
            //        //{
            //        //    context.People.Remove(prson);
            //        //    context.SaveChanges();
            //        //    return Results.NoContent();
            //        //}
            //        //return Results.NotFound();
            //    });

            //app.MapPut("Persons/{id:int}", (
            //    [FromRoute] int id,
            //    [FromBody] Personne po,
            //    [FromServices] ApiDbContext context) =>
            //{
            //    var result = context.People
            //    .Where(p => p.Id == id);
            //    var test = result.ExecuteUpdate(per =>
            //        per.SetProperty(pe => pe.LastName, po.LastName)
            //           .SetProperty(pe => pe.Name, po.Name));
            //    if(test>0) return Results.NoContent();
            //    return Results.NotFound(); 

            //    //var prson = context.People.Where(p => p.Id == id).FirstOrDefault();
            //    //if (prson is not null)
            //    //{
            //    //    prson.LastName = p.LastName;
            //    //    prson.Name = p.Name;
            //    //    context.People.Update(prson);
            //    //    context.SaveChanges();
            //    //   return Results.NoContent();
            //    //}
            //    //return Results.NotFound();

            //});

            //app.MapPost("/Persons", (
            //    [FromBody] Personne p,
            //    [FromServices] IValidator<Personne> validator,
            //    //on utilise DbContexte dans notre EndPoint 
            //    [FromServices] ApiDbContext Context) =>
            //{

            //    if (!validator.Validate(p).IsValid)
            //    {
            //        return Results.BadRequest(validator.Validate(p).Errors.Select(e => new
            //        {
            //            Message = e.ErrorMessage,
            //            e.PropertyName
            //        }));
            //    }
            //    // ajouter l'objet personne dans la BD
            //    Context.People.Add(p);
            //    // et sauvgarder le Changement 
            //    Context.SaveChanges();

            //    return Results.Ok(p);
            //});

            #endregion

            #region Rendre les Endpoint asynchrone

            //app.MapGet("personsss", async (
            //    [FromServices] ApiDbContext context,
            //    ///tout methode async possed un cancellationToken 
            //    //qui nous permet de géré notre methode async 
            //    //qui fournit par asp.net 
            //    //instruction lambda  entre {}
            //    //expression lambda sera dans la meme ligne () 
            //    CancellationToken token) =>
            //{
            //    var peopole = await context.People.ToListAsync(token);
            //    return Results.Ok(peopole);
            //});
            //app.MapGet("personss/{id:int}", async (
            //    [FromRoute] int id,
            //    [FromServices] ApiDbContext context) =>
            //{
            //    var peopole = await context.People.Where(p => p.Id == id).FirstOrDefaultAsync();
            //    if (peopole is null) return Results.NotFound();
            //    return Results.Ok(peopole);
            //});

            //app.MapDelete("personss/{id:int}", async (
            //    [FromRoute] int id,
            //    [FromServices] ApiDbContext context) =>
            //{
            //    var peopole = context.People.Where(p => p.Id == id).FirstOrDefault();
            //    if (peopole is not null)
            //    {
            //        context.People.Remove(peopole);
            //        await context.SaveChangesAsync();///////
            //        return Results.Accepted();
            //    }
            //    return Results.NotFound();
            //});

            //app.MapPost("/Persons", async (
            //    [FromBody] Personne p,
            //    [FromServices] IValidator<Personne> validator,
            //     [FromServices] ApiDbContext Context) =>
            //{

            //    if (!validator.Validate(p).IsValid)
            //    {
            //        return Results.BadRequest(validator.Validate(p).Errors.Select(e => new
            //        {
            //            Message = e.ErrorMessage,
            //            e.PropertyName
            //        }));
            //    }
            //    Context.People.Add(p);// what does mean ValueTask
            //    await Context.SaveChangesAsync();

            //    return Results.Ok(p);
            //});

            #endregion

            #region Ajouter un cache memoire

            #region Cach Memoire


            //app.MapGet("persons/{id:int}", (
            //   [FromRoute] int id,
            //   [FromServices] ApiDbContext context,
            //// imemorycache ce trouve dans l'espace extensions.caching.memory 
            ////par la suite on va gérer le cache manuellement ca durer de vie son comportement ...
            //    [FromServices] IMemoryCache cache) =>
            //{
            //    //on teste si le trygetvalue trouve une personne avec l'id fournie par la route
            //    // trygetvalue renvoie vrais si il trouve la clé  
            //    if (!cache.trygetvalue<personne>($"person{id}", out var person))
            //    {
            //        person = context.people.where(p => p.id == id).firstordefault();
            //        if (person is null) return results.notfound();

            //        cache.set($"person{id}", person);


            //    }
            //    return results.ok(person);
            //});
            //app.MapPut("Persons/{id:int}", (
            //    [FromRoute] int id,
            //    [FromBody] Personne po,
            //    [FromServices] ApiDbContext context,
            //    [FromServices] IMemoryCache cache) =>
            //{
            //    var result = context.People
            //    .Where(p => p.Id == id);
            //    var test = result.ExecuteUpdate(per =>
            //        per.SetProperty(pe => pe.LastName, po.LastName)
            //           .SetProperty(pe => pe.Name, po.Name));
            //    if (test > 0)
            //    {

            //        cache.Remove($"person{id}");
            //        return Results.NoContent();
            //    }
            //    return Results.NotFound();
            //});
            #endregion

            #region Cach sortie



            //app.MapGet("persons/{id:int}", (
            //   [FromRoute] int id,
            //   [FromServices] ApiDbContext context) =>
            //{
            //    var person = context.People.Where(p => p.Id == id).FirstOrDefault();
            //    if (person is null) return Results.NotFound();
            //    return Results.Ok(person);
            //})
            //    .CacheOutput();

            //app.MapPut("Persons/{id:int}", (
            //    [FromRoute] int id,
            //    [FromBody] Personne po,
            //    [FromServices] ApiDbContext context ) =>
            //{
            //    var result = context.People
            //    .Where(p => p.Id == id);
            //    var test = result.ExecuteUpdate(per =>
            //        per.SetProperty(pe => pe.LastName, po.LastName)
            //           .SetProperty(pe => pe.Name, po.Name));
            //    if (test > 0)
            //    {
            //        return Results.NoContent();
            //    }
            //    return Results.NotFound();
            //})
            //    .CacheOutput("ByID");

            #endregion

            #region Cach disperser 
            //app.MapGet("personsss", async (
            //    [FromServices] ApiDbContext context,
            //    CancellationToken token) =>
            //{
            //    var peopole = await context.People.ToListAsync(token);
            //    return Results.Ok(peopole);
            //});
            //app.MapGet("personss/{id:int}", async (
            //    [FromRoute] int id,
            //    [FromServices] ApiDbContext context,
            //    [FromServices] IDistributedCache cach) =>
            //{
            //    var peopole = await cach.GetAsync<Personne>($"personne_{id}");
            //    if (peopole is null)
            //    {
            //        peopole = await context.People.Where(p => p.Id == id).FirstOrDefaultAsync();
            //        if (peopole is null) return Results.NotFound();

            //        await cach.SetAsync($"personne_{id}", peopole);
            //    }
            //    return Results.Ok(peopole);
            //})
            //    .CacheOutput();

            //app.MapDelete("personss/{id:int}", async (
            //    [FromRoute] int id,
            //    [FromServices] ApiDbContext context) =>
            //{
            //    var peopole = context.People.Where(p => p.Id == id).FirstOrDefault();
            //    if (peopole is not null)
            //    {
            //        context.People.Remove(peopole);
            //        await context.SaveChangesAsync();
            //        return Results.Accepted();
            //    }
            //    return Results.NotFound();
            //});

            //app.MapPost("/Persons", async (
            //    [FromBody] Personne p,
            //    [FromServices] IValidator<Personne> validator,
            //     [FromServices] ApiDbContext Context) =>
            //{

            //    if (!validator.Validate(p).IsValid)
            //    {
            //        return Results.BadRequest(validator.Validate(p).Errors.Select(e => new
            //        {
            //            Message = e.ErrorMessage,
            //            e.PropertyName
            //        }));
            //    }
            //    Context.People.Add(p);
            //    await Context.SaveChangesAsync();

            //    return Results.Ok(p);
            //});
            #endregion
            #endregion

            #region gestion des services 
            //app.MapGet("persons", async (
            //    [FromServices] IPersonService service,
            //    CancellationToken token) =>
            //{
            //    var peopole = await service.GetAll();
            //    return Results.Ok(peopole);
            //});
            //app.MapGet("persons/{id:int}", async (
            //    [FromRoute] int id,
            //    [FromServices] IPersonService service) =>
            //{
            //    var peopole = await service.GetById(id);
            //    if (peopole is null) return Results.Ok(peopole);
            //    return Results.Ok(peopole);
            //});

            //app.MapDelete("persons/{id:int}", async (
            //    [FromRoute] int id,
            //    [FromServices] IPersonService service) =>
            //{
            //    var peopole = await service.Delete(id);
            //    if (peopole) return Results.Ok(peopole);
            //    return Results.NotFound();
            //});

            //app.MapPut("Persons/{id:int}", async (
            //    [FromRoute] int id,
            //    [FromBody] Personne po,
            //    [FromServices] IPersonService service) =>
            //{
            //    var result = await service.Update(id, po);
            //    if (result) return Results.Ok(result);
            //    return Results.NotFound();
            //});
            //app.MapPost("/Persons", async (
            //    [FromBody] Personne p,
            //    [FromServices] IPersonService service,
            //    [FromServices] IValidator<Personne> validator) =>
            //{
            //    if (!validator.Validate(p).IsValid)
            //    {
            //        return Results.BadRequest(validator.Validate(p).Errors.Select(e => new
            //        {
            //            Message = e.ErrorMessage,
            //            e.PropertyName
            //        }));
            //    }
            //    await service.Add(p);
            //    return Results.Ok(p);
            //});

            #endregion

            #region Utilisation du conteneur d'injection de dépendances avec les services  
            //app.MapGet("persons", async (
            //    [FromServices] IPersonService service,

            //    CancellationToken token) =>
            //{
            //    var peopole = await service.GetAll();
            //    return Results.Ok(peopole);
            //});

            //app.MapGet("persons/{id:int}", async (
            //    [FromRoute] int id,
            //    [FromServices] IDistributedCache cache,
            //    [FromServices] IPersonService service) =>
            //{
            //var peopole = await cache.GetAsync<PersonneOutPutModel>($"personne_{id}");
            //    if (peopole is not null)
            //    {
            //        peopole = await service.GetById(id);
            //        if (peopole is null) return Results.NotFound(); 
            //         await cache.SetAsync($"personne_{id}", peopole);
            //            return Results.Ok(peopole);

            //    }
            //    return Results.Ok(peopole);
            //});

            //app.MapDelete("persons/{id:int}", async (
            //    [FromRoute] int id,
            //    [FromServices] IPersonService service) =>
            //{
            //    var peopole = await service.Delete(id);
            //    if (peopole) return Results.Ok(peopole);
            //    return Results.NotFound();
            //});

            //app.MapPut("Persons/{id:int}", async (
            //    [FromRoute] int id,
            //    [FromBody] PersonneInPutModel po,
            //    [FromServices] IPersonService service) =>
            //{
            //    var result = await service.Update(id, po);
            //    if (result) return Results.Ok(po);
            //    return Results.NotFound();
            //});
            //app.MapPost("/Persons", async (
            //    [FromBody] PersonneInPutModel p,
            //    [FromServices] IPersonService service,
            //    [FromServices] IValidator<PersonneInPutModel> validator) =>
            //{
            //    if (!validator.Validate(p).IsValid)
            //    {
            //        return Results.BadRequest(validator.Validate(p).Errors.Select(e => new
            //        {
            //            Message = e.ErrorMessage,
            //            e.PropertyName
            //        }));
            //    }
            //    await service.Add(p);
            //    return Results.Ok(p);
            //});
            #endregion

            #region Swagger using 
            //app.MapGet(" ", async (
            //    [FromServices] IPersonService service,

            //    CancellationToken token) =>
            //{
            //    var peopole = await service.GetAll();
            //    return Results.Ok(peopole);
            //})

            //    .WithTags("PersonManagement");

            //app.MapGet("persons/{id:int}", async (
            //    [FromRoute] int id,
            //    [FromServices] IDistributedCache cache,
            //    [FromServices] IPersonService service) =>
            //{
            //    var peopole = await cache.GetAsync<PersonneOutPutModel>($"personne_{id}");
            //    if (peopole is not null)
            //    {
            //        peopole = await service.GetById(id);
            //        if (peopole is null) return Results.NotFound();
            //        await cache.SetAsync($"personne_{id}", peopole);
            //        return Results.Ok(peopole);

            //    }
            //    return Results.NotFound(peopole);
            //})
            //    .Produces(200)
            //    .Produces(404)
            //    .WithTags("PersonManagement");

            //app.MapDelete("persons/{id:int}", async (
            //    [FromRoute] int id,
            //    [FromServices] IPersonService service) =>
            //{
            //    var peopole = await service.Delete(id);
            //    if (peopole) return Results.Ok(peopole);
            //    return Results.NotFound();
            //})
            //    .Produces(200)
            //    .Produces(404)
            //    .WithTags("PersonManagement");

            //app.MapPut("Persons/{id:int}", async (
            //    [FromRoute] int id,
            //    [FromBody] PersonneInPutModel po,
            //    [FromServices] IPersonService service) =>
            //{
            //    var result = await service.Update(id, po);
            //    if (result) return Results.Ok(po);
            //    return Results.NotFound();
            //})
            //    .Produces(200)
            //    .Produces(404)
            //    .WithTags("PersonManagement");

            //app.MapPost("/Persons", async (
            //    [FromBody] PersonneInPutModel p,
            //    [FromServices] IPersonService service,
            //    [FromServices] IValidator<PersonneInPutModel> validator) =>
            //{
            //    if (!validator.Validate(p).IsValid)
            //    {
            //        return Results.BadRequest(validator.Validate(p).Errors.Select(e => new
            //        {
            //            Message = e.ErrorMessage,
            //            e.PropertyName
            //        }));
            //    }
            //    await service.Add(p);
            //    return Results.Ok(p);
            //})
            //    .Produces(200)
            //    .Produces(404)
            //    //cette methode explique que on attend un objet personneOutPutModel de format Json
            //    .Produces<PersonneOutPutModel>(contentType: "application/json")
            //    .Accepts<PersonneInPutModel>(contentType: "application/json")
            //    .WithTags("PersonManagement");
            #endregion

            #region Group les Endpoint 

            group.MapGet("", GetAll)

                .WithTags("PersonManagement");

            group.MapGet("/{id:int}", GetById)
                .Produces(200)
                .Produces(404)
                .WithTags("PersonManagement");

            group.MapDelete("/{id:int}", DeleteById)
                .Produces(200)
                .Produces(404)
                .WithTags("PersonManagement");

            group.MapPut("/{id:int}", PutById)
                .Produces(200)
                .Produces(404)
                .WithTags("PersonManagement");

            group.MapPost("", Post)
                .Produces(200)
                .Produces(404)
                //cette methode explique que on attend un objet personneOutPutModel de format Json
                .Produces<PersonneOutPutModel>(contentType: "application/json")
                .Accepts<PersonneInPutModel>(contentType: "application/json")
                .WithTags("PersonManagement");
            #endregion

            return group;
        }

        public static async Task<IResult> GetAll
             (
               [FromServices] IPersonService service,

               CancellationToken token)
        {
            {
                var peopole = await service.GetAll();
                return Results.Ok(peopole);
            }
        }

        public static async Task<IResult> GetById(
                [FromRoute] int id,
                [FromServices] IDistributedCache cache,
                [FromServices] IPersonService service)
        {
            var peopole = await cache.GetAsync<PersonneOutPutModel>($"personne_{id}");
            if (peopole is not null)
            {
                peopole = await service.GetById(id);
                if (peopole is null) return Results.NotFound();
                await cache.SetAsync($"personne_{id}", peopole);
                return Results.Ok(peopole);

            }
            return Results.NotFound(peopole);
        }
        public static async Task<IResult> DeleteById(
                [FromRoute] int id,
                [FromServices] IPersonService service)
        {
            var peopole = await service.Delete(id);
            if (peopole) return Results.Ok(peopole);
            return Results.NotFound();
        }
        public static async Task<IResult> PutById(
                [FromRoute] int id,
                [FromBody] PersonneInPutModel po,
                [FromServices] IPersonService service)
        {
            var result = await service.Update(id, po);
            if (result) return Results.Ok(po);
            return Results.NotFound();
        }
        public static async Task<IResult> Post(
                [FromBody] PersonneInPutModel p,
                [FromServices] IPersonService service,
                [FromServices] IValidator<PersonneInPutModel> validator)
        {
            if (!validator.Validate(p).IsValid)
            {
                return Results.BadRequest(validator.Validate(p).Errors.Select(e => new
                {
                    Message = e.ErrorMessage,
                    e.PropertyName
                }));
            }
            await service.Add(p);
            return Results.Ok(p);
        }
    }
}
