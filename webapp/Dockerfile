#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
#USER app
WORKDIR /app
EXPOSE 80
EXPOSE 443
#EXPOSE 8080
#EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["webapp/VolvoWebApp.csproj", "webapp/"]
RUN dotnet restore "./webapp/./VolvoWebApp.csproj"
COPY . .
#RUN dotnet tool install --global dotnet-ef
#RUN /root/.dotnet/tools/dotnet-ef migrations add InitialCreate --project testapi.csproj -v
#RUN /root/.dotnet/tools/dotnet-ef database update --project webapp
WORKDIR "/src/webapp"
RUN dotnet build "./VolvoWebApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "./VolvoWebApp.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "VolvoWebApp.dll"]