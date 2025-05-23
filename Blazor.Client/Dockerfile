# Build del cliente
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Blazor.Client/Blazor.Client.csproj", "Blazor.Client/"]
RUN dotnet restore "Blazor.Client/Blazor.Client.csproj"
COPY . .
WORKDIR "/src/Blazor.Client"
RUN dotnet publish "Blazor.Client.csproj" -c Release -o /app/publish

# Servir con Nginx
FROM nginx:alpine
COPY --from=build /app/publish/wwwroot /usr/share/nginx/html
