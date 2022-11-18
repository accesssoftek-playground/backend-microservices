#!/bin/sh -e
cd ../
sudo docker build -f ./AdminToolRootService/Dockerfile -t admintoolprototyperoot:latest . && \
 sudo docker run -d --rm -p 5002:5002 --name AdminToolPrototypeRoot admintoolprototyperoot:latest