# NordAlbiCashier
Cash system for distributed cashes at Nordalbi

The basis of the DB implementation is a sample implementation of chapter 23 "Build a Data Access Layer with Entity Framework Core"  
"Pro C# 9 with .NET 5  
Foundational Principles and Practices in Programming"  
from Andrew Troelsen and Phillip Japikse.

# set up the docker container for development
Currently you only need this command:  
`podman run --name NacPostgres -d -e "POSTGRES_PASSWORD=NacP@ssw0rd" -p 5432:5432 postgres:14-alpine`  

# build the docker image and push to docker hub
```powershell
$TagVersion = "2.2.1"
# dotnet tool install -g Microsoft.Web.LibraryManager.Cli
# cd Nac.Mvc/
# libman restore
# cd ..
podman build -f .\Nac.Mvc\Dockerfile -t munhei/nac_service:latest .
podman tag munhei/nac_service:latest munhei/nac_service:$TagVersion
podman image save -o _images/nac_service_$TagVersion.tar munhei/nac_service:$TagVersion
# docker image load -i _images/nac_service_$TagVersion.tar
podman login docker.io
podman image push munhei/nac_service
podman image push munhei/nac_service:$TagVersion
podman image ls munhei/nac_service:*
```

# deployment in docker compose
- `podman compose create` to create network and containers for the service
- `podman compose start` to start the services
- `podman compose stop` to stop the services
- `podman compose rm` to remove containers for the service
Shortcut:
- `podman compose up -d --wait` to create and start app and database  
- `podman compose down` to shutdown and remove services and network

- create the migration bundle: 
  `dotnet ef migrations bundle --force --self-contained --verbose -o ..\..\efbundle.exe`  
  `dotnet ef migrations bundle --force --self-contained -r linux-x64 --verbose -o ..\..\efbundle`  
  execute  
  `./efbundle -v --connection "Host=localhost;Username=postgres;Password=NacP@ssw0rd;Database=NacDB;Include Error Detail=true"`
- OR: create the DB structure, either with bundle or with `dotnet ef` commands
- fill database with SQL insert files
