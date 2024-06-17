# Use the official .NET image as the base image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# Use the SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["UsersService/UsersService.csproj", "UsersService/"]
RUN dotnet restore "UsersService/UsersService.csproj"
COPY . .

# Build the app
RUN dotnet build "UsersService/UsersService.csproj" -c Release -o /app/build

# Publish the app
FROM build AS publish
RUN dotnet publish "UsersService/UsersService.csproj" -c Release -o /app/publish

# Create the final image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UsersService.dll"]
