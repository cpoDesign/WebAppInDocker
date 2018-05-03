== Installation of application and its exposure through docker

1, Install .NET Core 2 SDK
2, Create directory into your website directory
   : dotnet new mvc --auth Individual -f netcoreapp2.0

3, Ensure you can compile the application
    dotnet run
   the following steps will be executed:
   - restore your application
   - application will be compiled
   - application will start on top of kestrel on url: http://localhost:5000

Dockerise the application steps
1, Create new "DockerFile" in root of the application

    FROM microsoft/aspnetcore-build:2.0 AS build-env
    WORKDIR /app

    # Copy csproj and restore as distinct layers
    COPY *.csproj ./
    RUN dotnet restore

    # Copy everything else and build
    COPY . ./
    RUN dotnet publish -c Release -o out

    # Build runtime image
    FROM microsoft/aspnetcore:2.0
    WORKDIR /app
    COPY --from=build-env /app/out .
    ENTRYPOINT ["dotnet", "WebApp.dll"]
    
 - the entry point dll name must reflect the compiled application dll.

 2, Add docker ignore file: ".dockerignore"

        bin\
        obj\

    to ensure we are using as little footprint on the container as possible

3, Execute the following command

    docker build -t aspnetapp .

    to build the application
4, Execute the following command to get the container accessible
    
   docker run -d -p 8080:80 --name myapp aspnetapp

   This will allow us to see that the application is hosted on http://localhost:8080

5, Use docker compose command
    ``` docker-compose up

    will run and bring the application but the terminal will be still attached to the process

    ``` docker-compose up -d

    will run and bring the application up but it will detach from the process after. so the containers do run under background

