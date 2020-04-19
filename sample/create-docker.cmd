@echo off

docker run --name postgres -p 5432:5432 -e POSTGRES_PASSWORD=!p@ssword -d postgres:latest
docker run --name mysql -p 3306:3306 -e MYSQL_ROOT_PASSWORD=!p@ssword -d mysql:latest