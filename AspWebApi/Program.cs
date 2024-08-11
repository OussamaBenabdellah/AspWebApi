
using AspWebApi;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using FluentValidation;
using System;
using AspWebApi.Validation;

var builder = WebApplication.CreateBuilder();
/// la configuration des systeme de log ce fait  entre l'instantiation de WebApplicationBuilder  
/// et WebApplication
/// 

//ClearProviders vas supprimer ce que AspNet a mis pâr default 
builder.Logging.ClearProviders();
//crée la configuration de Serielog pour logger dans la console 
//  crée la "loggerconfiguration" de type LoggerConfiguration qui ce trouve
//  dans la bibliotheque serielog  
var loggerConfiguration = new LoggerConfiguration()
        // .WriteTo  pour ecrir dans la console 
        // jusqu'as la la configuration est crée     
        .WriteTo.Console()
        //.Write.File on l'obtient du package Serilog.Sinks.file
        .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Minute);

// on crée un logger de type serilog.Core.Logger
// a partir de loggerConfiguration
var logger = loggerConfiguration.CreateLogger();
// AddSerilog est une extantion fournis par Serlilog.NetCore 
builder.Logging.AddSerilog(logger);

// on doit ajouter using FluentValidation
// avec cette methode on crée notre validator
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

var app = builder.Build();
#region Utilisation ILogger 

// on ajoute un Service ILogger de Type Program avec la variable logger   pour accéeder au méthode de ce service 
//service pour logger 
app.MapGet("/hello", (
    [FromServices] ILogger<Program> logger) =>
{
    // le ILogger vas nous donais accée a des méthodes qui vos retournée des message du ILogger  
    logger.LogInformation("lod depuis l'endpoint hello");
    Results.Ok("hi");

});


app.MapGet("/hello/{nom}", (
    [FromRoute] string nom,
    [FromServices] ILogger<Program> logger) =>
{
    logger.LogInformation($"on a salué{nom}", nom);
    Results.Ok($"Bonjour {nom}");

});

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

app.MapPost("/Person", (
    [FromBody] Personne p,
    // on recuper un IValidator 
    [FromServices] IValidator<Personne> validator) =>
{
    //pour gérer les donnée d'entrer avec le package FluentValidation.DependencyInjectionExtens
    //et FluentValidation.AspNetCore

    /// on peut stocker validator.Validate(p) dans unevariable 
    if (!validator.Validate(p).IsValid)
    {
        return Results.BadRequest(validator.Validate(p).Errors.Select(e => new
        {
            Message = e.ErrorMessage,
            PropertyName = e.PropertyName
        }));
    }
    return Results.Ok(p);
});
#endregion
app.Run();

