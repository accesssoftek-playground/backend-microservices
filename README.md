# backend-microservices
.NET 6 microservices based backend solution for the Admin Tool prototype

## Getting Started

``docker build -f .\AdminToolRootService\Dockerfile -t admintoolprototyperoot:latest .``

``docker run -p 5002:5002 --name AdminToolPrototypeRoot admintoolprototyperoot:latest``