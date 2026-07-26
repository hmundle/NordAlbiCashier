FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 5000

ENV ASPNETCORE_URLS=http://+:5000

# Install curl (or wget) while still root
RUN apt-get update \
    && apt-get install -y --no-install-recommends curl \
    && rm -rf /var/lib/apt/lists/*
    
# Switch to the default non-root user provided by the .NET image
USER $APP_UID

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY ["Nac.Models/Nac.Models.csproj", "Nac.Models/"]
COPY ["Nac.Lib/Nac.Lib.csproj", "Nac.Lib/"]
COPY ["Nac.Dal/Nac.Dal.csproj", "Nac.Dal/"]
COPY ["Nac.Services/Nac.Services.csproj", "Nac.Services/"]
COPY ["Nac.Mvc/Nac.Mvc.csproj", "Nac.Mvc/"]
RUN dotnet restore "Nac.Mvc/Nac.Mvc.csproj"
COPY . .
WORKDIR "/src/Nac.Mvc"
RUN dotnet build "Nac.Mvc.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Nac.Mvc.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Nac.Mvc.dll"]
