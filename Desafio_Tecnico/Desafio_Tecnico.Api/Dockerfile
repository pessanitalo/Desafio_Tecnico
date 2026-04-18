# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy all files
COPY . .

# Restore and build
RUN dotnet restore "Desafio_Tecnico/Desafio_Tecnico.Api/Desafio_Tecnico.Api.csproj"
RUN dotnet build "Desafio_Tecnico/Desafio_Tecnico.Api/Desafio_Tecnico.Api.csproj" -c Release -o /app/build

# Publish
FROM build AS publish
RUN dotnet publish "Desafio_Tecnico/Desafio_Tecnico.Api/Desafio_Tecnico.Api.csproj" -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

EXPOSE 80

COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "Desafio_Tecnico.Api.dll"]
