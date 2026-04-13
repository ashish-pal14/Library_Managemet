FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["LibraryManagement.csproj", "."]
RUN dotnet restore "./LibraryManagement.csproj"
COPY . .
RUN dotnet publish "LibraryManagement.csproj" -c Release -o /app/publish

FROM build AS publish   # <-- this defines the stage 'publish'
WORKDIR /src

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LibraryManagement.dll"]
