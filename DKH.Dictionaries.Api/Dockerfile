#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["DKH.Dictionaries.Api/DKH.Dictionaries.Api.csproj", "DKH.Dictionaries.Api/"]
COPY ["DKH.Dictionaries.Application/DKH.Dictionaries.Application.csproj", "DKH.Dictionaries.Application/"]
COPY ["DKH.Dictionaries.Domain/DKH.Dictionaries.Domain.csproj", "DKH.Dictionaries.Domain/"]
COPY ["DKH.Dictionaries.Infrastructure/DKH.Dictionaries.Infrastructure.csproj", "DKH.Dictionaries.Infrastructure/"]
RUN dotnet restore "DKH.Dictionaries.Api/DKH.Dictionaries.Api.csproj"
COPY . .
WORKDIR "/src/DKH.Dictionaries.Api"
RUN dotnet build "DKH.Dictionaries.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DKH.Dictionaries.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DKH.Dictionaries.Api.dll"]