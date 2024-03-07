FROM mcr.microsoft.com/dotnet/aspnet:6.0 as base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 as build
WORKDIR /src

COPY ["Services/Ordering.API/Ordering.Api/Ordering.API.csproj","Services/Ordering.API/Ordering.Api/"]
COPY ["BuildingBlocks/Common.Logging/Common.Logging.csproj","BuildingBlocks/Common.Logging/"]
COPY ["Services/Ordering.API/Ordering.Application/Ordering.Application.csproj","Services/Ordering.API/Ordering.Application/"]
COPY ["Services/Ordering.API/Ordering.Infrastructure/Ordering.Infrastructure.csproj","Services/Ordering.API/Ordering.Infrastructure/"]

RUN dotnet restore "Services/Ordering.API/Ordering.Api/Ordering.API.csproj"
COPY . .
WORKDIR "/src/Services/Ordering.API/Ordering.Api/"
RUN dotnet build "Ordering.API.csproj" -c Release -o /app/build

FROM build as publish
RUN dotnet publish "Ordering.API.csproj" -c Release -o /app/publish

FROM base as final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet","Ordering.API.dll"]



