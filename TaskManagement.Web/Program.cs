using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using Polly;
using System.Net.Mime;
using TaskManagement.Web.Filters;
using TaskManagement.Web.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new()
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(new byte[128]),
        ValidateAudience = false,
        ValidateIssuer = false,
        ClockSkew = TimeSpan.Zero
    };
});

//add a named client
builder.Services.AddHttpClient("TeamLead", configure =>
{
    configure.BaseAddress = new Uri("https://localhost:7138/TeamLead");
    configure.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
}).AddTransientHttpErrorPolicy(policyBuilder => policyBuilder.WaitAndRetryAsync(3, retryNumner => TimeSpan.FromSeconds(20)));

//add a named client for country
builder.Services.AddHttpClient("Country", configure =>
{
    configure.BaseAddress = new Uri("https://localhost:7138/Country");
    configure.DefaultRequestHeaders.Add(HeaderNames.Accept, MediaTypeNames.Application.Json);
}).AddTransientHttpErrorPolicy(policyBuilder => policyBuilder.WaitAndRetryAsync(3, retryNumner => TimeSpan.FromSeconds(20)));

builder.Services.AddHttpClient("TokenValidation", configure =>
{
    configure.BaseAddress = new Uri("https://localhost:7138/Validatetoken");
    configure.DefaultRequestHeaders.Add(HeaderNames.Accept, MediaTypeNames.Application.Json);
}).AddTransientHttpErrorPolicy(policyBuilder => policyBuilder.WaitAndRetryAsync(3, retryNumner => TimeSpan.FromSeconds(20)));


builder.Services.AddScoped<TeamLead>();
builder.Services.AddScoped<Country>();
builder.Services.AddScoped<TokenAuth>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
