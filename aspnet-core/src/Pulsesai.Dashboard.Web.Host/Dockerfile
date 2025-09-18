FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src
COPY ["src/Pulsesai.Dashboard.Web.Host/Pulsesai.Dashboard.Web.Host.csproj", "src/Pulsesai.Dashboard.Web.Host/"]
COPY ["src/Pulsesai.Dashboard.Web.Core/Pulsesai.Dashboard.Web.Core.csproj", "src/Pulsesai.Dashboard.Web.Core/"]
COPY ["src/Pulsesai.Dashboard.Application/Pulsesai.Dashboard.Application.csproj", "src/Pulsesai.Dashboard.Application/"]
COPY ["src/Pulsesai.Dashboard.Core/Pulsesai.Dashboard.Core.csproj", "src/Pulsesai.Dashboard.Core/"]
COPY ["src/Pulsesai.Dashboard.EntityFrameworkCore/Pulsesai.Dashboard.EntityFrameworkCore.csproj", "src/Pulsesai.Dashboard.EntityFrameworkCore/"]
WORKDIR "/src/src/Pulsesai.Dashboard.Web.Host"
RUN dotnet restore 

WORKDIR /src
COPY ["src/Pulsesai.Dashboard.Web.Host", "src/Pulsesai.Dashboard.Web.Host"]
COPY ["src/Pulsesai.Dashboard.Web.Core", "src/Pulsesai.Dashboard.Web.Core"]
COPY ["src/Pulsesai.Dashboard.Application", "src/Pulsesai.Dashboard.Application"]
COPY ["src/Pulsesai.Dashboard.Core", "src/Pulsesai.Dashboard.Core"]
COPY ["src/Pulsesai.Dashboard.EntityFrameworkCore", "src/Pulsesai.Dashboard.EntityFrameworkCore"]
WORKDIR "/src/src/Pulsesai.Dashboard.Web.Host"
RUN dotnet publish -c Release -o /publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0
EXPOSE 80
WORKDIR /app
COPY --from=build /publish .
ENTRYPOINT ["dotnet", "Pulsesai.Dashboard.Web.Host.dll"]
