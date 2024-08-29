# TeamManagement
The Team Management Project Application in ASP.NET Core 8

To run the project successfully, make sure you have postgres database installed and the migrations will be applied automatically.

To apply migrations manually, run this

```
dotnet ef database update --project TeamManagement.Infrastructure  --startup-project TeamManagement.WebAPI
```

If you are using a MAC, you can install Postgres on your Mac and EF packages for Postgres and change the connection string and apply migrations as well.
```
  builder.Services.AddDbContext<TaskManagementContext>(options =>
  {
    options.UseNpgsql(builder.Configuration["ConnectionString"], opt =>
    {
        opt.EnableRetryOnFailure(15, TimeSpan.FromSeconds(30), errorCodesToAdd: null);
    });
  });
```

To run the web api and razor pages projects, open the terminal and type this:

```
   Web API - dotnet run -p TaskManagement.WebAPI  -lp https
   Website - dotnet run -p TaskManagement.WebA  -lp https
```

<h3>Note: The master branch uses Postgres sql. To work with Microsoft Sql Server, use the Dev branch</h3>
