# project-future-six-dotnet-academy
Code.Hub | .NET PF6 Academy

The main project for all lessons is the LessonOne/DI.TinyCrm.Web web application. It is an ASP.NET Core MVC 5 web application.

To run the migrations for the project
1. Right click on solution in Visual Studio
2. Click "Open in Terminal"
3. Type the following command:
  - dotnet ef database update --project LessonOne\DI.TinyCrm.Persistence --startup-project LessonOne\DI.TinyCrm.Web

In order to run the above command you have to open the appSettings.json file and put in the correct connection string that points to your SQL server instance running in your machine.

To create new migrations for the project and experiment:
1. Do the necessary changes in your Entities
2. Right click on solution in Visual Studio
3. Click "Open in Terminal"
4. Type the following command: 
  - dotnet ef migrations add "{MigrationName}" --project LessonOne\DI.TinyCrm.Persistence --startup-project LessonOne\DI.TinyCrm.Web --output-dir Migrations
