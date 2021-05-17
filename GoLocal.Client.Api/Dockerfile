FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["GoLocal.Client.Api/GoLocal.Client.Api.csproj", "GoLocal.Client.Api/"]
RUN dotnet restore "GoLocal.Client.Api/GoLocal.Client.Api.csproj"
COPY . .
WORKDIR "/src/GoLocal.Client.Api"
RUN dotnet build "GoLocal.Client.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GoLocal.Client.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GoLocal.Client.Api.dll"]
