FROM mcr.microsoft.com/dotnet/core/aspnet:3.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.0 AS build
WORKDIR /appcode
COPY ["/src/Project.Backend.csproj", "./src/"]
RUN dotnet restore "./src/Project.Backend.csproj"
COPY /src/ ./src/
ARG buildnumber=1
RUN dotnet publish "./src/Project.Backend.csproj" -c Release -r linux-x64 --version-suffix $buildnumber -o /app

FROM base AS final
WORKDIR /app
COPY --from=build /app .
ENV ASPNETCORE_ENVIRONMENT Production
ENV ASPNETCORE_URLS "http://+"
ENTRYPOINT ["dotnet", "./Project.Backend.dll"]