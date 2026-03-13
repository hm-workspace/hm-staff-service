# syntax=docker/dockerfile:1.7
ARG DOTNET_VERSION=8.0

FROM mcr.microsoft.com/dotnet/sdk: AS build
WORKDIR /src
COPY . .
RUN dotnet restore "src/StaffService.Api/StaffService.Api.csproj"
RUN dotnet publish "src/StaffService.Api/StaffService.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet: AS runtime
WORKDIR /app
COPY --from=build /app/publish ./
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080
ENTRYPOINT ["dotnet", "StaffService.Api.dll"]
