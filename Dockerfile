# Use the official image as a parent image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Set the working directory
WORKDIR /source

# Copy the current directory contents into the container at /app
COPY . .

# Restore dependencies
RUN dotnet restore

# Build the application
RUN dotnet publish Eros.Api/Eros.Api.csproj --no-restore -o /app

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

# Set the working directory
WORKDIR /app

# Copy the build output to the container
COPY --from=build /app .

# Expose the port
EXPOSE 80

# Run the application
ENTRYPOINT ["dotnet", "Eros.Api.dll"]
