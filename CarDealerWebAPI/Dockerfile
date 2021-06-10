FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 5000

ENV ASPNETCORE_URLS=http://*:5000
ENV ASPNETCORE_ENVIRONMENT=Development

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["CarDealerWebApi/CarDealerWebApi.csproj", "CarDealerWebApi/"]
RUN dotnet restore "CarDealerWebApi\CarDealerWebApi.csproj"
COPY . .
WORKDIR "/src/CarDealerWebApi"
RUN dotnet build "CarDealerWebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CarDealerWebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CarDealerWebApi.dll"]
