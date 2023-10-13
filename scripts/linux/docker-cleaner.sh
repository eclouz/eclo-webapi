# clear all docker containers
docker rm -vf $(docker ps -aq)
#clear all docker images
docker rmi -f $(docker images -aq)