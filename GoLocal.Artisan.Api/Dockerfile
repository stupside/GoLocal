FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["GoLocal.Artisan.Api/GoLocal.Artisan.Api.csproj", "GoLocal.Artisan.Api/"]
RUN dotnet restore "GoLocal.Artisan.Api/GoLocal.Artisan.Api.csproj"
COPY . .
WORKDIR "/src/GoLocal.Artisan.Api"
RUN dotnet build "GoLocal.Artisan.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GoLocal.Artisan.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GoLocal.Artisan.Api.dll"]
