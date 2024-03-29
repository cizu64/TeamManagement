# TeamManagement
The Team Management Project Application in ASP.NET Core 8

To run the project successfully, make sure you create the database and apply migration to create the tables

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

<h3>Note: The master branch uses Postgres sql. To work with Microsoft Sql Server, use the Dev branch</h3>
