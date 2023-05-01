#dotnet new gitignore

dotnet new sln -n NordAlbiCashier_AllProjects

dotnet new classlib -lang c# -n Nac.Models -o .\Nac.Models
dotnet add Nac.Models package Microsoft.EntityFrameworkCore.Abstractions
dotnet add Nac.Models package System.Text.Json
# for extented enum to string conversions
dotnet add Nac.Models package Macross.Json.Extensions 
dotnet sln .\NordAlbiCashier_AllProjects.sln add .\Nac.Models

dotnet new classlib -lang c# -n Nac.Dal -o .\Nac.Dal
dotnet add Nac.Dal reference Nac.Models
dotnet add Nac.Dal package Microsoft.EntityFrameworkCore
dotnet add Nac.Dal package Microsoft.EntityFrameworkCore.Design
dotnet add Nac.Dal package Microsoft.EntityFrameworkCore.Tools
dotnet add Nac.Dal package Npgsql.EntityFrameworkCore.PostgreSQL
dotnet add Nac.Dal package EFCore.NamingConventions
dotnet sln .\NordAlbiCashier_AllProjects.sln add .\Nac.Dal

dotnet new xunit -lang c# -n Nac.Dal.Tests -o .\Nac.Dal.Tests
dotnet add Nac.Dal.Tests package Microsoft.EntityFrameworkCore
dotnet add Nac.Dal.Tests package Microsoft.Extensions.Configuration.Json
dotnet remove Nac.Dal.Tests package Microsoft.NET.Test.Sdk
dotnet add Nac.Dal.Tests package Microsoft.NET.Test.Sdk
dotnet add Nac.Dal.Tests package Npgsql.EntityFrameworkCore.PostgreSQL
# for extented unit test asserts, mainly to compare object trees
dotnet add Nac.Dal.Tests package FluentAssertions
dotnet add Nac.Dal.Tests reference Nac.Models
dotnet add Nac.Dal.Tests reference Nac.Dal
dotnet sln .\NordAlbiCashier_AllProjects.sln add Nac.Dal.Tests

dotnet new classlib -lang c# -n Nac.Services -o .\Nac.Services
dotnet add Nac.Services package Microsoft.Extensions.Hosting.Abstractions
dotnet add Nac.Services package Microsoft.Extensions.Options
dotnet add Nac.Services package Serilog.AspNetCore
dotnet add Nac.Services package Serilog.Enrichers.Environment
dotnet add Nac.Services package Serilog.Settings.Configuration
dotnet add Nac.Services package Serilog.Sinks.Console
dotnet add Nac.Services package Serilog.Sinks.File
dotnet add Nac.Services package Serilog.Sinks.Postgresql.Alternative
dotnet add Nac.Services package System.Text.Json
dotnet add Nac.Services reference Nac.Models
dotnet add Nac.Services reference Nac.Dal
dotnet sln .\NordAlbiCashier_AllProjects.sln add Nac.Services

dotnet new mvc -lang c# -n Nac.Mvc -au none -o .\Nac.Mvc
dotnet add Nac.Mvc package AutoMapper
dotnet add Nac.Mvc package System.Text.Json
dotnet add Nac.Mvc package LigerShark.WebOptimizer.Core
dotnet add Nac.Mvc package Microsoft.Web.LibraryManager.Build
dotnet add Nac.Mvc package Npgsql.EntityFrameworkCore.PostgreSQL
dotnet add Nac.Mvc package Microsoft.VisualStudio.Web.CodeGeneration.Design
dotnet add Nac.Mvc reference Nac.Models
dotnet add Nac.Mvc reference Nac.Dal
dotnet add Nac.Mvc reference Nac.Services
dotnet sln .\NordAlbiCashier_AllProjects.sln add Nac.Mvc

dotnet new classlib -lang c# -n Nac.Lib -o .\Nac.Lib
dotnet add Nac.Lib package System.Text.Json
dotnet add Nac.Lib reference Nac.Models
dotnet sln .\NordAlbiCashier_AllProjects.sln add Nac.Lib
dotnet add Nac.Api reference Nac.Lib

dotnet new xunit -lang c# -n Nac.Lib.Tests -o .\Nac.Lib.Tests
dotnet remove Nac.Lib.Tests package Microsoft.NET.Test.Sdk
dotnet add Nac.Lib.Tests package Microsoft.NET.Test.Sdk
# for extented unit test asserts, mainly to compare object trees
dotnet add Nac.Lib.Tests package FluentAssertions
dotnet add Nac.Lib.Tests reference Nac.Lib
dotnet sln .\NordAlbiCashier_AllProjects.sln add Nac.Lib.Tests
