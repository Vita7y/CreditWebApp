FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["./CreditWebApp.csproj", "CreditWebApp/"]
RUN dotnet restore "CreditWebApp/CreditWebApp.csproj"
WORKDIR "/src/CreditWebApp"
COPY . .
RUN dotnet build "CreditWebApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CreditWebApp.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CreditWebApp.dll"]
