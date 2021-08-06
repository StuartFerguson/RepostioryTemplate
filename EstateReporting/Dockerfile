#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM stuartferguson/txnprocbase:latest AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0.102-ca-patch-buster-slim AS build
WORKDIR /src
COPY ["EstateReporting/NuGet.Config", "."]
COPY ["EstateReporting/EstateReporting.csproj", "EstateReporting/"]
COPY ["EstateReporting/NuGet.Config", "EstateReporting/"]
COPY ["EstateReporting.BusinessLogic/EstateReporting.BusinessLogic.csproj", "EstateReporting.BusinessLogic/"]
COPY ["EstateReporting.Repository/EstateReporting.Repository.csproj", "EstateReporting.Repository/"]
COPY ["EstateReporting.Database/EstateReporting.Database.csproj", "EstateReporting.Database/"]
RUN dotnet restore "EstateReporting/EstateReporting.csproj"
COPY . .
WORKDIR "/src/EstateReporting"
RUN dotnet build "EstateReporting.csproj" -c Release -o /app/build

# Sort out certificate stuff here
RUN openssl x509 -inform DER -in /src/EstateReporting/aspnetapp-root-cert.cer -out /src/EstateReporting/aspnetapp-root-cert.crt
RUN cp /src/EstateReporting/aspnetapp-root-cert.crt /usr/local/share/ca-certificates/
RUN update-ca-certificates

FROM build AS publish
RUN dotnet publish "EstateReporting.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EstateReporting.dll"]