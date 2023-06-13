# NordAlbiCashier
Cash system for distributed cashes at Nordalbi

The basis of the DB implementation is a sample implementation of chapter 23 "Build a Data Access Layer with Entity Framework Core"  
"Pro C# 9 with .NET 5  
Foundational Principles and Practices in Programming"  
from Andrew Troelsen and Phillip Japikse.

# set up the docker containers
Currently you only need these two commands:  
`docker run --name NacPostgres -d -e "POSTGRES_PASSWORD=NacP@ssw0rd" -p 5432:5432 postgres:14-alpine`  
