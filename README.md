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

# Main port For Running on Local Enviroment
1.Protainer: http://localhost:9000 - username:admin - password : anthuyenle08
2.Kibana: http://localhost:5601 - username:elastic - password : admin
3.RabbitMq: http://localhost:15672 - username:guest - password : guest
4.Postgres: http://localhost:5050/browser/ - username:admin - password : admin1234

# Main port For Running on Docker Enviroment
1. Product.API : http://localhost:6002 (Local Enviroment: http://localhost:5002)
2. Customer.API : http://localhost:6003 (Local Enviroment: http://localhost:5003)
3. Baseket.API : http://localhost:6004 (Local Enviroment: http://localhost:5004)
4. Ordering.API : http://localhost:6005 (Local Enviroment: http://localhost:5005)

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
+ redis-cli : onpen redis cli
+ ping : check redis if it respone PONG that is Oke
+ set {yourkey} {value} : set key values in redis
+ get {yourkey} : get values of your key that you already set
+ incr {yourkey}: increase value of key
+ lpush {yourkey} {value1 value2 ...}: create a list 
+ lindex {yourkey} 0: get first value that you push in list
+ lrange {yourkey} 0 -1: get all value of list

# About Clean Achitechture
- It's quite same Abp Framwork structure 
- Application 
- Domain 
- Infrastruture 
- Host Api (Main API) 

# About Entity Framework CLI
- To do create a migration, you can use two cli command below here and you must cd to root folder which has your project (Ordering.Infrastucture and Ordering.Api). Ordering.Api as a host to run command and another side, Ordering.Infrastructure contain OrderContext
- Firstly, dotnet ef migrations add "{Your migration name that you wanna named}" -p Ordering.Infrastructure --startup-project Ordering.Api -o Persistence/Migrations ( -p (FullName is project) : it's mean Project target contain Context File, -o (-o is output dir) which is folder that you want Context file contained in there))
- Secondly, dotnet ef database update -p Ordering.Infrastructure --startup-project Ordering.Api ( Inorder to updating your mirgration)
- Learn more about Entity Framework core : https://www.entityframeworktutorial.net/code-first/what-is-code-first.aspx (Highly Recomment use Fluent API -> Code Firt -> DatabaseFirst)
