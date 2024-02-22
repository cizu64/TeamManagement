# TeamManagement
The Team Management Project Application in ASP.NET Core 8

To run the project successfully, make sure you create the database and apply migration to create the tables

```
dotnet ef database update --project TaskManagement.Infrastructure  --startup-project TaskManagement.WebAPI
```

Or delete the files in the migrations folder and run the add migration command before the update command above
```
 dotnet ef migrations add first -p TaskManagement.Infrastructure --startup-project TaskManagement.WebAPI
```

