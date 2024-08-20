using AspWebApi;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using FluentValidation;
using System;
using AspWebApi.Validation;
using AspWebApi.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Caching.Distributed;
using AspWebApi;
using AspWebApi.Services;
using AspWebApi.Models;
using AspWebApi.DTO;
using AutoMapper;
using AspWebApi.Endpoint;
#region configuration d'une Api 


var builder = WebApplication.CreateBuilder();
#region AutoMapper
//   nos permet de valider notre configaration qui est normalement définie qpour les projet de test
//new MapperConfiguration(cfg => cfg.AddProfile<ApiMappingConfiguration>())
//                                    .AssertConfigurationIsValid();

builder.Services.AddAutoMapper(cfg => cfg.AddProfile<ApiMappingConfiguration>());
                                    

#endregion

#region configuration logger


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
#endregion

#region ajout DbContext dans les services
// on ajoute notre  DbContext dans les service
//
//builder.Services.AddDbContext<ApiDbContext>();
builder.Services
    .AddDbContext<ApiDbContext>(opt => 
    opt.UseSqlite(builder.Configuration
    .GetConnectionString("sqlite")));
#endregion

#region configuration Cache memoire
//builder.Services.AddMemoryCache();

#endregion

#region Cach sortie 

//builder.Services.AddOutputCache(opt =>
//{
//    opt.AddBasePolicy(b => b.Expire(TimeSpan.FromMinutes(1)));
//    opt.AddPolicy("Epire en 2 minute", b => b.Expire(TimeSpan.FromSeconds(40)));
//    opt.AddPolicy("ByID", b => b.SetVaryByRouteValue("id"));
//});

#endregion

#region conteneur d'injection de dépendances avec les services
builder.Services.AddScoped<IPersonService, EfCorePersonneservice>();

#endregion

///midelwear de cach sortie
//app.UseOutputCache();
#region Cach Disperser
//pour utiliser cache distributeur de cache  
//builder.Services.AddDistributedMemoryCache();
builder.Services.AddStackExchangeRedisCache(opt =>
{
    opt.Configuration = "localhost:6379";
});

//h
//builder.Services.AddDistributedSqlServerCache(); 
#endregion

#region Swagger Configuration
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#endregion

#endregion
var app = builder.Build();
#region Support Swagger

//limiter l'accée a swager que pour le mode devloppement 
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
#endregion

#region création de base de donée 
//quand l'app ce lance on demande a EF core de s'assuré que la BD est crée 
//rt de s'assurer de la crée si elle n'existe pas 
//
//app.Services

//    .CreateScope().ServiceProvider
//    .GetRequiredService<ApiDbContext>().Database
//    .EnsureCreated();
#endregion

#region Migration de la BDD

await app.Services
    .CreateScope().ServiceProvider
    .GetRequiredService<ApiDbContext>().Database
    .MigrateAsync();
#endregion

#region Group Map
//on groupe les EndPoint par préfix de la route
app.MapGroup("/person")
    .MapPersonEndpoint();

#endregion


app.Run();

