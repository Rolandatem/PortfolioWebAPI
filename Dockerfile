FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

COPY *.csproj ./
RUN dotnet restore

COPY . ./
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app/publish ./

ENV ASPNETCORE_URLS=http://+:5000
ENV ASPNETCORE_ENVIRONMENT=DockerSolo
EXPOSE 5000

ENTRYPOINT ["dotnet", "PortfolioWebAPI.dll"]
#ENTRYPOINT ["dotnet", "watch", "run", "--urls", "http://0.0.0.0:5000"]