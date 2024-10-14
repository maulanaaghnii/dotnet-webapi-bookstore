# Menggunakan .NET 8.0 Preview SDK
FROM mcr.microsoft.com/dotnet/sdk:8.0-preview AS build
WORKDIR /src

# Copy dan restore dependencies
COPY ["BookStore/BookStore.csproj", "BookStore/"]
RUN dotnet restore "BookStore/BookStore.csproj"

# Copy seluruh file dan build
COPY . .
WORKDIR "/src/BookStore"
RUN dotnet build "BookStore.csproj" -c Release -o /app/build

# Publish aplikasi
FROM build AS publish
RUN dotnet publish "BookStore.csproj" -c Release -o /app/publish

# Image runtime yang lebih ringan untuk produksi
FROM mcr.microsoft.com/dotnet/aspnet:8.0-preview AS final
WORKDIR /app
COPY --from=publish /app/publish .

EXPOSE 80

ENTRYPOINT ["dotnet", "BookStore.dll"]
