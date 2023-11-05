# TeamManagement
The Team Management Project Application in ASP.NET Core 8

To run the project successfully, make sure you create the database and apply migration to create the tables

<i>dotnet ef database update --project TeamManagement.Infrastructure  --startup-project TeamManagement.WebAPI</i>

If you are using a MAC, you can install Postgres on your Mac and EF packages for Postgres and change the connection string and apply migrations as well.
