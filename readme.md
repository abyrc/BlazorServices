docker-compose up --build
dotnet ef database update --project Blazor.Server/Blazor.Server.csproj
dotnet ef migrations script --output migration.sql --project Blazor.Server/Blazor.Server.csproj