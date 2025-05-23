## Para usar en local usar el puerto que da Visual Studio, en este caso 5057
```bash	
{
  "BaseUrl": "http://localhost:5057"
}
```	

## Para usar en local por medio de tunel de Visual Studio Code
```bash	
{
  "BaseUrl": "https://ps4f3q53-5000.euw.devtunnels.ms"
}
```	
## Para usar docker-compose usar puerto 5000
```bash	
{
  "BaseUrl": "http://localhost:5000"
}
```	

## Cambiar url en local 
```bash	
"ConnectionStrings": {
    "DefaultConnection": "Server=localhost,1433;Database=blazorWebApi;User Id=sa;Password=TuPasswordFuerte123!;TrustServerCertificate=True;"
}
```	
## Cambiar url en Docker-Compose 
```bash	
"ConnectionStrings": {
    "DefaultConnection": "Server=sqlserver,1433;Database=blazorWebApi;User Id=sa;Password=TuPasswordFuerte123!;TrustServerCertificate=True;"
}
```	

# Intrucciones para levantar el proyecto con docker-compose

```bash
docker compose down
docker compose up --build
```

# Intrucciones para analisis actualizar database de Sql Server cn Script
```bash
dotnet ef database update --project Blazor.Server/Blazor.Server.csproj
dotnet ef migrations script --output migration.sql --project Blazor.Server/Blazor.Server.csproj
```

# Instrucciones para analisis de SonarQube en local
```bash
sqp_12f14696be1cdead65f18a5b50e9834aa762cc7f
dotnet sonarscanner begin /k:"blazor-app" /d:sonar.host.url="http://localhost:9000"  /d:sonar.token="sqp_12f14696be1cdead65f18a5b50e9834aa762cc7f"
dotnet build
dotnet sonarscanner end /d:sonar.token="sqp_12f14696be1cdead65f18a5b50e9834aa762cc7f"
```

# (Experimental) Uso de Ngrok para exponer apps
```bash
2xVqc9THqz1eIYZdTdSra9NGnEh_7wPkL3c4b8pp1TzKaR99T
ngrok http http://localhost:8080
ngrok http http://localhost:5000
ngrok config add-authtoken 2xVqc9THqz1eIYZdTdSra9NGnEh_7wPkL3c4b8pp1TzKaR99T
ngrok start --config ./ngrok.yml --all

```