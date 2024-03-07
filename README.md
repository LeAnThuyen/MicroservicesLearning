# Tana Production
-- docker compose -f docker-compose.yml -f docker-compose.override.yml up -d --remove-orphans
( câu lệnh này giúp chạy docker)
-- docker ps
( câu lệnh này giúp liệt kê các container và image đang chạy)
-- docker compose down
( câu lệnh này giúp tắt  các container và image đang chạy)
-- docker ps ( câu lệnh này giúp liệt kê các câu lệnh đang chạy)

# Main CLI
1. add-migration {{Your Update Or Init For Database}}
2. update-database 
3. docker-compose -f docker-compose.yml -f docker-compose.override.yml up -d --remove-orphans (chế độ chạy ở local không build)

# Main port For Running on Local Environment
1.Portainer: http://localhost:9000 - username:admin - password : anthuyenle08
2.Kibana: http://localhost:5601 - username:elastic - password : admin
3.RabbitMq: http://localhost:15672 - username:guest - password : guest
4.Postgres: http://localhost:5050/browser/ - username:admin - password : admin1234

# Main port For Running on Docker Environment
1. Product.API : http://localhost:6002/swagger/index.html (Local Environment: http://localhost:5002/swagger/index.html)
2. Customer.API : http://localhost:6003 (Local Environment: http://localhost:5003)
3. Basket.API : http://localhost:6004/swagger/index.html (Local graEnvironment: http://localhost:5004/swagger/index.html)
4. Ordering.API : http://localhost:6005/swagger/index.html (Local Environment: http://localhost:5005/swagger/index.html)

# Build Command CLI
docker-compose -f docker-compose.yml -f docker-compose.override.yml up -d --remove-orphans --build

# Install Redis for window
1. go to https://github.com/microsoftarchive/redis/releases to download redis and install
2. turn on terminal and see first location (Example below is my terminal)
C:\Users\ASUS>
and after download redis successfully and it will be located in Program Files
3. Copy all and paste it in your first location that found on in step 2
4. turn on your terminal and type
+ redis-server : start redis
+ redis-cli : open redis cli
+ ping : check redis if it response PONG that is Oke
+ set {yourkey} {value} : set key values in redis
+ get {yourkey} : get values of your key that you already set
+ incr {yourkey}: increase value of key
+ lpush {yourkey} {value1 value2 ...}: create a list 
+ lindex {yourkey} 0: get first value that you push in list
+ lrange {yourkey} 0 -1: get all value of list
+ 
# Install Redis for MacOS
1. Checking version of Home brew by the following cli " brew --version "
2. Install redis " brew install redis "
3. Run command " redis-server " to checking install successfully Redis on MacOS 
* Learn more about Redis cli
- keys * (select all key stored in redis)
- hgetall (select your value of key if it is hash)
- del {name of  your key} (delete key has stored)
- type {name of your key} check type of your key has stored

# About Clean Architecture
- It's quite same Abp Framework structure 
- Application 
- Domain 
- Infrastructure 
- Host Api (Main API) 

# About Entity Framework CLI
- To do create a migration, you can use two cli command below here and you must cd to root folder which has your project (Ordering. Infrastructure and Ordering.Api). Ordering.Api as a host to run command and another side, Ordering.Infrastructure contain OrderContext
- Firstly, dotnet ef migrations add "{Your migration name that you wanna named}" -p Ordering.Infrastructure --startup-project Ordering.Api -o Persistence/Migrations ( -p (FullName is project) : it's mean Project target contain Context File, -o (-o is output dir) which is folder that you want Context file contained in there))
- Secondly, dotnet ef database update -p Ordering.Infrastructure --startup-project Ordering.Api ( Inorder to updating your migration)
- Learn more about Entity Framework core : https://www.entityframeworktutorial.net/code-first/what-is-code-first.aspx (Highly Recommend use Fluent API -> Code First -> DatabaseFirst)
# Tana when the king is back - killin it
- Backup times (Product API, Customer API, Basket API, Ordering API)
# More than about Dotnet Ef
- dotnet ef migrations add {Name of Migration} to create new migration
- dotnet ef database update to update latest migration version that you wanna migrate.
# Secret password 
- pjka btgu hxtj kmoj
# All of step to write a Dockerfile for csproj file
1. Include dotnet version and dotnet sdk version like command below

   FROM mcr.microsoft.com/dotnet/aspnet:6.0 as base
   WORKDIR /app
   EXPOSE 80

   FROM mcr.microsoft.com/dotnet/sdk:6.0 as build
+ "as base" and "as build". as base is the base dotnet version of csproj file. as build is the environment of this one to build csproj file (Force same version with base).

2. Let's Copy all of path reference of csproj file and write like below
   COPY ["{ Typing your path of csproj file. example: Services/Ordering.API/Ordering.Api/Ordering.API.csproj }","{ Typing your path of csproj file and remove from start of path csproj file such as Services/Ordering.API/Ordering.Api/ }"]
+ do similar for all remaining reference

3. Running command build and publish

   RUN dotnet restore "{ Type your path of csproj. example: Services/Ordering.API/Ordering.Application/Ordering.Application.csproj } "
   COPY . .
   WORKDIR "{ Type your path of csproj and remove from start of csproj file. example: /src/Services/Ordering.API/Ordering.Api/ }"
   RUN dotnet build "{ Type your csproj file name. example: Ordering.API.csproj }" -c Release -o /app/build


   FROM build as publish
   RUN dotnet publish "{ Type your csproj file name. example: Ordering.API.csproj }" -c Release -o /app/publish
   FROM base as final

5. Last step, copy all files were published and run it 
   WORKDIR /app
   COPY --from=publish /app/publish .
   ENTRYPOINT ["dotnet","{Type your name of csproj file and following by end of .dll, example Ordering.API.dll"]