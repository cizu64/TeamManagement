using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.RateLimiting;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.SeedWork;
using TaskManagement.Infrastructure;
using TaskManagement.Infrastructure.Repository;
using TaskManagement.WebAPI.BackgroundServices;
using TaskManagement.WebAPI.Filter;
using TaskManagement.WebAPI.Filters;
using TaskManagement.WebAPI.Security;
using static System.Net.Mime.MediaTypeNames;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        options.InvalidModelStateResponseFactory = context =>
            new BadRequestObjectResult(new
            {
                Detail = context.ModelState.Values.FirstOrDefault().Errors.FirstOrDefault().ErrorMessage,
                StatusCode = (int)HttpStatusCode.BadRequest
            });
    });
builder.Services.AddRateLimiter(options =>
{
    options.AddFixedWindowLimiter(policyName: "fixed", options =>
    {
        options.PermitLimit = 2;
        options.Window = TimeSpan.FromSeconds(30);
        options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        options.QueueLimit = 2; //remove this if you want the RejectionStatusCode and the OnRejected to be invoked. 

    });
    options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
    options.OnRejected = async (context, _) =>
    {
        await context.HttpContext.Response.WriteAsync("too many request");
    };
});

builder.Services.AddMemoryCache();
builder.Services.AddResponseCaching();
builder.Services.AddDataProtection();

const string CorsPolicy = "CORS";
builder.Services.AddCors(options =>
{
    options.AddPolicy(CorsPolicy, policy =>
    {
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
        policy.AllowAnyOrigin();
    });
});
var key = Encoding.UTF8.GetBytes(builder.Configuration["JWT:KEY"]);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new()
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateAudience = false,
        ValidateIssuer = false,
        ClockSkew = TimeSpan.Zero
    };
});


builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("TeamLeadOnly", policy =>
    {
        policy.RequireClaim(ClaimTypes.Role);
        policy.RequireRole("TeamLead");
    });
    options.AddPolicy("TeamMemberOnly", policy =>
    {
        policy.RequireClaim(ClaimTypes.Role);
        policy.RequireRole("TeamMember");
    });
});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new List<string>()
        }
    });
});

// builder.Services.AddDbContext<TaskManagementContext>(options =>
// {
//     options.UseSqlServer(builder.Configuration["ConnectionString"], options =>
//     {
//         options.EnableRetryOnFailure(maxRetryCount: 5, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
//     });
// });

  builder.Services.AddDbContext<TaskManagementContext>(options =>
  {
    options.UseNpgsql(builder.Configuration["ConnectionString"], opt =>
    {
        opt.EnableRetryOnFailure(15, TimeSpan.FromSeconds(30), errorCodesToAdd: null);
    });
  });
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<JwtAuth>();
builder.Services.AddScoped<DataProtector>();
builder.Services.AddSingleton<Logger>();
builder.Services.AddHostedService<CustomLogger>();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddHealthChecks().AddSqlServer(builder.Configuration["ConnectionString"]);

var app = builder.Build();

await SeedAsync(app);
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseExceptionHandler(new ExceptionHandlerOptions()
    {
        AllowStatusCode404Response = true,
        ExceptionHandlingPath = "/error"
    });
    
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseRateLimiter();
app.UseCors(CorsPolicy);

app.UseResponseCaching(); //UseCors must be called before UseResponseCaching

app.MapControllers();
app.MapHealthChecks("/healths");
app.Run();

async Task SeedAsync(WebApplication host)
{
    using (var scope = host.Services.CreateScope())
    {
        var service = scope.ServiceProvider;
        var context = service.GetRequiredService<TaskManagementContext>();
        await TaskManagementContextSeed.SendAsync(context);
    }
}
