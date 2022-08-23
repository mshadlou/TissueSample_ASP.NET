#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["TissueSample2/Server/TissueSample2.Server.csproj", "TissueSample2/Server/"]
COPY ["TissueSample2/Client/TissueSample2.Client.csproj", "TissueSample2/Client/"]
COPY ["TissueSample2/Shared/TissueSample2.Shared.csproj", "TissueSample2/Shared/"]
RUN dotnet restore "TissueSample2/Server/TissueSample2.Server.csproj"
COPY . .
WORKDIR "/src/TissueSample2/Server"
RUN dotnet build "TissueSample2.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TissueSample2.Server.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TissueSample2.Server.dll"]