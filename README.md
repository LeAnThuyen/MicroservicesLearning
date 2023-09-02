# Tana Production
-- docker compose -f docker-compose.yml -f docker-compose.override.yml up -d --remove-orphans
( câu lệnh này giúp chạy docker)
-- docker ps
( câu lệnh này giúp liệt kê các container và image đang chạy)
-- docker compose down
( câu lệnh này giúp tắt  các container và image đang chạy)

# Main port
1.Protainer: http://localhost:9000 - username:admin - password : anthuyenle08
2.Kibana: http://localhost:5601 - username:elastic - password : admin
3.RabbitMq: http://localhost:15672 - username:guest - password : guest
# Build Command CLI
docker-compose -f docker-compose.yml -f docker-compose.override.yml up -d --remove-orphans --build

