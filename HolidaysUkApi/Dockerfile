#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
#WORKDIR /app
#EXPOSE 80
#EXPOSE 443
#
#FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
#WORKDIR /src
#COPY ["HolidaysUkApi/HolidaysUkApi.csproj", "HolidaysUkApi/"]
#RUN dotnet restore "HolidaysUkApi/HolidaysUkApi.csproj"
#COPY . .
#WORKDIR "/src/HolidaysUkApi"
#RUN dotnet build "HolidaysUkApi.csproj" -c Release -o /app/build
#
#FROM build AS publish
#RUN dotnet publish "HolidaysUkApi.csproj" -c Release -o /app/publish
#
#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "HolidaysUkApi.dll"]

FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine as build
WORKDIR /app
COPY . .
RUN dotnet restore
RUN dotnet publish -o /app/published-app

FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine as runtime
WORKDIR /app
COPY --from=build /app/published-app /app
ENTRYPOINT [ "dotnet", "/app/HolidaysUkApi.dll" ]