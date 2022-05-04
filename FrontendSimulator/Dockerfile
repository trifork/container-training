#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["FrontendSimulator/FrontendSimulator.csproj", "FrontendSimulator/"]
RUN dotnet restore "FrontendSimulator/FrontendSimulator.csproj"
COPY . .
WORKDIR "/src/FrontendSimulator"
RUN dotnet build "FrontendSimulator.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FrontendSimulator.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Install curl
# RUN apt-get update \
# && apt-get install -y --no-install-recommends curl=7.74.* \
# && apt-get clean \
# && rm -rf /var/lib/apt/lists/* \
# && curl -s https://raw.githubusercontent.com/vishnubob/wait-for-it/master/wait-for-it.sh > wait_for_it.sh \
ENTRYPOINT ["dotnet", "FrontendSimulator.dll"]