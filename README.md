# PortfolioWebAPI
.NET Web API Used for my various Portfolio demos.

## Note
This .NET Core Web API is a support tool for some of my other Portfolio applications. Feel free do look through the code, however I at this time I have not commented code to reflect usage.

This is meant to run in a docker container and the image has is publised as `estebanfmartinez/portfolio-webapi:latest` on Docker Hub. However this can be run straight out of Visual Studio/Code but is dependant on the existance of the database below.

## Demo Features
This demo features my use of the following technologies/frameworks:
- .NET Core C#
- Entity Framework Core
- PostgreSQL Database
- Automapper
- REST API
- Google Cloud SQL (In live sites)

## Requirements
The data repository for this Web API is a PostgreSQL database and is maintained using the image `estebanfmartinez/portfolio-postgre:latest` on Docker Hub. There is currently no stand-alone version in my GitHub repository. It's preferred to use the image anyway because it includes seeding scripts.

**Images are built under the linux engine.**

## Docker Container
As stated before this Web API can be run by code, but it is preferred to use the docker compose file to view the demo.

### How to Test
There is no need to download all the contents of the repository to test, just download the docker-compose.yaml file to a directory on your computer.

With docker installed open a terminal and navigate to the directory that the docker-compose file is located, then run the command: `docker compose up -d`. This will download the images, create docker containers and run them. Using the `-d` command will run them in detached mode freeing your terminal.

Open an internet browser and navigate to `http://localhost:5000/swagger` to view the Swagger UI API Browser.

When done with viewing the demo you can run the command `docker compose down -v` in the terminal. This will shutdown and remove the docker containers as well as delete the data volume used by the database.