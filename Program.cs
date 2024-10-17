using APIDEMO_.Middlewares;
using APIDEMO_.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using APIDEMO_.Context;
using APIDEMO_.Models;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;



static IEdmModel GetEdmModel()
{
    ODataConventionModelBuilder builder = new();
    builder.EntitySet<User>("Users");
    return builder.GetEdmModel();
}
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
///<sumary>
///Hace una instancia del servicio cada que hay un request.
///builder.Services.AddScoped<IUserDataService, UserDataService>();
/// </sumary> 

///<sumary>
/// Si queremos una instancia para toda la aplicacion desde que se levanta.
///builder.Services.AddSingleton<IUserDataService, UserDataService>();
/// </sumary>

///<sumary>
/// se crea
///builder.Services.AddTransient<IUserDataService, UserDataService>();
/// </sumary>
builder.Services.AddScoped<IUserDataService, UserDataService>();
builder.Services.AddCors(p =>
    p.AddPolicy("MyPolicy",
        builder =>
        {
            builder.AllowAnyHeader()
                .WithOrigins("http://127.0.0.1:5500")
                .WithMethods("GET", "POST", "PUT")
                .Build();
        }));


builder.Services.AddControllers(/*config =>
    {
        var policy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
            .Build();
        config.Filters.Add(new AuthorizeFilter(policy));
    }*/
    ).AddOData(options => options
     
    .AddRouteComponents("odata", GetEdmModel())
    .Select()
    .Filter()
    .OrderBy()
    .SetMaxTop(20)
    .Count()
    .Expand()
            )
    .AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
    );


/*var SecretKey = builder.Configuration.GetValue<string>("SecretKey") ;

var key = Encoding.ASCII.GetBytes(SecretKey);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options => {

    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateLifetime = true,
        ValidIssuer = "",
        ValidAudience = "",
        ValidateAudience = false,
        ValidateIssuer = false,
        ValidateIssuerSigningKey = true
    };
});
*/



// builder.Services.AddDbContext<ApiAppContext>(options =>
// {
//     options.UseInMemoryDatabase("AppDB");
// });



builder.Services.AddResponseCaching();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<ApiAppContext>(options =>options.UseSqlServer(builder.Configuration.GetConnectionString("connection_sql")) );




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
///<sumary>
///Denege el permiso  si el acceso no es con HTTPS
/// </sumary>
//app.UseHttpsRedirection();

///Cross-origin resource sharing
// Es un estándar que habilita una API dependiendo del origen del request. Por lo tanto, puede ayudar a limitar los llamados maliciosos desde orígenes desconocidos.
//     CORS no es un estándar propio de .NET Core por lo tanto otras tecnologías también lo han implementado de diferentes formas.
//     Usa middlewares para interceptar los request y dependiendo de su programación el usuario podrá usar o no la API.

/// Se puede pasar la politica y agregar al controlador que se requiere aplicar.


app.UseResponseCaching();

app.UseCors();





///app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

///app.Run(async context => await context.Response.WriteAsync("URL no encontrada"));
//Intercepta todos los request.
// app.Use(async (context, next) =>
// {
//     var yourMessage = "<!DOCTYPE html><html lang=\"en\"><head><title>Base   API is Running</title></head><body><h3>Base API is Running</h3>";
//     await context.Response.WriteAsync("URL Missing");
//     await next(context);
// });
//app.UseWelcomePage();
app.UseStatusMiddleware();


app.Run();
