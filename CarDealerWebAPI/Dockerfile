FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 5000

ENV ASPNETCORE_URLS=http://*:5000
ENV ASPNETCORE_ENVIRONMENT=Development

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["CarDealerWebAPI/CarDealerWebAPI.csproj", "CarDealerWebAPI/"]
RUN dotnet restore "CarDealerWebAPI\CarDealerWebAPI.csproj"
COPY . .
WORKDIR "/src/CarDealerWebAPI"
RUN dotnet build "CarDealerWebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CarDealerWebAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CarDealerWebAPI.dll"]