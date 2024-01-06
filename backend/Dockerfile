FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443


FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["MikartEnergy.WebAPI/MikartEnergy.WebAPI.csproj", "MikartEnergy.WebAPI/"]
RUN dotnet restore "MikartEnergy.WebAPI/MikartEnergy.WebAPI.csproj"
COPY . .
WORKDIR "/src/MikartEnergy.WebAPI"
RUN dotnet build "MikartEnergy.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MikartEnergy.WebAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MikartEnergy.WebAPI.dll"]