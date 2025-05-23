FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Blazor.Server/Blazor.Server.csproj", "Blazor.Server/"]
COPY ["BlazorCRUD.Shared/BlazorCRUD.Shared.csproj", "BlazorCRUD.Shared/"]
RUN dotnet restore "Blazor.Server/Blazor.Server.csproj"
COPY . .
WORKDIR "/src/Blazor.Server"
RUN dotnet publish "Blazor.Server.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
EXPOSE 5000
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Blazor.Server.dll"]
