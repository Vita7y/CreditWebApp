FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
ENV ASPNETCORE_URLS http://+:5000;https://+:5001
EXPOSE 5000
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY CreditWebApp/aspnetcore-cert.pfx ./
COPY CreditWebApp.sln ./
COPY Credit.Core/*.csproj Credit.Core/
COPY Credit.BLL/*.csproj Credit.BLL/
COPY Credit.MockDAL/*.csproj Credit.MockDAL/
COPY CreditWebApp/*.csproj CreditWebApp/
COPY Credit.Tests/*.csproj Credit.Tests/

RUN dotnet restore 
COPY . .
WORKDIR /src/CreditWebApp
RUN dotnet build "CreditWebApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CreditWebApp.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY CreditWebApp/aspnetcore-cert.pfx ./
ENV Kestrel:Certificates:Default:Path=/app/aspnetcore-cert.pfx
ENV Kestrel:Certificates:Default:Password=testpassword
ENV Kestrel:Certificates:Default:AllowInvalid=true
ENV Kestrel:EndPointDefaults:Protocols=Http1AndHttp2
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CreditWebApp.dll"]
