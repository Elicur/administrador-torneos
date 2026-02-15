FROM mcr.microsoft.com/dotnet/sdk:8.0

WORKDIR /app

# Copiamos csproj y restauramos primero (cache)
COPY Torneos.Api.csproj ./
RUN dotnet restore

# Copiamos el resto
COPY . ./

# Para que dotnet watch funcione bien en Docker
ENV DOTNET_USE_POLLING_FILE_WATCHER=1
ENV ASPNETCORE_URLS=http://0.0.0.0:8000

EXPOSE 8000

CMD ["dotnet", "watch", "run", "--urls", "http://0.0.0.0:8000"]
