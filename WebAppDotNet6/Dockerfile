FROM mcr.microsoft.com/dotnet/aspnet:6.0-nanoserver-20H2 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["WebAppDotNet6.csproj", ""]
RUN dotnet restore "./WebAppDotNet6.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "WebAppDotNet6.csproj" -c Release -o /app/build
FROM build AS publish
RUN dotnet publish "WebAppDotNet6.csproj" -c Release -o /app/publish
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
#ENV APP_SETTING_KEY_1="APP_SETTING_KEY_1_ENV_VAL"
ENTRYPOINT ["dotnet", "WebAppDotNet6.dll"]