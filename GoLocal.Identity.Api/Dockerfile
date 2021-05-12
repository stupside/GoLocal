FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["GoLocal.Identity.Api/GoLocal.Identity.Api.csproj", "GoLocal.Identity.Api/"]
RUN dotnet restore "GoLocal.Identity.Api/GoLocal.Identity.Api.csproj"
COPY . .
WORKDIR "/src/GoLocal.Identity.Api"
RUN dotnet build "GoLocal.Identity.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GoLocal.Identity.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GoLocal.Identity.Api.dll"]
