@echo off
FOR /f "tokens=*" %%i IN ('docker ps -aq') DO (
  docker stop %%i
)

docker container prune --force
docker volume prune --force