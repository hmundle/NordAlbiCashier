# NordAlbiCashier
Cash system for distributed cashes at Nordalbi

The basis of the DB implementation is a sample implementation of chapter 23 "Build a Data Access Layer with Entity Framework Core"  
"Pro C# 9 with .NET 5  
Foundational Principles and Practices in Programming"  
from Andrew Troelsen and Phillip Japikse.

# set up the docker container for development
Currently you only need this command:  
`docker run --name NacPostgres -d -e "POSTGRES_PASSWORD=NacP@ssw0rd" -p 5432:5432 postgres:14-alpine`  

# build the docker image and push to docker hub
`docker build -f .\Nac.Mvc\Dockerfile -t munhei/nac_service:latest .`
`docker image push munhei/nac_service`

# deployment in docker compose
- `docker compose create` to create network and containers for the service
- `docker compose start` to start the services
- `docker compose stop` to stop the services
- `docker compose rm` to remove containers for the service
Shortcut:
- `docker compose up -d` to create and start app and database  
- `docker compose down` to shutdown and remove services and network

- create the DB structure, either with bundle or with `dotnet ef` commands
- fill database with SQL insert files



