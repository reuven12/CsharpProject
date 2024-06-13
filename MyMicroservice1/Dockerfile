# Use the official .NET image as the base image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# Use the SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["MyMicroservice1/MyMicroservice1.csproj", "MyMicroservice1/"]
RUN dotnet restore "MyMicroservice1/MyMicroservice1.csproj"
COPY . .

# Build the app
RUN dotnet build "MyMicroservice1/MyMicroservice1.csproj" -c Release -o /app/build

# Publish the app
FROM build AS publish
RUN dotnet publish "MyMicroservice1/MyMicroservice1.csproj" -c Release -o /app/publish

# Create the final image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MyMicroservice1.dll"]
