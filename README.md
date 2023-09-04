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
1. Customer.API : http://localhost:6003 (Local Enviroment: http://localhost:5003)

# Build Command CLI
docker-compose -f docker-compose.yml -f docker-compose.override.yml up -d --remove-orphans --build

