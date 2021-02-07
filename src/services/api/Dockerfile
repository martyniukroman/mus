FROM mcr.microsoft.com/dotnet/core/aspnet AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
ENV ASPNETCORE_URLS="http://*:5000"


# Stage 2: Build and publish the code

FROM mcr.microsoft.com/dotnet/core/sdk AS build
WORKDIR /app
COPY api.csproj .
RUN dotnet restore
COPY . .
RUN dotnet build -c Release

FROM build AS publish
RUN dotnet publish -c Release -o /publish


# Stage 3: Build and publish the code

FROM base AS final
WORKDIR /app
COPY --from=publish /publish .
ENTRYPOINT ["dotnet", "api.dll"]