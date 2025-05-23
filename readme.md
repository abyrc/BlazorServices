docker-compose up --build
dotnet ef database update --project Blazor.Server/Blazor.Server.csproj
dotnet ef migrations script --output migration.sql --project Blazor.Server/Blazor.Server.csproj


```bash
docker-compose up --build
dotnet ef database update --project Blazor.Server/Blazor.Server.csproj
dotnet ef migrations script --output migration.sql --project Blazor.Server/Blazor.Server.csproj
```

```bash
sqp_12f14696be1cdead65f18a5b50e9834aa762cc7f
dotnet sonarscanner begin /k:"blazor-app" /d:sonar.host.url="http://localhost:9000"  /d:sonar.token="sqp_12f14696be1cdead65f18a5b50e9834aa762cc7f"
dotnet build
dotnet sonarscanner end /d:sonar.token="sqp_12f14696be1cdead65f18a5b50e9834aa762cc7f"
```
