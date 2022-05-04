#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["BFFConsumptionApi/BFFConsumptionApi.csproj", "BFFConsumptionApi/"]
RUN dotnet restore "BFFConsumptionApi/BFFConsumptionApi.csproj"
COPY . .
WORKDIR "/src/BFFConsumptionApi"
RUN dotnet build "BFFConsumptionApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BFFConsumptionApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

#Install curl
#RUN apt-get update \
#&& apt-get install -y --no-install-recommends curl=7.74.* \
#&& apt-get clean \
#&& rm -rf /var/lib/apt/lists/* \
#&& curl -s https://raw.githubusercontent.com/vishnubob/wait-for-it/master/wait-for-it.sh > wait_for_it.sh \
#&& chmod +x wait_for_it.sh
ENTRYPOINT ["dotnet", "BFFConsumptionApi.dll"]