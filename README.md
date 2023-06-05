Need to download : 
1- git & git bash
2- ASP.NET V7 : https://dotnet.microsoft.com/en-us/download

Note : maybe you need to restart your device so the env path is set in your local machine

Steps : 
1- CLone the repo : https://github.com/tareq-abuaqlain/DB_project.git
2- Type : cd ./DB_project then code .
3- Type :  
dotnet restore
dotnet build
4- Delete these folders : obj , bin , .vs
5- Go to folder Data in ContextDB.cs , change line 19 to your database connection 
Note : you need to create your own database (postgres) from dBeaver
6- Type : dotnet run
7- Open https://localhost:7111

Note : you need to download (the run time for asp ) this also :
 https://dotnet.microsoft.com/en-us/download/dotnet/thank-you/runtime-6.0.16-windows-x64-installer?cid=getdotnetcore
 https://aka.ms/dotnet-core-applaunch?framework=Microsoft.AspNetCore.App&framework_version=6.0.0&arch=x64&rid=win10-x64