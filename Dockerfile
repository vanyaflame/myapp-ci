FROM mcr.microsoft.com/dotnet/aspnet:9.0-alpine AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:9.0-alpine AS build
WORKDIR /src
COPY ["SimpleTaskApp.csproj", "./"]
RUN dotnet restore "SimpleTaskApp.csproj"
COPY . .
RUN dotnet build "SimpleTaskApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SimpleTaskApp.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_URLS=http://0.0.0.0:8080
ENTRYPOINT ["dotnet", "SimpleTaskApp.dll"]
